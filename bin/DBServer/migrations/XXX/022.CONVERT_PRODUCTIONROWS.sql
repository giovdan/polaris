USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 8192) THEN
	CALL MigrateAttributeDefinitions(8192) //
END IF //

CREATE OR REPLACE FUNCTION GetProductionRowStatus(iRowStatus INT)
RETURNS VARCHAR(50)
BEGIN
	DECLARE pRowStatusEnum VARCHAR(50);
	
	SET pRowStatusEnum = 
		CASE iRowStatus
			WHEN 1 THEN 'Empty'
			WHEN 2 THEN 'InProgress'
			WHEN 4 THEN 'Processed'
			WHEN 8 THEN 'Executed'
			WHEN 16 THEN 'RQLoad'
			WHEN 32 THEN 'RQAck'
			WHEN 64 THEN 'NotAvailable'
			WHEN 128 THEN 'Aborted'
			WHEN 256 THEN 'NotReady'
		END;	
		
	RETURN pRowStatusEnum;	
END //

CREATE OR REPLACE PROCEDURE ConvertProductionRow(IN iProductionRow INT, pDataFormatId INT)
BEGIN
	DECLARE pHashCode CHAR(64);
	DECLARE pRelatedHashCode CHAR(64);	
	DECLARE pEntityTypeId INT;
	DECLARE pRelatedParentTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pNumberOfStation INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pIsOriginCalculated BOOL;
	DECLARE pPriority INT;
	DECLARE pProfileTypeId INT;
	DECLARE pStatus VARCHAR(50);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	DECLARE pProgramId INT;
   DECLARE pExecutionDate DATETIME;
	DECLARE pOriginsCalculatedLinkId INT; 
	DECLARE pExecutionDateLinkId INT;
	DECLARE pOriginsCalculatedEnumId INT DEFAULT 395;
	DECLARE pExecutionDateEnumId INT DEFAULT 396;
		 
	SELECT pr.ProgramId, pr.ExecutionDate
		   , GetProductionRowStatus(pr.Status) as Status, se.ProfileTypeId
		   , pr.IsOriginCalculated, pr.NumberOfStation, pr.Priority
		   , CONCAT('PR_',p.Name, '_', pr.Id) AS DisplayName
		   , pr.CreatedBy, pr.CreatedOn, pr.UpdatedBy, pr.UpdatedOn 
		   INTO
			pProgramId, pExecutionDate
		   , pStatus, pProfileTypeId
		   , pIsOriginCalculated, pNumberOfStation, pPriority
		   , pDisplayName
		   , pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn 		   
			FROM productionrow pr
			INNER JOIN program p ON p.Id = pr.ProgramId
			INNER JOIN stockentity se ON se.Id=p.StockEntityId
			WHERE pr.Id = iProductionRow;
				
		SET pParentTypeId = 8192; 			# ProductionRow ParentType
		SET pRelatedParentTypeId = 256;		# Program ParentType
		SET pEntityTypeId = GetEntityType(pParentTypeId, pProfileTypeId);
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pHashCode = SHA2(CONCAT(pEntityTypeId,pDisplayName),256);
		# Recupero HashCode dello stock associato
		SELECT e.HashCode INTO pRelatedHashCode FROM entity e 
			INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
			WHERE 
				me.ParentId = pProgramId AND me.ParentTypeId = pRelatedParentTypeId;

		SET newId = 0;
		# Inserimento record tabella
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `Priority`, `RowVersion`
			,CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pNumberOfStation, pPriority, UUID() 
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
		FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
		
		IF newId <> 0 THEN
			INSERT INTO entitylink
			(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
			SELECT pRelatedHashCode, pHashCode, 'ForeignKey', 1, 1 FROM DUAL
				WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
															AND EntityHashCode = pHashCode AND RelationType = 'ForeignKey');		
	
			SELECT adl.Id INTO pOriginsCalculatedLinkId
			FROM attributedefinitionlink adl
			INNER JOIN attributedefinition _ad ON adl.AttributeDefinitionId = _ad.Id
			WHERE adl.EntityTypeId = pEntityTypeId AND _ad.EnumId = pOriginsCalculatedEnumId;
			
			SELECT adl.Id INTO pExecutionDateLinkId
			FROM attributedefinitionlink adl
			INNER JOIN attributedefinition _ad ON adl.AttributeDefinitionId = _ad.Id
			WHERE adl.EntityTypeId = pEntityTypeId AND _ad.EnumId = pExecutionDateEnumId;
	
			INSERT INTO attributevalue
			(EntityId, AttributeDefinitionLinkId, DataFormatId, `Value`, TextValue, Priority, RowVersion)
			SELECT newId, adl.Id as AttributeDefinitionLinkId
				, av.DataFormatId, av.`Value`, av.TextValue, av.Priority
				, UUID() AS RowVersion
				FROM attributevalue_old av 
				INNER JOIN attributedefinition_old ad ON ad.Id = av.AttributeDefinitionId AND av.ParentTypeId = ad.ParentTypeId
		        INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
		        LEFT JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id AND EntityTypeId = pEntityTypeId
				WHERE av.ParentId = iProductionRow AND av.ParentTypeId = pParentTypeId
	         AND adl.Id IS NOT NULL AND adl.Id NOT IN (pOriginsCalculatedLinkId, pExecutionDateLinkId)
				AND NOT EXISTS(SELECT Id FROM attributevalue WHERE EntityId = newId AND AttributeDefinitionLinkId = adl.Id)				
			UNION
			SELECT newId, pOriginsCalculatedLinkId
					, pDataFormatId, pIsOriginCalculated AS `Value`
					, '' AS TextValue
					, 999 AS Priority
					, UUID() AS RowVersion
			FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM attributevalue WHERE EntityId = newId 
							AND AttributeDefinitionLinkId = pOriginsCalculatedLinkId)
			UNION
			SELECT newId, pExecutionDateLinkId
					, pDataFormatId, 0 AS `Value`
					, DATE_FORMAT(pExecutionDate, '%Y-%m-%d') AS TextValue
					, 999 AS Priority
					, UUID() AS RowVersion
			FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM attributevalue WHERE EntityId = newId 
							AND AttributeDefinitionLinkId = pExecutionDateLinkId);
					
			
			INSERT INTO migratedentity
			(EntityTypeId, ParentTypeId, ParentId, EntityId)
			VALUES
			(pEntityTypeId, pParentTypeId, iProductionRow, newId);
		END IF;	

END //

CREATE OR REPLACE PROCEDURE `ConvertProductionRows`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pOriginsCalculatedLinkId INT;
	DECLARE pExecutionDateLinkId INT;
	DECLARE pDataFormatId INT DEFAULT 17;

	
	DECLARE curEntities CURSOR FOR 	
		SELECT Id FROM productionrow;

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
		FETCH curEntities INTO  oldId;

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		CALL ConvertProductionRow(oldId, pDataFormatId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 8192;
	COMMIT;	
END //

CALL ConvertProductionRows() //

DROP PROCEDURE IF EXISTS ConvertProductionRows //
DROP PROCEDURE IF EXISTS ConvertProductionRow //

DELIMITER ; 






