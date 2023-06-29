USE machine;

DELIMITER //

CALL MigrateAttributeDefinitions(1) //

CREATE OR REPLACE PROCEDURE ConvertProfile(IN oldId INT)
BEGIN
	DECLARE pCode VARCHAR(32);
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	DECLARE pContext TEXT DEFAULT 'Errore => Prima di ogni azione';
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	SET pContext = 'Errore => Recupero informazioni';
	SELECT `Code`, ProfileTypeId, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn 
		INTO pCode, pEntityTypeId, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn 
	FROM `profile`
		WHERE Id = oldId;

	SET pParentTypeId = 1;
	SET pContext = 'Errore => Creazione HashCode';
	SET pHashCode = SHA2(CONCAT(pEntityTypeId,pCode), 256);
	
	IF NOT EXISTS (SELECT Id FROM entity WHERE EntityTypeId = pEntityTypeId AND HashCode = pHashCode) THEN
		SET pContext = CONCAT('Errore => Inserimento tabella entity, Parameters => ', pCode, ',', pHashCode, ',', pEntityTypeId);
		# Inserimento nella master entity
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `RowVersion`
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		VALUES
		(pCode, pHashCode, pEntityTypeId, UUID()
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn);
	
		SELECT LAST_INSERT_ID() INTO newId;

		SET pContext = CONCAT('Errore => Inserimento tabella attributevalue, Parameters => ',oldId,',',newId);	
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

 			SET pContext = CONCAT('Errore => Inserimento tabella migratedentity, Parameters => ',oldId,',',newId);				
			INSERT INTO migratedentity
			(EntityTypeId, ParentTypeId, ParentId, EntityId)
			VALUES
			(pEntityTypeId, pParentTypeId, oldId, newId);			
	END IF;	

END //

CREATE OR REPLACE PROCEDURE `ConvertProfiles`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	
	DECLARE curEntities CURSOR FOR 	
	SELECT Id FROM `profile` ORDER BY Id;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO oldId;	

		IF done THEN
			LEAVE loop_entities;		
		END IF;
		
		CALL ConvertProfile(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 1;
	COMMIT;	
END //

CALL ConvertProfiles() //

DROP PROCEDURE IF EXISTS ConvertProfile //
DROP PROCEDURE IF EXISTS ConvertProfiles //

DELIMITER ; 

