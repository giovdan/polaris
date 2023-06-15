USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 16384) THEN
	CALL MigrateAttributeDefinitions(16384) //
END IF //


CREATE OR REPLACE PROCEDURE `ConvertMaterials`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pDisplayName VARCHAR(50);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	DECLARE pParentTypeId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	
	DECLARE curEntities CURSOR FOR 	
		SELECT Id, `Code`, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn FROM material;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 16384;			
	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO  oldId, pDisplayName, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		SET pEntityTypeId = GetEntityType(pParentTypeId, 0, 1);
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pHashCode = SHA2(CONCAT(pEntityTypeId,pDisplayName),256);

		SET newId = 0;
		# Inserimento record tabella
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `RowVersion`
			,CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT pDisplayName, pHashCode, pEntityTypeId, UUID() 
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
		FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;

		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = oldId AND av.ParentTypeId = pParentTypeId
         AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				
			
			INSERT INTO migratedentity
			(EntityTypeId, ParentTypeId, ParentId, EntityId)
			VALUES
			(pEntityTypeId, pParentTypeId, oldId, newId);			
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertMaterials() //

DROP PROCEDURE IF EXISTS ConvertMaterials //

DELIMITER ; 






