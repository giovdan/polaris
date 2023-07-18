USE machine;

START TRANSACTION;

# Aggiornamento IsAttributeStatus
UPDATE attributedefinitionlink adl
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
SET adl.IsStatusAttribute = 1
WHERE ad.DisplayName IN ('MaxToolLife','ToolLife','WarningToolLife','ToolLength','AutoSensitiveEnable'
		,'ToolEnableA','ToolEnableB','ToolEnableC','ToolEnableD'
		,'NozzleLifeMaxTime','NozzleLifeTime','NozzleLifeWarningTime'
		,'NozzleLifeMaxIgnitions','NozzleLifeIgnitions','NozzleLifeWarningLimitIgnitions'
		,'BladeLife','MaxBladeLife','WarningBladeLife');

DROP VIEW IF EXISTS attributevaluesview;		
DROP VIEW IF EXISTS attributedefinitionView;
DROP VIEW IF EXISTS detailidentifiersview;
DROP VIEW IF EXISTS toollifeattributesview;
DROP VIEW IF EXISTS ToolStatusAttributesView;
DROP VIEW IF EXISTS toolrangeminrequiredconsoleview;
DROP VIEW IF EXISTS plasmatoolmasters;
DROP VIEW IF EXISTS tootypeattributedefinitionsview;
DROP VIEW IF EXISTS stockattributesview;
DROP VIEW IF EXISTS stocksview;

CREATE OR REPLACE VIEW stockattributesview_old
AS
SELECT `av`.`Id` AS `Id`,`ad`.`Code` AS `AttributeCode`,`ad`.`DisplayName` AS `DisplayName`
,`ad`.`EnumId` AS `EnumId`,`ad`.`Description` AS `Description`,`av`.`Value` AS `Value`
,`av`.`AttributeDefinitionId` AS `AttributeDefinitionId`,`av`.`TextValue` AS `TextValue`
,`ad`.`AttributeKindId` AS `AttributeKindId`,`av`.`DataFormatId` AS `DataFormatId`,`stock`.`ProfileTypeId` AS `ProfileType`
,`stock`.`Id` AS `IDStock`,`ad`.`ControlTypeId` AS `ControlTypeId`,`ad`.`OverrideTypeId` AS `OverrideTypeId`
FROM `attributevalue_old` `av`
INNER JOIN `attributedefinition_old` `ad` ON `ad`.`Id` = `av`.`AttributeDefinitionId` AND ad.ParentTypeId = av.ParentTypeId
INNER JOIN `stockentity` `stock` ON `stock`.`Id` = `av`.`ParentId`
WHERE `av`.`ParentTypeId` = 2048
ORDER BY `av`.`ParentId`;

CREATE OR REPLACE VIEW stocksview_old
AS
WITH StockWithInfo AS (
SELECT `s`.`Id` AS `Id`,`s`.`ProfileTypeId` AS `ProfileTypeId`,`s`.`WarehouseId` AS `WarehouseId`
,`s`.`WarehousePosition` AS `WarehousePosition`
,`GetAttributeValue_old`('MaterialCode',`s`.`Id`,2048) AS `MaterialCode`
FROM `stockentity` `s`)
SELECT `s`.`Id` AS `Id`,`s`.`ProfileTypeId` AS `ProfileTypeId`,`s`.`MaterialCode` AS `MaterialCode`
, CAST(`GetAttributeValue_old`('MaterialTypeAttribute',`m`.`Id`,16384) AS signed) AS `MaterialTypeId`
FROM `material` `m`
INNER JOIN `stockwithinfo` `s` ON `s`.`MaterialCode` = `m`.`Code`;

CREATE OR REPLACE VIEW tootypeattributedefinitionsview_old
AS
SELECT `ad`.`Id` AS `Id`,`ad`.`EnumId` AS `EnumId`,`ad`.`Code` AS `Code`,`ad`.`DisplayName` AS `DisplayName`,`ad`.`AttributeTypeId` AS `AttributeTypeId`,`ad`.`AttributeKindId` AS `AttributeKindId`,`ad`.`DataFormatId` AS `DataFormatId`,`ad`.`ControlTypeId` AS `ControlTypeId`,`ad`.`OverrideTypeId` AS `OverrideTypeId`,`ad`.`TypeName` AS `TypeName`,`ad`.`GroupId` AS `GroupId`,`ad`.`HelpImage` AS `HelpImage`,`ad`.`ProtectionLevel` AS `ProtectionLevel`,`tta`.`ToolTypeId` AS `ToolTypeId`,`tta`.`ParentTypeId` AS `ParentTypeId`,`tta`.`ProcessingTechnology` AS `ProcessingTechnology`,`tta`.`Priority` AS `Priority`,`tta`.`UseLastInsertedAsDefault` AS `UseLastInsertedAsDefault`,`tta`.`LastInsertedValue` AS `LastInsertedValue`,`tta`.`LastInsertedTextValue` AS `LastInsertedTextValue`
FROM (`attributedefinition_old` `ad`
JOIN `tooltypeattribute` `tta` ON(`tta`.`AttributeDefinitionId` = `ad`.`Id` AND `tta`.`ParentTypeId` = `ad`.`ParentTypeId`))
WHERE `ad`.`ControlTypeId` <> 1
ORDER BY `tta`.`Priority`;

CREATE OR REPLACE VIEW plasmatoolmasters
AS
SELECT DISTINCT HashCode, EntityTypeId, CAST(Split(GetCodeFromHashCode(HashCode, e.EntityTypeId),'-',2) AS SIGNED) AS PlasmaCurrent
, GetDisplayValueFromHashCode(HashCode, e.EntityTypeId) AS DisplayValue, 1 AS AtLeastOneTool
FROM entity e
WHERE e.EntityTypeId = 51
UNION
SELECT DISTINCT el.RelatedEntityHashCode, 51 as EntityTypeId
, CAST(Split(GetCodeFromHashCode(el.RelatedEntityHashCode, 51), '-',2) AS SIGNED) AS PlasmaCurrent
, GetDisplayValueFromHashCode(el.RelatedEntityHashCode, 51) AS DisplayValue, 0 AS AtLeastOneTool
FROM entitylink el
INNER JOIN entity e ON el.EntityHashCode = e.HashCode
WHERE 
	e.EntityTypeId = 151
	AND el.RelatedEntityHashCode NOT IN (SELECT HashCode FROM entity WHERE EntityTypeId = 51)
ORDER BY PlasmaCurrent;

CREATE OR REPLACE VIEW plasmatoolmasters_old
as
SELECT DISTINCT `t`.`ToolMasterId` AS `ToolMasterId`,`t`.`ToolTypeId` AS `ToolTypeId`, CAST(`Split`(`GetCodeFromToolMasterId`(`t`.`ToolMasterId`,`t`.`ToolTypeId`),'-',2) AS signed) AS `PlasmaCurrent`,`GetDisplayValueFromToolMasterId`(`t`.`ToolMasterId`,`t`.`ToolTypeId`, 0) AS `DisplayValue`,1 AS `AtLeastOneTool`
FROM `tool` `t`
WHERE `t`.`ToolTypeId` = 51 UNION
SELECT DISTINCT `tr`.`ToolMasterIdentifierId` AS `ToolMasterId`,`mi`.`ParentId` AS `tooltypeId`
	, CAST(`Split`(`GetCodeFromToolMasterId`(`tr`.`ToolMasterIdentifierId`,`mi`.`ParentId`),'-',2) AS signed) AS `PlasmaCurrent`,`GetDisplayValueFromToolMasterId`(`tr`.`ToolMasterIdentifierId`,`mi`.`ParentId`,0) AS `DisplayValue`,0 AS `AtLeastOneTool`
FROM (`toolrange` `tr`
JOIN `masteridentifier` `mi` ON(`mi`.`Id` = `tr`.`ToolMasterIdentifierId`))
WHERE `mi`.`ParentTypeId` = 2 AND `mi`.`ParentId` = 51 AND !(`mi`.`Id` in (
SELECT DISTINCT `tool`.`ToolMasterId`
FROM `tool`
WHERE `tool`.`ToolTypeId` = 51))
ORDER BY `PlasmaCurrent`;

CREATE OR REPLACE VIEW attributedefinitionView 
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, ad.DisplayName
, adl.HelpImage, ad.AttributeType,ad.TypeName
, ad.OverrideType, adl.ControlType, ad.AttributeKind
, ad.DataFormat, adl.ProtectionLevel, adl.GroupId, adl.Priority 
FROM attributedefinition ad
INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = ad.Id;

CREATE OR REPLACE VIEW detailidentifiersview AS
SELECT di.Id,
		   di.HashCode,
		   di.Value,
		   di.AttributeDefinitionLinkId,
		   di.Priority,
		   adl.EntityTypeId,
		   adl.AttributeScopeId,
		   adl.IsCodeGenerator,
		   ad.AttributeKind,
		   ad.DataFormat,
		   ad.DisplayName,
		   ad.TypeName,
		   ad.EnumId,
		   ad.AttributeType,
		   adl.ControlType,
		   adl.GroupId,
		   adl.HelpImage,
		   adl.ProtectionLevel
	  FROM detailidentifier di
	  INNER JOIN attributedefinitionlink adl ON adl.Id = di.AttributeDefinitionLinkId
	  INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
	  ORDER BY adl.EntityTypeId; 
	  
CREATE OR REPLACE VIEW detailidentifiersview_OLD
AS
SELECT `di`.`Id` AS `Id`,`di`.`MasterId` AS `MasterIdentifierId`,`di`.`Value` AS `Value`
,`di`.`AttributeDefinitionId` AS `AttributeDefinitionId`
,`di`.`Priority` AS `Priority`,`tta`.`ParentTypeId` AS `ParentTypeId`
,`tta`.`AttributeScopeId` AS `AttributeScopeId`,`tta`.`IsCodeGenerator` AS `IsCodeGenerator`
,`tta`.`ProcessingTechnology` AS `ProcessingTechnology`
,`tta`.`ToolTypeId` AS `ToolTypeId`,`ad`.`EnumId` AS `EnumId`
FROM `detailidentifier_old` `di`
INNER JOIN `tooltypeattribute` `tta` ON `tta`.`AttributeDefinitionId` = `di`.`AttributeDefinitionId` AND `tta`.`ParentTypeId` = `di`.`ParentTypeId` AND `tta`.`ToolTypeId` = `GetToolTypeByMasterId`(`di`.`MasterId`)
INNER JOIN `attributedefinition_old` `ad` ON `ad`.`Id` = `tta`.`AttributeDefinitionId` AND `ad`.`ParentTypeId` = `tta`.`ParentTypeId`;	  

CREATE OR REPLACE VIEW migratedtypes AS
SELECT mt.Id, mt.EntityTypeId, et.DisplayName AS EntityType, mt.ParentTypeId
FROM migratedentity mt
INNER JOIN entitytype et ON et.Id = mt.EntityTypeId;


CREATE OR REPLACE VIEW toolrangeminrequiredconsoleview
AS		
SELECT e.Id AS EntityId, di.HashCode, CAST(COALESCE(di.Value,-1) AS INT) AS MinRequiredConsole 
FROM 
detailidentifier di
INNER JOIN entity e ON e.HashCode = di.HashCode
LEFT OUTER JOIN attributedefinitionlink adl ON adl.Id = di.AttributeDefinitionLinkId
LEFT JOIN entitytype et ON et.Id = adl.EntityTypeId
LEFT OUTER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
WHERE et.ParentType = 'ToolRange' AND ad.DisplayName = 'MinRequiredConsole';

CREATE OR REPLACE VIEW toolstatusattributesview
AS		
SELECT av.Id, av.EntityId, adl.EntityTypeId, av.DataFormatId, av.Priority,av.Value, av.TextValue
, av.AttributeDefinitionLinkId, ad.EnumId, ad.DisplayName
, adl.AttributeType, adl.ControlType, ad.AttributeKind, adl.ProtectionLevel, adl.GroupId
, et.SecondaryKey AS PlantUnitId
FROM attributevalue av 
INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
INNER JOIN entitytype et ON et.Id = adl.EntityTypeId
WHERE adl.IsStatusAttribute = 1;

CREATE OR REPLACE VIEW stocklistview
AS
SELECT `s`.`Id` AS `Id`,`s`.`ProfileTypeId` AS `ProfileTypeId`
,`s`.`WarehouseId` AS `WarehouseId`,`s`.`WarehousePosition` AS `WarehousePosition`
,`qb`.`TotalQuantity` AS `TotalQuantity`,`qb`.`ExecutedQuantity` AS `ExecutedQuantity`
, COALESCE(`GetScheduledQuantity_old`(`s`.`Id`,2048),0) AS `ScheduledQuantity`
, EXISTS(SELECT `program`.`Id` FROM `program`WHERE `program`.`StockEntityId` = `s`.`Id` LIMIT 1) AS `IsInProgram`
,`s`.`CreatedOn` AS `CreatedOn`
FROM 
`stockentity` `s`
INNER JOIN `quantitybacklog_old` `qb` ON `qb`.`EntityId` = `s`.`Id` AND qb.EntityTypeId = 2048
LEFT JOIN `program` `p` ON `p`.`StockEntityId` = `s`.`Id`
WHERE `qb`.`EntityTypeId` = 2048;
			
CREATE OR REPLACE VIEW attributevaluesview_old
AS
WITH QuickAccess AS (
SELECT `tta`.`AttributeScopeId` AS `AttributeScopeId`
,`tta`.`AttributeDefinitionId` AS `AttributeDefinitionId`,`tta`.`ToolTypeId` AS `SubParentTypeId`,`tta`.`Priority` AS `Priority`,`tta`.`ParentTypeId` AS `ParentTypeId`,`tta`.`ProcessingTechnology` AS `ProcessingTechnology`,`tta`.`IsCodeGenerator` AS `IsCodeGenerator`,0 AS `AttributeDefinitionGroupId`,0 AS `IsSubFilter`,0 AS `attributedefinitiongrouppriority`,`ad`.`HelpImage` AS `HelpImage`
FROM `tooltypeattribute` `tta`
INNER JOIN `attributedefinition_old` `ad` ON `ad`.`Id` = `tta`.`AttributeDefinitionId` AND `ad`.`ParentTypeId` = `tta`.`ParentTypeId` 
UNION
SELECT `pta`.`AttributeScopeId` AS `AttributeScopeId`,`pta`.`AttributeDefinitionId` AS `AttributeDefinitionId`,
`pta`.`ProfileTypeId` AS `SubParentTypeId`,`pta`.`Priority` AS `Priority`,`pta`.`ParentTypeId` AS `ParentTypeId`,1 AS `ProcessingTechnology`,0 AS `IsCodeGenerator`,0 AS `AttributeDefinitionGroupId`,0 AS `attributedefinitiongrouppriority`,0 AS `IsSubFilter`,`ad`.`HelpImage` AS `HelpImage`
FROM `profiletypeattribute` `pta`
INNER JOIN `attributedefinition_old` `ad` ON `ad`.`Id` = `pta`.`AttributeDefinitionId` 
UNION
SELECT `ota`.`AttributeScopeId` AS `AttributeScopeId`,`ota`.`AttributeDefinitionId` AS `AttributeDefinitionId`,`ota`.`OperationTypeId` AS `SubParentTypeId`,`ota`.`Priority` AS `Priority`,4096 AS `ParentTypeId`,1 AS `ProcessingTechnology`,`ota`.`IsCodeGenerator` AS `IsCodeGenerator`,`ota`.`AttributeDefinitionGroupId` AS `AttributeDefinitionGroupId`,`ota`.`IsSubFilter` AS `IsSubFilter`, COALESCE(`adgp`.`Priority`,0) AS `attributedefinitiongrouppriority`,'' AS `HelpImage`
FROM `operationtypeattribute` `ota`
LEFT JOIN `attributedefinitiongrouppriority_old` `adgp` ON `adgp`.`AttributeDefinitionGroupId` = `ota`.`AttributeDefinitionGroupId` AND `adgp`.`ParentTypeId` = 4096 AND `adgp`.`SubParentTypeId` = `ota`.`OperationTypeId`
INNER JOIN `attributedefinition_old` `ad` ON `ad`.`Id` = `ota`.`AttributeDefinitionId` 
UNION
SELECT 0 AS `IsQuickAccess`,`attributedefinition_old`.`Id` AS `AttributeDefinitionId`,0 AS `SubParentTypeId`,`attributedefinition_old`.`Priority` AS `Priority`,`attributedefinition_old`.`ParentTypeId` AS `ParentTypeId`,1 AS `ProcessingTechnology`,0 AS `IsCodeGenerator`,`attributedefinition_old`.`GroupId` AS `AttributeDefinitionGroupId`,0 AS `IsSubFilter`,0 AS `attributedefinitiongrouppriority`,`attributedefinition_old`.`HelpImage` AS `HelpImage`
FROM `attributedefinition_old`)
SELECT `av`.`ParentId` AS `ParentId`,`av`.`ParentTypeId` AS `ParentTypeId`,`av`.`SubParentTypeId` AS `SubParentTypeId`,`av`.`Id` AS `Id`, CONCAT('LBL_ATTR_', UCASE(`ad`.`Code`)) AS `LocalizationKey`,`av`.`Value` AS `Value`,`av`.`TextValue` AS `TextValue`,`ad`.`DisplayName` AS `DisplayName`,`av`.`AttributeDefinitionId` AS `AttributeDefinitionId`,`ad`.`AttributeTypeId` AS `AttributeTypeId`,`ad`.`AttributeKindId` AS `AttributeKindId`,`ad`.`EnumId` AS `EnumId`,`av`.`DataFormatId` AS `DataFormatId`,`ad`.`ControlTypeId` AS `ControlTypeId`,`ad`.`ProtectionLevel` AS `ProtectionLevel`,`qa`.`Priority` AS `Priority`,`ad`.`Priority` AS `DefaultPriority`,`qa`.`ProcessingTechnology` AS `ProcessingTechnology`,`qa`.`IsCodeGenerator` AS `IsCodeGenerator`,`qa`.`AttributeScopeId` AS `AttributeScopeId`, CASE `qa`.`AttributeDefinitionGroupId` WHEN 0 THEN `ad`.`GroupId` ELSE `qa`.`AttributeDefinitionGroupId` END AS `AttributeDefinitionGroupId`,`qa`.`IsSubFilter` AS `IsSubFilter`,`ad`.`TypeName` AS `EnumType`, CASE `qa`.`attributedefinitiongrouppriority` WHEN 0 THEN `qa`.`AttributeDefinitionGroupId` ELSE `qa`.`attributedefinitiongrouppriority` END AS `attributedefinitiongrouppriority`,`qa`.`HelpImage` AS `HelpImage`
FROM `attributevalue_old` `av`
INNER JOIN `attributedefinition_old` `ad` ON `av`.`AttributeDefinitionId` = `ad`.`Id` AND `ad`.`ParentTypeId` = `av`.`ParentTypeId`
INNER JOIN `quickaccess` `qa` ON `qa`.`ParentTypeId` = `av`.`ParentTypeId` AND `av`.`AttributeDefinitionId` = `qa`.`AttributeDefinitionId` AND `av`.`SubParentTypeId` = `qa`.`SubParentTypeId`
WHERE `ad`.`AttributeTypeId` in (1,2,8);
			

			
COMMIT;	  



