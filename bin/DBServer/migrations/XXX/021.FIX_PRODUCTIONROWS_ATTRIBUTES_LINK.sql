USE machine;

DELIMITER //

CREATE OR REPLACE PROCEDURE FixProfileTypeAttributesForProductionRow()
BEGIN
	DECLARE done BOOL DEFAULT FALSE;
	DECLARE pProfileTypeId INT;
	
	DECLARE curEntities CURSOR FOR 	
	SELECT Id FROM profiletype ORDER BY Id;
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curEntities;
	
	START TRANSACTION;		
loop_entities: LOOP
		FETCH curEntities INTO  pProfileTypeId;

		IF done THEN
			LEAVE loop_entities;		
		END IF;
		
		INSERT INTO `profiletypeattribute` 
		(`ParentTypeId`, `ProfileTypeId`, `AttributeDefinitionId`, `LastInsertedValue`, `LastInsertedTextValue`, `Priority`
			, `IsCodeGenerator`, `AttributeScopeId`, `UseLastInsertedAsDefault`, `AttributeDefinitionGroupId`) 
		SELECT ParentTypeId, pProfileTypeId, Id, 0, '', Priority, 0 AS IsCodeGenerator 
		, 0 AS AttributeScopeId, 0 AS UseLastInsertedAsDefault, 1 AS `Offset`
		FROM attributedefinition a 
		WHERE a.ParentTypeId = 8192
		AND a.EnumId IN (273,274)
		AND NOT EXISTS (SELECT Id FROM profiletypeattribute WHERE AttributeDefinitionId = a.Id AND ProfileTypeId = pProfileTypeId AND ParentTypeId = 8192);

		IF pProfileTypeId = 13 THEN
			INSERT INTO `profiletypeattribute` 
			(`ParentTypeId`, `ProfileTypeId`, `AttributeDefinitionId`, `LastInsertedValue`, `LastInsertedTextValue`, `Priority`
				, `IsCodeGenerator`, `AttributeScopeId`, `UseLastInsertedAsDefault`, `AttributeDefinitionGroupId`) 
			SELECT ParentTypeId, 13, Id, 0, '', Priority, 0 AS IsCodeGenerator 
			, 0 AS AttributeScopeId, 0 AS UseLastInsertedAsDefault, 1 AS `Offset`
			FROM attributedefinition a 
			WHERE a.ParentTypeId = 8192
			AND a.EnumId IN (246,247,248)
			AND NOT EXISTS (SELECT Id FROM profiletypeattribute WHERE AttributeDefinitionId = a.Id AND ProfileTypeId = 13 AND ParentTypeId = 8192);		
		END IF;
	END LOOP;

	CLOSE curEntities;
	COMMIT;
END //

CALL FixProfileTypeAttributesForProductionRow() //

DROP PROCEDURE FixProfileTypeAttributesForProductionRow //

DELIMITER ;

