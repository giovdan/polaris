USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 64) THEN
	CALL MigrateAttributeDefinitions(64) //
END IF //

CREATE OR REPLACE PROCEDURE `ConvertMarkingTables`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pRelatedHashCode CHAR(64);	
	DECLARE pToolTypeId INT;
	DECLARE pRangeMasterId INT;
	DECLARE pToolManagementId INT;
	DECLARE pToolMasterId INT;
	DECLARE pRevisionType INT;
	DECLARE pEntityTypeId INT;
	DECLARE pRelatedEntityTypeId INT;
	DECLARE pPlantUnitId INT;
	DECLARE pRowNumber INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pProcessingTechnology INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	DECLARE pParameters VARCHAR(4000);
	DECLARE pContext TEXT DEFAULT 'Error => Prima di ogni operazione';
		 
	DECLARE curEntities CURSOR FOR 	
	SELECT mr.Id, mr.ToolMasterId, mr.MasterIdentifierId, mr.RowNumber
				, mr.RevisionType, 2 as PlantUnitId, COALESCE(t.ToolTypeId,51)
				, mr.CreatedBy, mr.CreatedOn, mr.UpdatedBy, mr.UpdatedOn					
			 FROM markingrow mr 
			 LEFT JOIN tool t ON t.ToolMasterId = mr.ToolMasterId
			 ORDER BY mr.Id;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 64;

	START TRANSACTION;		
loop_entities: LOOP
		SET pContext = CONCAT('Errore => Recupero informazioni dal cursore');
		FETCH curEntities INTO oldId, pToolMasterId, pRangeMasterId
				, pRowNumber, pRevisionType, pPlantUnitId, pToolTypeId
				, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;
		
		IF done THEN
			LEAVE loop_entities;		
		END IF;
		

		SET pProcessingTechnology = GetProcessingTechnology(oldId, pParentTypeId);
		SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId, COALESCE(pProcessingTechnology,1));
		SET pRelatedEntityTypeId = GetEntityType(2, pToolTypeId, COALESCE(pProcessingTechnology,1));
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pRelatedHashCode = CreateHashCodeByIdentifiers(pRelatedEntityTypeId , pToolMasterId, 0);
		SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pRangeMasterId, pToolMasterId);
		SET pDisplayName = GetDisplayValueFromToolMasterId(pRangeMasterId, pToolTypeId, pToolMasterId);

		SET newId = 0;
		# Inserimento record tabella
		
		SET pParameters = CONCAT(' Parameters =>', pDisplayName, '-', pHashCode,'-', pEntityTypeId
				,'-', pPlantUnitId,'-', UUID(),'-'
				, pCreatedBy,'-', pCreatedOn,'-', pUpdatedBy, '-', pUpdatedOn);
				
		SET pContext = CONCAT(oldId, ', Errore => Inserimento entity,', pParameters);
		
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pPlantUnitId, UUID()
		, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn FROM
		DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
	
		SET pParameters = CONCAT('Parameters =>', pRelatedHashCode, '-', pHashCode,'-Child-', pRowNumber,'-1');
		SET pContext = CONCAT(oldId, ', Errore => Inserimento entitylink,', pParameters);			
		INSERT INTO entitylink
		(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
		SELECT pRelatedHashCode, pHashCode, 'Child', pRowNumber, 1 FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
														AND EntityHashCode = pHashCode AND RelationType = 'Child');		
	
		# Inserisco in _detailidentfier utilizzando hashCode creato
		SET pParameters = CONCAT('Parameters =>', pHashCode);
		SET pContext = CONCAT(oldId, ', Errore => Inserimento _detailidentifier,', pParameters);			
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
			WHERE di.MasterId = pRangeMasterId
			AND NOT EXISTS (SELECT Id FROM detailidentifier WHERE HashCode = pHashCode);
	
		SET pContext = CONCAT(oldId, ', Errore => Inserimento _attributevalue');
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			, av.CreatedBy, av.CreatedOn, av.UpdatedBy, av.UpdatedOn
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = oldId AND av.ParentTypeId = pParentTypeId
	      AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				
	
			SET pParameters = CONCAT(',Parameters => ', pEntityTypeId, ',',pParentTypeId, ',', oldId, ',', newId);
			SET pContext = CONCAT(oldId, ', Errore => Inserimento migratedentity ', pParameters);
			INSERT INTO migratedentity
			(EntityTypeId, ParentTypeId, ParentId, EntityId)
			SELECT pEntityTypeId, pParentTypeId, oldId, newId FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM migratedentity WHERE EntityTypeId = pEntityTypeId AND 
								ParentTypeId = pParentTypeId AND ParentId = oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertMarkingTables() //

DROP PROCEDURE IF EXISTS ConvertMarkingTables //

DELIMITER ; 




