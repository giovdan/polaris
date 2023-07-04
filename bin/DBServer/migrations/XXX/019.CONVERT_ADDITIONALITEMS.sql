USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 65536) THEN
	CALL MigrateAttributeDefinitions(65536) //
END IF //

CREATE OR REPLACE PROCEDURE ConvertAdditionalItem(iAdditionalItemId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pParentHashCode CHAR(64);
	DECLARE pParentId INT;
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pOperationTypeId INT;
	DECLARE pIndex INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	DECLARE pStatus ENUM('Available','Unavailable','Warning','Alarm','NoIconToDisplay','ToBeDeleted','Original','ModifiedByCustomer','ModifiedByFICEP','ToBeSkipped');
	DECLARE pContext TEXT;
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	SET pParentTypeId = 65536;
	SET pContext = 'Errore => Recupero informazioni';
	SELECT CONCAT(GetDisplayNameForPiece(op.PieceId),'_', opt.DisplayName, '_', op.LineNumber, '_', ae.Index)
		, ae.Index, ae.PieceOperationId, op.`OperationType`
		, ae.CreatedBy, ae.CreatedOn, ae.UpdatedBy, ae.UpdatedOn
		INTO pDisplayName, pIndex, pParentId, pOperationTypeId
		, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
		FROM additionalentity ae
		INNER JOIN pieceoperation op ON op.Id= ae.PieceOperationId
		INNER JOIN operationtype opt ON opt.Id = op.`OperationType`
		WHERE ae.Id = iAdditionalItemId;

	SET pContext = 'Errore => Recupero Tipo Entità ed HashCode';	
	SET pEntityTypeId = GetEntityType(pParentTypeId, pOperationTypeId);
	# Recupero gli identificatori tramite il masterId e creo HashCode
	SET pHashCode = CreateHashCodeByAttributeValues(pEntityTypeId, iAdditionalItemId, pParentTypeId);

START TRANSACTION;	
	SET newId = 0;
	# Inserimento record tabella
	SET pStatus = 'Available';
	
	SET pContext = 'Errore => Inserimento tabella entity';
	INSERT INTO entity
	(`DisplayName`,`HashCode`, `EntityTypeId`, `Status`, `RowVersion`)
	SELECT pDisplayName, pHashCode, pEntityTypeId, pStatus, UUID() FROM DUAL
	WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
		
	SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
	
	IF newId <> 0 THEN
		SET pContext = 'Errore => Recupero HashCode Parent';
		SELECT HashCode INTO pParentHashCode FROM entity e
		INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
		WHERE
			me.ParentId = pParentId AND me.ParentTypeId = 4096;

		SET pContext = CONCAT('Errore => Inserimento tabella entitylink per LINK Operation - AdditionalEntity
								, Parameters => ', iAdditionalItemId,',', newId);							
		# INSERIMENTO LINK Operation - AdditionalEntity					
		INSERT INTO entitylink
		(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
		SELECT pParentHashCode, pHashCode, 'Child', pIndex, 1 FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pParentHashCode
														AND EntityHashCode = pHashCode AND RelationType = 'Child'
 														AND RowNumber = pIndex AND `Level` = 1);		

	
		SET pContext = 'Errore => Inserimento tabella _attributevalue';														
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = iAdditionalItemId AND av.ParentTypeId = pParentTypeId
         AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				

		SET pContext = 'Errore => Inserimento tabella migratedentity';														
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		VALUES
		(pEntityTypeId, pParentTypeId, iAdditionalItemId, newId);
COMMIT;		
	ELSE
		ROLLBACK;
	END IF;

END //

CREATE OR REPLACE PROCEDURE `ConvertAdditionalItemsForPieceOperation`(IN iOperationId INT)
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	 
	DECLARE curEntities CURSOR FOR 	
		SELECT Id
		FROM additionalentity ad
		WHERE ad.PieceOperationId = iOperationId
		ORDER BY ad.Index;

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
		
		CALL ConvertAdditionalItem(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 65536;
END //

CREATE OR REPLACE PROCEDURE `ConvertAdditionalItems`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	 
	DECLARE curEntities CURSOR FOR 	
		SELECT Id
		FROM additionalentity ad
		WHERE ad.Id NOT IN (55800,55801,55807)
		ORDER BY ad.PieceOperationId, ad.Index;

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
		
		CALL ConvertAdditionalItem(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 65536;
END //

CALL ConvertAdditionalItems() //

DROP PROCEDURE IF EXISTS ConvertAdditionalItems //

DROP PROCEDURE IF EXISTS ConvertAdditionalItem //

DELIMITER ; 




