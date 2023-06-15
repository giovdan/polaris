USE machine;

DELIMITER //

IF NOT EXISTS(SELECT Id FROM migratedattribute WHERE ParentTypeId = 512) THEN
	CALL MigrateAttributeDefinitions(512) //
END IF //

CREATE OR REPLACE PROCEDURE `ConvertProgramPieceLinks`()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE oldId INT;
	DECLARE pHashCode CHAR(64);
	DECLARE pProgramHashCode CHAR(64);	
	DECLARE pPieceHashCode CHAR(64);
	DECLARE pProgramId INT;	
	DECLARE pPieceId INT;
	DECLARE pGroupId INT;
	DECLARE pEntityTypeId INT;
	DECLARE newId INT;
	DECLARE pParentTypeId INT;
	DECLARE pDisplayName VARCHAR(200);
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATE;
	DECLARE pUpdatedOn DATE;
	 
	DECLARE curEntities CURSOR FOR 	
		SELECT pl.Id, CONCAT(p.Name, '_', GetDisplayNameForPiece(pie.Id)) AS DisplayName
		, pl.ProgramId, pl.PieceId, pl.GroupId
		, pl.CreatedBy,pl.CreatedOn, pl.UpdatedBy, pl.UpdatedOn 
		FROM programpiecelink pl 
		INNER JOIN program p ON p.Id = pl.ProgramId
		INNER JOIN piece pie ON pie.Id = pl.PieceId
		WHERE p.ToBeRemoved = FALSE AND pie.ToBeRemoved = FALSE;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	SET pParentTypeId = 512; 			# Program ParentType
	
	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO
			oldId, pDisplayName, pProgramId, pPieceId, pGroupId
			,pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;

		IF done THEN
			LEAVE loop_entities;		
		END IF;

		SET pEntityTypeId = GetEntityType(pParentTypeId, 0, 0);
		# Recupero gli identificatori tramite il masterId e creo HashCode
		SET pHashCode = SHA2(CONCAT(pEntityTypeId,pDisplayName),256);
		
		SET newId = 0;
		# Inserimento record tabella
		INSERT INTO entity
		(`DisplayName`,`HashCode`, `EntityTypeId`, `SecondaryKey`, `RowVersion`)
		SELECT pDisplayName, pHashCode, pEntityTypeId, pGroupId, UUID() FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId); 
			
		SELECT Id INTO newId FROM entity WHERE HashCode = pHashCode AND EntityTypeId = pEntityTypeId;
		
		IF newId <> 0 THEN
			SELECT HashCode INTO pProgramHashCode FROM entity e
			INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
			WHERE
				me.ParentId = pProgramId AND me.ParentTypeId = 256;

			SELECT HashCode INTO pPieceHashCode FROM entity e
			INNER JOIN migratedentity me ON me.EntityId = e.Id AND me.EntityTypeId = e.EntityTypeId
			WHERE
				me.ParentId = pPieceId AND me.ParentTypeId = 1024;
								
			INSERT INTO entitylink
			(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
			SELECT pProgramHashCode, pHashCode, 'ForeignKey', 1, 1 FROM DUAL
				WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pProgramHashCode
															AND EntityHashCode = pHashCode AND RelationType = 'ForeignKey');		
	
			INSERT INTO entitylink
			(RelatedEntityHashCode, EntityHashCode, RelationType, RowNumber, `Level`)
			SELECT pPieceHashCode, pHashCode, 'ForeignKey', 1, 1 FROM DUAL
				WHERE NOT EXISTS (SELECT Id FROM entitylink WHERE RelatedEntityHashCode = pPieceHashCode
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

CALL ConvertProgramPieceLinks() //

DROP PROCEDURE IF EXISTS ConvertProgramPieceLinks //

DELIMITER ; 




