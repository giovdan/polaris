USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 4) THEN
	CALL MigrateAttributeDefinitions(4) //
END IF //

CREATE OR REPLACE FUNCTION GetEntityStatusFromRevisionType(iRevisionType INT)
RETURNS ENUM ('Available','Unavailable','Warning','Alarm','NoIconToDisplay','ToBeDeleted','Original','ModifiedByCustomer','ModifiedByFICEP')
BEGIN
	DECLARE pEntityStatus 
		ENUM('Available','Unavailable','Warning','Alarm','NoIconToDisplay','ToBeDeleted'
				,'Original','ModifiedByCustomer','ModifiedByFICEP');
	
	SET pEntityStatus = 
		CASE 
			WHEN iRevisionType = 0 THEN 'Original'
			WHEN iRevisionType = 1 THEN 'ModifiedByCustomer'
			WHEN iRevisionType = 2 THEN 'ModifiedByFICEP'
			ELSE 'Original'
		END;
		
	RETURN pEntityStatus;					
END //

CREATE OR REPLACE PROCEDURE `ConvertToolTables`()
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
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
		 
	DECLARE curEntities CURSOR FOR 	
	SELECT Id, ToolMasterIdentifierId, RangeMasterIdentifierId, RowNumber,RevisionType
			, PlantUnitId, ToolTypeId, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
			 FROM toolrange
			 ORDER BY Id;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 4;

	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO oldId, pToolMasterId, pRangeMasterId, pRowNumber
					, pRevisionType, pPlantUnitId, pToolTypeId
					, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		SET newId = 0;
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pDisplayName = GetDisplayValueFromToolMasterId(pRangeMasterId, pToolTypeId);
		SET pProcessingTechnology = GetProcessingTechnology(oldId, pParentTypeId);
		SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId, COALESCE(pProcessingTechnology,1));
		SET pRelatedEntityTypeId = GetEntityType(2, pToolTypeId, COALESCE(pProcessingTechnology,1));
		SET pRelatedHashCode = CreateHashCodeByIdentifiers(pRelatedEntityTypeId , pToolMasterId, 0);
		SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pRangeMasterId, pToolMasterId);
		
		IF NOT EXISTS 
			(SELECT Id FROM entitylink el 
				WHERE el.RelatedEntityHashCode = pRelatedHashCode AND el.EntityHashCode = pHashCode
						AND RelationType = 'Child') THEN


			# Inserimento record tabella
			INSERT INTO entity
			(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
				, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
			VALUES(pDisplayName, pHashCode, pEntityTypeId, pPlantUnitId, UUID()
				, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn); 
			
			SELECT LAST_INSERT_ID() INTO newId;
			
			IF EXISTS(SELECT Id FROM entity WHERE HashCode = pRelatedHashCode AND EntityTypeId = pRelatedEntityTypeId) THEN
				INSERT INTO entitylink
				(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
				SELECT pRelatedHashCode, pHashCode, 'Child', pRowNumber, 1 FROM DUAL
					WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
																AND EntityHashCode = pHashCode AND RelationType = 'Child');		
			END IF;

			# Inserisco in _detailidentfier utilizzando hashCode creato
			INSERT INTO _detailidentifier
				(HashCode, AttributeDefinitionLinkId, `Value`, Priority
				, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
				SELECT pHashCode, adl.Id,  di.Value, adl.Priority
					, di.CreatedBy, di.CreatedOn, di.UpdatedBy, di.UpdatedOn
					FROM detailidentifier di
				INNER JOIN attributedefinition ad ON ad.Id = di.AttributeDefinitionId AND ad.ParentTypeId = di.ParentTypeId
				INNER JOIN _attributedefinition _ad ON _ad.EnumId = ad.EnumId
				INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id 
							AND adl.EntityTypeId = pEntityTypeId
				WHERE di.MasterId = pRangeMasterId
				AND NOT EXISTS (SELECT Id FROM _detailidentifier WHERE HashCode = pHashCode);
				
			INSERT INTO _attributevalue
			(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
			SELECT newId, adl.Id as AttributeDefinitionLinkId
				, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
				, UUID() AS RowVersion
				, av.CreatedBy, av.CreatedOn, av.UpdatedBy, av.UpdatedOn
				FROM attributevalue av 
				INNER JOIN attributedefinition ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
				INNER JOIN _attributedefinition _ad ON _ad.EnumId = ad.EnumId
				LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
				WHERE av.ParentId = oldId AND av.ParentTypeId = pParentTypeId
			 AND adl.Id IS NOT NULL
				AND NOT EXISTS(SELECT Id FROM _attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				

			INSERT INTO migratedentity
			(EntityTypeId, ParentTypeId, ParentId, EntityId)
			VALUES
			(pEntityTypeId, pParentTypeId, oldId, newId);
		END IF;	
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertToolTables() //

DROP PROCEDURE IF EXISTS ConvertToolTables //

DELIMITER ; 


