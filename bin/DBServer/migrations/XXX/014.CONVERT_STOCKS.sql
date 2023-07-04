USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 2048) THEN
	CALL MigrateAttributeDefinitions(2048);
END IF;

CREATE OR REPLACE PROCEDURE `ConvertStock`(IN iStockId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	DECLARE pProfileTypeId INT;
	DECLARE pContext TEXT;
	
	SET pContext = CONCAT('Errore recupero informazioni, StockId: ',iStockId);
	SELECT s.ProfileTypeId
			, s.CreatedBy, s.CreatedOn, s.UpdatedBy, s.UpdatedOn 
			INTO 
		 	pProfileTypeId, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
	FROM stockentity s
	WHERE s.Id = iStockId;
				
	SET pContext = CONCAT('Errore recupero EntityTypeId e HashCode, StockId: ',iStockId);				
	SET pParentTypeId = 2048;
	SET pEntityTypeId = GetEntityType(pParentTypeId, pProfileTypeId);
	SET pDisplayName = GetDisplayNameByAttributeValues(iStockId, pParentTypeId);
	# Recupero gli identificatori tramite il masterId e creo HashCode
	SET pHashCode = CreateHashCodeByAttributeValues(pEntityTypeId, iStockId, pParentTypeId);

	SET pContext = CONCAT('Errore inserimento tabella entity, StockId: ',iStockId);				
	SET newId = 0;
	INSERT INTO entity
	(`DisplayName`,`HashCode`, `EntityTypeId`, `RowVersion`)
	VALUES
	(pDisplayName, pHashCode, pEntityTypeId, UUID()); 
	
	SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode;
	
	IF newId <> 0 THEN
		SET pContext = CONCAT('Errore inserimento tabella _attributevalue, StockId: ',iStockId,',',newId);				
		INSERT INTO attributevalue
		(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion)
		SELECT newId, adl.Id as AttributeDefinitionLinkId
			, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
			, UUID() AS RowVersion
			FROM attributevalue_old av 
			INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
	        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
	        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
			WHERE av.ParentId = iStockId AND av.ParentTypeId = pParentTypeId
         AND adl.Id IS NOT NULL
			AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id);				

		SET pContext = CONCAT('Errore inserimento tabella _attributevalue, StockId: ',iStockId,',',newId);				
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		VALUES
		(pEntityTypeId, pParentTypeId, iStockId, newId);
	END IF;		
END;

CREATE OR REPLACE PROCEDURE `ConvertStocks`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;

	 
	DECLARE curEntities CURSOR FOR 	
	SELECT s.Id	FROM stockentity s;
			
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

		CALL ConvertStock(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 2048;
	COMMIT;	
END //

CALL ConvertStocks() //

DROP PROCEDURE IF EXISTS ConvertStocks //

DROP PROCEDURE IF EXISTS ConvertStock //

DELIMITER ; 





