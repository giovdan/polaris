USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 8) THEN
	CALL MigrateAttributeDefinitions(8) //
END IF //

CREATE OR REPLACE FUNCTION GetAttributeDefinitionLinkId(iEnumId INT, iEntityTypeId INT)
RETURNS INT
BEGIN
	DECLARE pAttributeLinkId INT DEFAULT 0;
	
	SELECT adl.Id INTO pAttributeLinkId FROM AttributeDefinitionLink adl
	INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
	WHERE ad.EnumId = iEnumId AND EntityTypeId = iEntityTypeId;
	
	RETURN pAttributeLinkId;
END //

CREATE OR REPLACE PROCEDURE `ConvertToolHolders`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE toolHoldersCount INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pCode VARCHAR(32);
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pEntityTypeId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pCreatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pUpdatedOn DATETIME;
	
	DECLARE curToolHolders CURSOR FOR 	
	SELECT Id,Code
		, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn	
		FROM toolholder ORDER BY Id;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
		UPDATE migratedattribute SET MigrationStatus = 'MigrationKO' WHERE ParentTypeId = pParentTypeId;
	 END; 
	 
	OPEN curToolHolders;

	SET pParentTypeId = 8;	
	SET pEntityTypeId = GetEntityType(pParentTypeId, 0,0);
	SELECT FOUND_ROWS() INTO toolHoldersCount;

	START TRANSACTION;	
	loop_toolHolders: LOOP
			FETCH curToolHolders INTO oldId, pCode
					, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;	
		
			IF done THEN
				LEAVE loop_toolHolders;		
			END IF;
		
			SET pHashCode = SHA2(CONCAT(pEntityTypeId,pCode),256);
			IF NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId) THEN
				# Inserimento nella master entity
				INSERT INTO entity
				(`DisplayName`,`HashCode`, `EntityTypeId`, `RowVersion`
				, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
				VALUES
				(pCode, pHashCode, pEntityTypeId, UUID()
					, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn); 
				
				SELECT LAST_INSERT_ID() INTO newId;
				
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
					AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);	
					
				INSERT INTO migratedentity
				(EntityTypeId, ParentTypeId, ParentId, EntityId)
				VALUES
				(pEntityTypeId, pParentTypeId, oldId, newId);
			END IF;		
		END LOOP;
	
	CLOSE curToolHolders;
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertToolHolders() //

DROP PROCEDURE IF EXISTS ConvertToolHolders //

DELIMITER ; 



