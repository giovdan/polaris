USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 4096) THEN
	CALL MigrateAttributeDefinitions(4096) //
END IF //

CREATE OR REPLACE PROCEDURE ConvertPieceOperation(iOperationId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pParentHashCode CHAR(64);
	DECLARE pPieceHashCode CHAR(64);
	DECLARE pPieceId INT;
	DECLARE pGroupId INT;
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pLineNumber INT;
	DECLARE pToBeSkipped BOOL;
	DECLARE pOperationTypeId INT;
	DECLARE pParentId INT;
	DECLARE pLevel INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	DECLARE pStatus ENUM('Available','Unavailable','Warning', 'Alarm', 'NoIconToDisplay', 'ToBeDeleted'
				, 'Original', 'ModifiedByCustomer'
				, 'ModifiedByFICEP', 'ToBeSkipped'
				, 'Empty', 'InProgress', 'Processed'
				, 'Executed', 'RQLoad', 'RQAck'
				, 'NotAvailable', 'Aborted', 'NotReady');
	DECLARE pContext TEXT;
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	SET pParentTypeId = 4096;
	SET pContext = CONCAT('Errore => Recupero informazioni, OperationId: ', iOperationId);
	SELECT CONCAT(GetDisplayNameForPiece(po.PieceId),'_', op.DisplayName, '_', po.LineNumber) AS DisplayName
		, po.PieceId, po.LineNumber, po.ToBeSkipped, po.`OperationType`, COALESCE(po.ParentId,0) AS ParentId, po.`Level` 
		, po.CreatedBy, po.CreatedOn, po.UpdatedBy, po.UpdatedOn
		INTO
		pDisplayName,  pPieceId
		, pLineNumber, pToBeSkipped, pOperationTypeId, pParentId, pLevel
		, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn			
	FROM pieceoperation po 
		INNER JOIN operationtype op ON op.Id = po.`OperationType`
		INNER JOIN piece p ON p.Id = po.PieceId
	WHERE po.Id = iOperationId AND p.ToBeRemoved = 0;

	SET pContext = CONCAT('Errore => Recupero Tipo Entità ed HashCodeOperationId: ', iOperationId);	
	SET pEntityTypeId = GetEntityType(pParentTypeId, pOperationTypeId, 0);
	# Recupero gli identificatori tramite il masterId e creo HashCode
	SET pHashCode = CreateHashCodeByAttributeValues(pEntityTypeId, iOperationId, pParentTypeId);

START TRANSACTION;	
	SET newId = 0;
	# Inserimento record tabella
	SET pStatus = 'Available';
	IF pToBeSkipped THEN
		SET pStatus = 'ToBeSkipped';
	END IF;
	
	SET pContext = CONCAT('Errore => Inserimento tabella entity, OperationId: ', iOperationId);
	INSERT INTO entity
	(`DisplayName`,`HashCode`, `EntityTypeId`, `Status`, `RowVersion`, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
	SELECT pDisplayName, pHashCode, pEntityTypeId, pStatus, UUID() 
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn			
	FROM DUAL
	WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
	
	SET pContext =	CONCAT('Errore => Recupero Id tabella entity, OperationId: ', iOperationId);			
	SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
		
	IF newId <> 0 THEN
		SET pContext = CONCAT('Errore => Recupero hashcode pezzo, OperationId: ', iOperationId);	
		SELECT HashCode INTO pPieceHashCode FROM entity e
		INNER JOIN migratedentity me ON me.EntityId = e.Id
		WHERE
			me.ParentId = pPieceId AND me.ParentTypeId = 1024;

		SET pContext = CONCAT('Errore => Inserimento tabella entitylink per LINK Piece - Operation, OperationId: ', iOperationId);	
		# INSERIMENTO LINK Piece - Operation					
		INSERT INTO entitylink
		(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
		SELECT pPieceHashCode, pHashCode, 'Child', pLineNumber, pLevel FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pPieceHashCode
														AND EntityHashCode = pHashCode AND RelationType = 'Child'
 														AND RowNumber = pLineNumber AND LEVEL = pLevel);		

		# INSERIMENTO LINK Operation - Operation
		IF pParentId > 0 THEN
			SET pContext = CONCAT('Errore => Inserimento tabella entitylink per LINK Operation - Operation -> Lettura HashCode Id ', iOperationId, ' ParentId '
						, pParentId);												
			SELECT COALESCE(HashCode,'') INTO pParentHashCode FROM entity e 
			INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
			WHERE 
				me.ParentId = pParentId AND me.ParentTypeId = pParentTypeId;

			SET pContext = CONCAT('Errore => Inserimento tabella entitylink per LINK Operation - Operation, OperationId: ', iOperationId);	
			INSERT INTO entitylink
			(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
			SELECT pParentHashCode, pHashCode, 'Child', pLineNumber, pLevel FROM DUAL
				WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pParentHashCode
															AND EntityHashCode = pHashCode AND RelationType = 'Child'
															AND RowNumber = pLineNumber AND LEVEL = pLevel);		
		END IF;	
													
		SET pContext = CONCAT('Errore => Inserimento tabella _attributevalue, OperationId: ', iOperationId);	
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion
			,CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = iOperationId AND av.ParentTypeId = pParentTypeId
         AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				

		SET pContext = CONCAT('Errore => Inserimento tabella migratedentity, OperationId: ', iOperationId);	
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		VALUES
		(pEntityTypeId, pParentTypeId, iOperationId, newId);
COMMIT;		
	ELSE
		ROLLBACK;
	END IF;

END //

CREATE OR REPLACE PROCEDURE `ConvertPieceOperationsForPiece`(IN iPieceId INT)
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	 
	DECLARE curEntities CURSOR FOR 	
		SELECT po.Id
		FROM pieceoperation po 
		WHERE po.PieceId = iPieceId
		ORDER BY po.LineNumber;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
	   SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
loop_entities: LOOP
		FETCH curEntities INTO oldId;

		IF done THEN
			LEAVE loop_entities;		
		END IF;
		
		CALL ConvertPieceOperation(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 4096;
END //


CREATE OR REPLACE PROCEDURE `ConvertPieceOperations`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	 
	DECLARE curEntities CURSOR FOR 	
		SELECT po.Id
		FROM pieceoperation po 
		ORDER BY po.PieceId, po.LineNumber;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
	   SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
loop_entities: LOOP
		FETCH curEntities INTO oldId;

		IF done THEN
			LEAVE loop_entities;		
		END IF;
		
		CALL ConvertPieceOperation(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 4096;
END //

CALL ConvertPieceOperations() //

DROP PROCEDURE IF EXISTS ConvertPieceOperations //

DROP PROCEDURE IF EXISTS ConvertPieceOperation //
DELIMITER ; 




