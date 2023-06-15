USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedentity WHERE ParentTypeId = 256) THEN
	CALL MigrateAttributeDefinitions(256) //
END IF //

CREATE OR REPLACE PROCEDURE `ConvertPrograms`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pRelatedHashCode CHAR(64);	
	DECLARE pStockEntityId INT;	
	DECLARE pEntityTypeId INT;
	DECLARE pRelatedParentTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pProfileTypeId INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pProgramType INT;
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	 
	DECLARE curEntities CURSOR FOR 	
	SELECT p.Id, p.`Name`, p.StockEntityId, p.ProgramType,
			p.CreatedBy, p.CreatedOn, p.UpdatedBy, p.UpdatedOn 
			, si.ProfileTypeId
			FROM program p 
			INNER JOIN stockentity si ON si.Id = p.StockEntityId 
			WHERE p.ToBeRemoved = FALSE;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 256; 			# Program ParentType
	SET pRelatedParentTypeId = 2048;	# StockItem ParentType
	
	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO oldId, pDisplayName, pStockEntityId, pProgramType, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
									,pProfileTypeId;

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		SET pEntityTypeId = GetEntityType(pParentTypeId, pProfileTypeId, 1);
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pHashCode = SHA2(CONCAT(pEntityTypeId,pDisplayName),256);
		# Recupero HashCode dello stock associato
		SELECT e.HashCode INTO pRelatedHashCode FROM entity e 
			INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
			WHERE 
				me.ParentId = pStockEntityId AND me.ParentTypeId = pRelatedParentTypeId;

		SET newId = 0;
		# Inserimento record tabella
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pProgramType, UUID() FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
		
		IF newId <> 0 THEN
			INSERT INTO entitylink
			(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
			SELECT pRelatedHashCode, pHashCode, 'ForeignKey', 1, 1 FROM DUAL
				WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
															AND EntityHashCode = pHashCode AND RelationType = 'ForeignKey');		
	
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
		END IF;	
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = pParentTypeId;
	COMMIT;	
END //

CALL ConvertPrograms() //

DROP PROCEDURE IF EXISTS ConvertPrograms //

DELIMITER ; 




