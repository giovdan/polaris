USE machine;

DELIMITER //

CREATE OR REPLACE PROCEDURE ConvertAttributeOverrides()
BEGIN
	DECLARE done BOOLEAN DEFAULT(false);
	DECLARE recordsNumber INT DEFAULT(0);
	DECLARE pEnumId INT;
	DECLARE pParentId INT;
	DECLARE pParentTypeId INT;
	DECLARE pSubParentTypeId INT;
	DECLARE pAttributeValueId INT;
	DECLARE pOverrideType INT;
	DECLARE pCreatedBy VARCHAR(32); 
	DECLARE pUpdatedBy VARCHAR(32);
	DECLARE pCreatedOn DATETIME;
	DECLARE pUpdatedOn DATETIME;
	DECLARE pEntityType INT;
	DECLARE pEntityId INT;
	DECLARE pAttributeOverrideValue DECIMAL(12,6);
	
	DECLARE curOverrideValues CURSOR FOR 	
		SELECT ad.EnumId, av.ParentId, av.ParentTypeId, av.SubParentTypeId, aov.AttributeValueId
		, aov.OverrideType, aov.Value
		, aov.CreatedBy, aov.CreatedOn, aov.UpdatedBy, aov.UpdatedOn 
		FROM attributeoverridevalue aov
		INNER JOIN attributevalue av ON av.Id = aov.AttributeValueId
		INNER JOIN attributedefinition ad ON ad.Id = av.AttributeDefinitionId 
		AND ad.ParentTypeId = av.ParentTypeId;
		
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 
	 
	OPEN curOverrideValues;
	
	SELECT FOUND_ROWS() INTO recordsNumber;
	
	START TRANSACTION;		
loop_entities: LOOP
			FETCH curOverrideValues INTO 
				pEnumId, pParentId, pParentTypeId, pSubParentTypeId, pAttributeValueId, pOverrideType
				, pAttributeOverrideValue
				, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn;
	
			IF done THEN
				LEAVE loop_entities;		
			END IF;
			
			SELECT EntityTypeId, EntityId INTO pEntityType, pEntityId 
					FROM migratedentity WHERE ParentId = pParentId AND ParentTypeId = pParentTypeId;
					
			SELECT _av.Id INTO pAttributeValueId 
			FROM _attributevalue _av
			INNER JOIN attributedefinitionlink adl ON adl.Id = _av.AttributeDefinitionLinkId
			INNER JOIN _attributedefinition _ad ON _ad.Id = adl.AttributeDefinitionId
			WHERE 
				_ad.EnumId = pEnumId AND adl.EntityTypeId = pEntityType
				AND _av.EntityId = pEntityId;
				
			IF pAttributeValueId IS NOT NULL THEN
				INSERT INTO _attributeoverridevalue
				(AttributeValueId, OverrideType, `Value`, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
				SELECT pAttributeValueId, pOverrideType, pAttributeOverrideValue, pCreatedBy, pCreatedOn, pUpdatedBy, pUpdatedOn
				FROM DUAL
					WHERE NOT EXISTS (SELECT Id FROM _attributeoverridevalue WHERE AttributeValueId = pAttributeValueId AND OverrideType = pOverrideType);
			END IF;	
				
		END LOOP;
	COMMIT;	
	CLOSE curOverrideValues;
END //

CALL ConvertAttributeOverrides() //
DROP PROCEDURE ConvertAttributeOverrides //

DELIMITER ;



