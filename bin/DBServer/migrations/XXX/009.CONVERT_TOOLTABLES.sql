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

CREATE OR REPLACE PROCEDURE ConvertToolTable(in oldId INT)
BEGIN
DECLARE pHashCode CHAR(64);
	DECLARE pRelatedHashCode CHAR(64);	
	DECLARE pToolTypeId INT;
	DECLARE pRangeMasterIdentifierId INT;
	DECLARE pToolMasterIdentifierId INT;
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
	DECLARE pContext TEXT DEFAULT ('Errore => Prima di ogni operazione');
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;

	SET pParentTypeId = 4;
	SET pContext = CONCAT('Errore => Recupero informazioni tabella toorange, Parameters => ', oldId);
	SELECT ToolMasterIdentifierId, RangeMasterIdentifierId, RowNumber,RevisionType
			, PlantUnitId, ToolTypeId, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
			INTO pToolMasterIdentifierId, pRangeMasterIdentifierId, pRowNumber, pRevisionType
				, pPlantUnitId, pToolTypeId, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
			 FROM toolrange	
	WHERE Id = oldId;

	
	SET newId = 0;
	SET pContext = CONCAT('Errore => Recupero informazioni tabella toorange, Parameters => ', pRangeMasterIdentifierId);	
	# Recupero gli identificatori tramite il pRangeMasterIdentifierId e creo HashCode
	SET pDisplayName = GetDisplayValueFromToolMasterId(pRangeMasterIdentifierId, pToolTypeId, oldId);
	SET pProcessingTechnology = GetProcessingTechnology(oldId, pParentTypeId);
	SET pEntityTypeId = GetEntityType(pParentTypeId, pToolTypeId, COALESCE(pProcessingTechnology,1));
	SET pRelatedEntityTypeId = GetEntityType(2, pToolTypeId, COALESCE(pProcessingTechnology,1));
	
	SET pContext = CONCAT('Errore => Creazione HashCode Tool collegato, Parameters => ', pToolMasterIdentifierId);	
	SET pRelatedHashCode = CreateHashCodeByIdentifiers(pRelatedEntityTypeId , pToolMasterIdentifierId, 0);
 	SET pContext = CONCAT('Errore => Creazione HashCode ToolRange, Parameters => ', pRangeMasterIdentifierId, ',', pToolMasterIdentifierId);	
	SET pHashCode = CreateHashCodeByIdentifiers(pEntityTypeId,pRangeMasterIdentifierId, pToolMasterIdentifierId);
	
	IF NOT EXISTS 
		(SELECT Id FROM entitylink el 
			WHERE el.RelatedEntityHashCode = pRelatedHashCode AND el.EntityHashCode = pHashCode
					AND RelationType = 'Child') THEN

		IF NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pRelatedHashCode) THEN
			# Creo un master nella detailidentifier per il tool "parent" che non è in anagrafica
		 	SET pContext = CONCAT('Errore => Creazione record detailidentifier per Tool non presente in anagrafica, Parameters => ', pRelatedEntityTypeId, ',', pRangeMasterIdentifierId, ',', pToolMasterIdentifierId, ',',pRelatedHashCode);	
			INSERT INTO detailidentifier
				(HashCode, AttributeDefinitionLinkId, `Value`, Priority
				, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
			SELECT pRelatedHashCode, adl.Id,  di.Value, adl.Priority
				, di.CreatedBy, di.CreatedOn, di.UpdatedBy, di.UpdatedOn
				FROM detailidentifier_old di
				INNER JOIN attributedefinition_old ad ON ad.Id = di.AttributeDefinitionId AND ad.ParentTypeId = di.ParentTypeId
				INNER JOIN attributedefinition _ad ON _ad.EnumId = ad.EnumId
				INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = _ad.Id 
							AND adl.EntityTypeId = pRelatedEntityTypeId
				WHERE di.MasterId = pToolMasterId;				
		END IF;

	 	SET pContext = CONCAT('Errore => Creazione record in tabella entity');	
		# Inserimento record tabella
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`
			, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pPlantUnitId, UUID()
			, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entity WHERE EntityTypeId = pEntityTypeId AND DisplayName = pDisplayName
									AND SecondaryKey = pPlantUnitId); 

		SELECT Id INTO newId FROM entity WHERE EntityTypeId = pEntityTypeId AND DisplayName = pDisplayName
									AND SecondaryKey = pPlantUnitId;
		
	 	SET pContext = CONCAT('Errore => Creazione record in tabella entitylink');	
		INSERT INTO entitylink
		(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
		SELECT pRelatedHashCode, pHashCode, 'Child', pRowNumber, 1 FROM DUAL
			WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pRelatedHashCode
														AND EntityHashCode = pHashCode AND RelationType = 'Child');		

	 	SET pContext = CONCAT('Errore => Creazione record in tabella detailidentifier');	
		# Inserisco in _detailidentfier utilizzando hashCode creato
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
			WHERE di.MasterId = pRangeMasterIdentifierId
			AND NOT EXISTS (SELECT Id FROM detailidentifier WHERE HashCode = pHashCode);

	 	SET pContext = CONCAT('Errore => Creazione record in tabella attributevalue, Parameters =>', oldId, ',',pParentTypeId, ',', newId, ',', pEntityTypeId);				
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

	 	SET pContext = CONCAT('Errore => Creazione record in tabella migratedentity, Parameters =>', oldId, ',',pParentTypeId, ',', newId, ',', pEntityTypeId);				
		INSERT INTO migratedentity
		(EntityTypeId, ParentTypeId, ParentId, EntityId)
		VALUES
		(pEntityTypeId, pParentTypeId, oldId, newId);
	END IF;		
END //

CREATE OR REPLACE PROCEDURE `ConvertToolTables`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	
		 
	DECLARE curEntities CURSOR FOR 	
	SELECT tr.Id FROM toolrange tr ORDER BY tr.Id;

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

		CALL ConvertToolTable(oldId);
	END LOOP;

	CLOSE curEntities;
	
	UPDATE migratedattribute SET MigrationStatus = 'Migrated' WHERE ParentTypeId = 4;
	COMMIT;	
END //

CALL ConvertToolTables() //

DROP PROCEDURE IF EXISTS ConvertToolTables //

DROP PROCEDURE IF EXISTS ConvertToolTable //

DELIMITER ; 


