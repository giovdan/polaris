
USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 2) THEN
	CALL MigrateAttributeDefinitions(2) //
END IF //

CREATE OR REPLACE PROCEDURE `ConvertTools`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pToolTypeId INT;
	DECLARE pToolManagementId INT;
	DECLARE pToolMasterId INT;
	DECLARE pMasterId INT;
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pProcessingTechnology INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE attributesCount INT;
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	
	DECLARE curEntities CURSOR FOR 	
	SELECT Id, ToolTypeId, ToolManagementId, ToolMasterId, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
				FROM `tool` ORDER BY ToolManagementId;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 2;

	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO oldId, pToolTypeId, pToolManagementId, pToolMasterId
								, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;	

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		# controllo se esistono gli attributi
		SELECT COUNT(*) INTO attributesCount FROM attributevalue WHERE ParentId = oldId AND ParentTypeId = 2;
		
		IF attributesCount > 0 THEN
         SET pProcessingTechnology = GetProcessingTechnology(oldId, pParentTypeId);
			SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId, COALESCE(pProcessingTechnology,1));
			# Recupero gli identificatori tramite il masterId e creo HashCode
			SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pToolMasterId,0);
			SET pDisplayName = GetDisplayValueFromToolMasterId(pToolMasterId, pToolTypeId);
			
			IF NOT EXISTS (SELECT Id FROM entity WHERE 
					HashCode = pHashCode
					AND EntityTypeId = pEntityTypeId 
					AND SecondaryKey = pToolManagementId) THEN
					
				INSERT INTO entity
				(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
					, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
				VALUES (pDisplayName, pHashCode, pEntityTypeId, pToolManagementId, UUID()
						, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn); 
				
				SELECT LAST_INSERT_ID() INTO newId;

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
					WHERE di.MasterId = pToolMasterId;
	
				INSERT INTO _attributevalue
				(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue
					, Priority, RowVersion
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
				 AND adl.Id IS NOT NULL;				
				
				INSERT INTO migratedentity
				(EntityTypeId, ParentTypeId, ParentId, EntityId)
				VALUES
				(pEntityTypeId, pParentTypeId, oldId, newId);
		
			END IF;
		ELSE
			DELETE FROM tool WHERE Id = oldId;
		END IF;	
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertTools() //

DROP PROCEDURE IF EXISTS ConvertTools //

DELIMITER ; 


