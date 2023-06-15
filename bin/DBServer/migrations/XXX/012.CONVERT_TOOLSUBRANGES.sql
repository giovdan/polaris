USE machine;

DELIMITER //

CREATE OR REPLACE FUNCTION GetParentTypeFromSubRangeType(iSubRangeTypeId INT)
RETURNS INT
BEGIN
	DECLARE pParentTypeId INT DEFAULT 0;
	
	SET pParentTypeId = 
		CASE 
			WHEN iSubRangeTypeId = 1 THEN 16
			WHEN iSubRangeTypeId = 2 THEN 32
			WHEN iSubRangeTypeId = 4 THEN 64
		END;
		
	RETURN pParentTypeId;
END //

CREATE OR REPLACE PROCEDURE `ConvertToolSubRange`(IN iToolRangeId INT)
BEGIN
	DECLARE oldId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pRelatedHashCode CHAR(64);	
	DECLARE pToolTypeId INT;
	DECLARE pRangeMasterId INT;
	DECLARE pParentId INT;
	DECLARE pSubRangeMasterId INT;
	DECLARE pToolMasterId INT;
	DECLARE pEntityTypeId INT;
	DECLARE pRevisionType INT;
	DECLARE pRowNumber INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pProcessingTechnology INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pUpdatedOn DATETIME;
	DECLARE pSubRangeTypeId INT;
	DECLARE pContext TEXT DEFAULT ('Errore => Prima di ogni operazione');
	DECLARE pParameters VARCHAR(3000);
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
START TRANSACTION;		
		SET pContext = CONCAT('Errore => Recupero informazioni dalla tabella, Parameters =>',iToolRangeId);
		SELECT tsr.Id, tsr.ToolRangeId, tsr.SubRangeTypeId, tr.RangeMasterIdentifierId
			, tsr.RangeMasterId
			, tsr.RowNumber, tsr.RevisionType, tr.ToolTypeId, tr.ToolMasterIdentifierId
			, tsr.CreatedBy, tsr.CreatedOn, tsr.UpdatedBy, tsr.UpdatedOn	
			INTO oldId, pParentId, pSubRangeTypeId, pRangeMasterId, pSubRangeMasterId
			, pRowNumber, pRevisionType, pToolTypeId, pToolMasterId
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
			 FROM toolsubrange tsr 
			 INNER JOIN toolrange tr ON tr.Id = tsr.ToolRangeId			 
			 WHERE tsr.Id = iToolRangeId;

		SET pContext = CONCAT('Errore => Recupero informazioni dalla tabella entity per il parent della tabella, Parameters =>',pParentId);
		SELECT HashCode INTO pRelatedHashCode			 
		FROM entity e
		INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
		WHERE
			me.ParentId = pParentId AND ParentTypeId = 4;

		SET pContext = CONCAT('Errore => Recupero informazioni, Parameters =>',iToolRangeId);						 
		SET newId = 0;
		SET pParentTypeId = GetParentTypeFromSubRangeType(pSubRangeTypeId);
				SET pContext = CONCAT('Errore => Recupero informazioni 2, Parameters =>',iToolRangeId);						 
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pDisplayName = GetSubRangeDisplayValueFromMasterId(pSubRangeMasterId, pSubRangeTypeId);
				SET pContext = CONCAT('Errore => Recupero informazioni 3, Parameters =>',iToolRangeId);						 		
		SET pProcessingTechnology = GetProcessingTechnology(oldId, pParentTypeId);
				SET pContext = CONCAT('Errore => Recupero informazioni 4, Parameters =>',iToolRangeId);						 		
		SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId, COALESCE(pProcessingTechnology,1));
				SET pContext = CONCAT('Errore => Recupero informazioni 5, Parameters =>',iToolRangeId);						 		
		SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pSubRangeMasterId, pRangeMasterId);

		# Inserimento record tabella
		SET pParameters = CONCAT(' Parameters =>', pDisplayName, '-', pHashCode,'-', pEntityTypeId
				,'-', pSubRangeTypeId,'-', UUID(),'-'
				, pCreatedBy,'-', pCreatedOn,'-', pUpdatedBy, '-', pUpdatedOn);
				
		SET pContext = CONCAT(iToolRangeId, ', Errore => Inserimento entity,', pParameters);
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pSubRangeTypeId, UUID()
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
		
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode;

		SET pParameters = CONCAT('Parameters =>', pRelatedHashCode, '-', pHashCode,'-Child-', pRowNumber,'-1');
		SET pContext = CONCAT(iToolRangeId, ', Errore => Inserimento entitylink,', pParameters);	
		INSERT INTO entitylink
		(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
		SELECT pRelatedHashCode, pHashCode, 'Child', pRowNumber, 1 FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
														AND EntityHashCode = pHashCode AND RelationType = 'Child');		
		# Inserisco in _detailidentfier utilizzando hashCode creato
		SET pParameters = CONCAT('Parameters =>', pHashCode);
		SET pContext = CONCAT(iToolRangeId, ', Errore => Inserimento detailidentifier,', pParameters);
		INSERT INTO detailidentifier
			(HashCode, AttributeDefinitionLinkId, `Value`, Priority
				, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
			SELECT pHashCode, adl.Id,  di.Value, adl.Priority 
			, di.CreatedBy, di.CreatedOn, di.UpdatedBy, di.UpdatedOn
			FROM detailidentifier_old di
			INNER JOIN attributedefinition_old ad ON ad.Id = di.AttributeDefinitionId AND ad.ParentTypeId = di.ParentTypeId
			INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
			INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id 
						AND adl.EntityTypeId = pEntityTypeId
			WHERE di.MasterId = pSubRangeMasterId
			AND NOT EXISTS (SELECT Id FROM detailidentifier WHERE HashCode = pHashCode);
			
		SET pContext = CONCAT(iToolRangeId, ', Errore => Inserimento attributevalue');
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
			INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
			INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = oldId AND av.ParentTypeId = pParentTypeId
		 AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				
			
		SET pParameters = CONCAT(',Parameters => ', pEntityTypeId, ',',pParentTypeId, ',', oldId, ',', newId);
		SET pContext = CONCAT(iToolRangeId, ', Errore => Inserimento migratedentity ', pParameters);
		
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		SELECT pEntityTypeId, pParentTypeId, oldId, newId FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM migratedentity WHERE EntityTypeId = pEntityTypeId AND 
							ParentTypeId = pParentTypeId AND ParentId = oldId);
COMMIT;
END //

CREATE OR REPLACE PROCEDURE `ConvertToolSubRanges`(IN iSubRangeTypeId INT)
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;

	
	DECLARE curEntities CURSOR FOR 		
	SELECT tsr.Id
			 FROM toolsubrange tsr 
			 INNER JOIN toolrange tr ON tr.Id = tsr.ToolRangeId 
			 WHERE tsr.SubRangeTypeId = iSubRangeTypeId ORDER BY tsr.Id;

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

		CALL ConvertToolSubRange(oldId);		
	END LOOP;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' 
			WHERE ParentTypeId = GetParentTypeFromSubRangeType(iSubRangeTypeId);

END //

IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 16) THEN
	CALL MigrateAttributeDefinitions(16) //
END IF //

CALL ConvertToolSubRanges(1) //


IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 32) THEN
	CALL MigrateAttributeDefinitions(32) //
END IF //

CALL ConvertToolSubRanges(2) //

DROP PROCEDURE ConvertToolSubRange //
DROP PROCEDURE ConvertToolSubRanges //

DELIMITER ;
