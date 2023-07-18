USE machine;

DELIMITER //

CREATE OR REPLACE FUNCTION GetBitMaskValue(iDisplayName VARCHAR(50), iValue INT)
RETURNS INT
BEGIN
	DECLARE bitMaskValue INT DEFAULT 0;
	
	SET bitMaskValue =
	CASE UPPER(iDisplayName) 
		WHEN 'TOOLENABLEA' THEN 1
		WHEN 'TOOLENABLEB' THEN 2
		WHEN 'TOOLENABLEC' THEN 4	
		WHEN 'TOOLENABLED' THEN 8
	END;
	
	RETURN bitMaskValue;
END //

CREATE OR REPLACE FUNCTION GetToolMask(iToolId INT)
RETURNS INT
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE attributeDisplayName VARCHAR(50);
	DECLARE decimalValue DECIMAL(14,7);
	DECLARE bitMaskValue INT;
	DECLARE toolMask INT;
	
	DECLARE curToolEnabledAttributes CURSOR FOR 
		SELECT ad.DisplayName, GetBitMaskValue(ad.DisplayName, av.Value) as BitMaskValue FROM attributevalue av 
		INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
		INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
		AND ad.EnumId IN (159,160,161,162) 
		WHERE av.EntityId = iToolId AND av.Value = 1
		ORDER BY av.Priority;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
	
	OPEN curToolEnabledAttributes;
	
	SET toolMask = 0;
	
	loop_values: LOOP
		FETCH curToolEnabledAttributes INTO attributeDisplayName, bitMaskValue;
		IF done THEN
			LEAVE loop_values;
		END IF;	
		
		SET toolMask = toolMask | bitMaskValue;
	END LOOP;
	CLOSE curToolEnabledAttributes;

	RETURN toolMask;
END //

CREATE OR REPLACE VIEW tools
AS
SELECT e.Id, e.EntityTypeId, e.HashCode, e.SecondaryKey AS ToolManagementId, e.`Status`
, et.IsManaged, et.SecondaryKey AS PlantUnitId, GetToolMask(e.Id) AS ToolMask
FROM entity e
INNER JOIN entitytype et ON e.EntityTypeId = et.Id
WHERE et.ParentType = 'Tool' //

CREATE OR REPLACE VIEW EntityLinks
AS
SELECT e.DisplayName AS RelatedEntityDisplayName, ec.DisplayName AS EntityDisplayName
, e.Id AS RelatedEntityId, ec.Id as EntityId
, el.RelatedEntityHashCode, el.EntityHashCode
, el.RelationType, el.RowNumber, el.`Level` FROM entitylink el
INNER JOIN entity e ON e.HashCode = el.RelatedEntityHashCode
INNER JOIN entity ec ON ec.HashCode = el.EntityHashCode //


CREATE OR REPLACE VIEW attributedefinitions
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, ad.DisplayName
, ad.EnumId, ad.DataFormat, ad.AttributeType, ad.AttributeKind, ad.TypeName
, ad.OverrideType, adl.ControlType, adl.HelpImage, adl.IsCodeGenerator, adl.IsSubFilter
, adl.AttributeScopeId, adl.DefaultBehavior, adl.LastInsertedValue, adl.LastInsertedTextValue
, adl.ProtectionLevel, adl.GroupId, adl.Priority
  FROM attributedefinitionlink adl
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId //

DROP VIEW IF EXISTS toolstatusattributesview //

CREATE OR REPLACE VIEW EntityStatusAttributesView
AS
SELECT av.Id, av.EntityId, av.DataFormatId, av.Priority, av.Value, av.TextValue, av.AttributeDefinitionLinkId
, ad.EnumId, ad.DisplayName, ad.AttributeType, adl.ControlType, ad.AttributeKind, adl.ProtectionLevel, adl.GroupId
, adl.EntityTypeId, et.SecondaryKey
FROM attributevalue av
INNER JOIN attributedefinitionlink adl ON av.AttributeDefinitionLinkId = adl.Id
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
INNER JOIN entitytype et ON et.Id = adl.EntityTypeId
WHERE adl.IsStatusAttribute = 1 //

CREATE OR REPLACE VIEW EntityListView
AS
WITH Links AS (SELECT COUNT(Id) AS LinksCount, EntityHashCode FROM entitylink GROUP BY EntityHashCode)
SELECT e.Id, e.DisplayName, e.EntityTypeId, e.SecondaryKey, e.`Status` 
, COALESCE(l.LinksCount,0) AS IsLinked
, e.HashCode
, COALESCE(qb.ExecutedQuantity, 0) AS ExecutedQuantity,  COALESCE(qb.TotalQuantity, 0) AS TotalQuantity
, COALESCE(qb.TotalQuantity, 0) - COALESCE(qb.ExecutedQuantity, 0) AS ScheduledQuantity
, e.CreatedOn
FROM entity e
LEFT JOIN Links l ON l.EntityHashCode = e.HashCode
LEFT JOIN quantitybacklog qb ON qb.EntityId = e.Id;

CREATE OR REPLACE VIEW EntityAttributesView
AS
SELECT av.EntityId, adl.EntityTypeId, av.Id, CONCAT('LBL_ATTR_', UCASE(`ad`.`DisplayName`)) AS `LocalizationKey`
, CAST(av.`Value` as DECIMAL(14,7)) as Value, av.TextValue, ad.DisplayName, av.AttributeDefinitionLinkId, ad.AttributeType
, ad.AttributeKind, ad.EnumId, ad.DataFormat, adl.ControlType, adl.ProtectionLevel
, adl.Priority, adl.ProcessingTechnology, adl.IsCodeGenerator, adl.AttributeScopeId, adl.GroupId AS AttributeDefinitionGroupId
, adl.IsSubFilter,  adl.IsStatusAttribute, ad.TypeName AS EnumType, COALESCE(adgp.Priority,0) AS GroupPriority
, adl.HelpImage, ad.OverrideType
, et.ParentType
FROM 
	attributevalue av
INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId	
INNER JOIN entitytype et ON et.Id = adl.EntityTypeId
LEFT JOIN attributedefinitiongrouppriority adgp ON adgp.AttributeDefinitionGroupId = adl.GroupId //


DELIMITER ;




