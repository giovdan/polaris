USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 1024) THEN
	CALL MigrateAttributeDefinitions(1024) //
END IF //

CREATE OR REPLACE PROCEDURE `ConvertPiece`(IN iPieceId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT DEFAULT 1024;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pProfileTypeId INT;
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	DECLARE pContract VARCHAR(50);
	DECLARE pProject VARCHAR(50);
	DECLARE pDrawing VARCHAR(50);
	DECLARE pAssembly VARCHAR(50);
	DECLARE pPart VARCHAR(50);				
	DECLARE pContext TEXT DEFAULT ('Errore => Prima di ogni operazione');
	DECLARE pParameters VARCHAR(3000);
	DECLARE pContractAttributeId INT;
	DECLARE pProjectAttributeId INT;
	DECLARE pDrawingAttributeId INT;
	DECLARE pAssemblyAttributeId INT;
	DECLARE pPartAttributeId INT;
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
      ROLLBACK;
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	START TRANSACTION;
	SET pContext = CONCAT('Errore nel recupero informazioni dalla tabella Piece, Paramteres: ', iPieceId);
	SELECT GetDisplayNameForPiece(p.Id) AS DisplayName
		, p.Contract, p.Project, p.Drawing, p.Assembly, p.Part
		, p.ProfileTypeId, p.CreatedBy, p.CreatedOn, p.UpdatedBy, p.UpdatedOn
	INTO pDisplayName
		, pContract, pProject, pDrawing, pAssembly, pPart
		, pProfileTypeId, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
	FROM piece p WHERE Id = iPieceId;
	
	SET pContext = CONCAT('Errore nel recupero informazioni per la tabella entity');
	SET pEntityTypeId = GetEntityType(pParentTypeId, pProfileTypeId, 1);
	# Recupero gli identificatori tramite il masterId e creo HashCode
	SET pHashCode = SHA2(CONCAT(pEntityTypeId,pDisplayName),256);

	SET pParameters = CONCAT(',Parameters => ',pDisplayName, '_', pEntityTypeId);
	SET pContext = CONCAT('Inserimento record nella tabella entity ',pParameters);
	# Inserimento record tabella
	INSERT INTO entity
	(`DisplayName`,`HashCode`, `EntityTypeId`, `RowVersion`, CreatedBy, CreatedOn
		,UpdatedBy, UpdatedOn)
	SELECT pDisplayName, pHashCode, pEntityTypeId, UUID(),
		pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn FROM DUAL
	WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
	SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
	
	IF newId > 0 THEN
		SET pParameters = CONCAT(' Parameters => ',newId,'_',pEntityTypeId,'_',iPieceId,'_',pParentTypeId);
		SET pContext = CONCAT('Inserimento record nella tabella attributevalue, ',pParameters);		
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion
			,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn 
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	      INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	      INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = iPieceId AND av.ParentTypeId = pParentTypeId
	      AND _ad.EnumId NOT IN (390,391,392,393,394) 
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				
	
		SET pContext = CONCAT('Inserimento record nella tabella detailidentifier , Parameter: Contract');		
		SET pContractAttributeId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'Contract');
		INSERT INTO detailidentifier
		(HashCode, AttributeDefinitionLinkId, VALUE, Priority)
		SELECT pHashCode, Id, pContract, 1 FROM attributedefinitionlink adl 
		WHERE 
			AttributeDefinitionId = pContractAttributeId
			AND NOT EXISTS (SELECT Id FROM detailidentifier WHERE HashCode = pHashCode 
												AND AttributeDefinitionLinkId = adl.Id 
												AND adl.Priority = 1);
	
		SET pContext = CONCAT('Inserimento record nella tabella detailidentifier , Parameter: Project');
		SET pProjectAttributeId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'Project');		
		INSERT INTO detailidentifier
		(HashCode, AttributeDefinitionLinkId, `Value`, Priority)
		SELECT pHashCode, Id, pProject, 2 FROM attributedefinitionlink adl WHERE 
				AttributeDefinitionId = pProjectAttributeId
				AND NOT EXISTS (SELECT Id FROM detailidentifier 
													WHERE HashCode = pHashCode AND AttributeDefinitionLinkId = 
													adl.Id AND adl.Priority = 2);														
	
		SET pContext = CONCAT('Inserimento record nella tabella detailidentifier , Parameter: Drawing');															
		SET pDrawingAttributeId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'Drawing');
		INSERT INTO detailidentifier
		(HashCode, AttributeDefinitionLinkId, VALUE, Priority)
		SELECT pHashCode, Id, pDrawing, 3 FROM attributedefinitionlink adl WHERE 
				AttributeDefinitionId = pDrawingAttributeId
				AND NOT EXISTS (SELECT Id FROM detailidentifier 
													WHERE HashCode = pHashCode AND AttributeDefinitionLinkId = 
													adl.Id AND adl.Priority = 3);														
	
		SET pContext = CONCAT('Inserimento record nella tabella detailidentifier , Parameter: Assembly');
		SET pAssemblyAttributeId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'Assembly');
		INSERT INTO detailidentifier
		(HashCode, AttributeDefinitionLinkId, VALUE, Priority)
		SELECT pHashCode, Id, pAssembly, 4 FROM 
			attributedefinitionlink adl 
			WHERE 
				AttributeDefinitionId = pAssemblyAttributeId
				AND NOT EXISTS (SELECT Id FROM detailidentifier 
													WHERE HashCode = pHashCode AND AttributeDefinitionLinkId = 
													adl.Id AND adl.Priority = 4);														
	
		SET pContext = CONCAT('Inserimento record nella tabella detailidentifier , Parameter: Part');													
		SET pPartAttributeId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'Part');	
		INSERT INTO detailidentifier
		(HashCode, AttributeDefinitionLinkId, VALUE, Priority)
		SELECT pHashCode, Id, pPart, 5 FROM attributedefinitionlink adl WHERE 
				AttributeDefinitionId  = pPartAttributeId
				AND NOT EXISTS (SELECT Id FROM detailidentifier 
													WHERE HashCode = pHashCode AND AttributeDefinitionLinkId = 
													adl.Id AND adl.Priority = 5);																																																								
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		VALUES
		(pEntityTypeId, pParentTypeId, iPieceId, newId);
	ELSE
		SET pContext = 'Errore Inserimento entity non riuscito';
	END IF;	
	
	COMMIT;
END //

CREATE OR REPLACE PROCEDURE `ConvertPieces`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	 
	DECLARE curEntities CURSOR FOR 	
	SELECT Id
	FROM piece
	WHERE 
		ToBeRemoved = FALSE;

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
		
		CALL ConvertPiece(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 1024;
END //

CALL ConvertPieces() //

DROP PROCEDURE IF EXISTS ConvertPieces //
DROP PROCEDURE IF EXISTS ConvertPiece //

DELIMITER ; 




