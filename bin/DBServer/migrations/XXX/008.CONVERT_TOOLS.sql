
USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 2 AND MigrationStatus = 'DefinitionMigrated') THEN
	CALL MigrateAttributeDefinitions(2) //
END IF //

CREATE OR REPLACE PROCEDURE ConvertTool(IN oldId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pToolTypeId INT;
	DECLARE pToolManagementId INT;
	DECLARE pToolMasterId INT;
	DECLARE pMasterId INT;
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE attributesCount INT;
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	DECLARE pContext TEXT DEFAULT ('Errore => Prima di ogni operazione');
	
		DECLARE EXIT HANDLER FOR SQLEXCEPTION
		BEGIN
			SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
		END;
	
		SET pParentTypeId = 2;
		SET pContext = CONCAT('Errore => Recupero informazioni, Parameters: => ', oldId);
		
		SELECT ToolTypeId, ToolManagementId, ToolMasterId, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
		INTO pToolTypeId, pToolManagementId, pToolMasterId, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
		FROM 
			tool 
		WHERE Id = oldId;
		
		SET pContext = CONCAT('Errore => Controllo esistenza attributi, Parameters: => ', oldId);
		# controllo se esistono gli attributi
		SELECT COUNT(*) INTO attributesCount FROM attributevalue_old WHERE ParentId = oldId AND ParentTypeId = 2;
		
		IF attributesCount > 0 THEN
  			SET pContext = CONCAT('Errore => Recupero EntitType, Parameters: => ', oldId);
			SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId);
			# Recupero gli identificatori tramite il masterId e creo HashCode
			SET pContext = CONCAT('Errore => Creazione HashCode, Parameters: => ', pEntityTypeId, ',', pToolMasterId);
			SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pToolMasterId,0);
			SET pContext = CONCAT('Errore => Recupero DisplayName, Parameters: => ', pToolTypeId, ',', pToolMasterId);			
			SET pDisplayName = GetDisplayValueFromToolMasterId(pToolMasterId, pToolTypeId, 0);

			SET pContext = CONCAT('Errore => Controllo esistenza entity, Parameters: => ', pEntityTypeId, ',', pToolMasterId);						
			IF NOT EXISTS (SELECT Id FROM entity WHERE 
					HashCode = pHashCode
					AND EntityTypeId = pEntityTypeId 
					AND SecondaryKey = pToolManagementId) THEN

				SET pContext = CONCAT('Errore => Inserimento in tabella entity, Parameters: => ', pEntityTypeId, ',', oldId, ',', pDisplayName, ',', pHashCode);											
				INSERT INTO entity
				(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
					, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
				VALUES (pDisplayName, pHashCode, pEntityTypeId, pToolManagementId, UUID()
						, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn); 
				
				SELECT LAST_INSERT_ID() INTO newId;

				# Inserisco in _detailidentfier utilizzando hashCode creato
				SET pContext = CONCAT('Errore => Inserimento in tabella detailidentifier, Parameters: => ', pHashCode);															
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
					WHERE di.MasterId = pToolMasterId;

				SET pContext = CONCAT('Errore => Inserimento in tabella attributevalue, Parameters: => ', newId);																
				INSERT INTO attributevalue
				(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue
					, Priority, RowVersion
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
				 AND adl.Id IS NOT NULL;				
				
				SET pContext = CONCAT('Errore => Inserimento in tabella migratedentity, Parameters: => ', newId);		
				INSERT INTO migratedentity
				(EntityTypeId, ParentTypeId, ParentId, EntityId)
				VALUES
				(pEntityTypeId, pParentTypeId, oldId, newId);
		
			END IF;
		ELSE
			SET pContext = CONCAT('Errore => Cancellazione entity, Parameters: => ', oldId);								
			DELETE FROM tool WHERE Id = oldId;
		END IF;	
END //

CREATE OR REPLACE PROCEDURE `ConvertTools`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	
	DECLARE curEntities CURSOR FOR 	
	SELECT Id FROM `tool` ORDER BY ToolManagementId;

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

		CALL ConvertTool(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 2;
	COMMIT;	
END //

CALL ConvertTools() //

DROP PROCEDURE IF EXISTS ConvertTools //

DROP PROCEDURE IF EXISTS ConvertTool //

DELIMITER ; 


