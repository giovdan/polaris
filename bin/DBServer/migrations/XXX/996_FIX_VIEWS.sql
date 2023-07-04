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
		
RENAME TABLE IF EXISTS attributedefinitionView TO attributedefinitionView_OLD;
RENAME TABLE IF EXISTS detailidentifiersview TO detailidentifiersview_OLD;
RENAME TABLE IF EXISTS toollifeattributesview TO toollifeattributesview_OLD;
RENAME TABLE IF EXISTS ToolStatusAttributesView TO ToolStatusAttributesView_OLD;
RENAME TABLE IF EXISTS toolrangeminrequiredconsoleview TO toolrangeminrequiredconsoleview_OLD;
RENAME TABLE IF EXISTS plasmatoolmasters TO plasmatoolmasters_old;
RENAME TABLE IF EXISTS tootypeattributedefinitionsview TO tootypeattributedefinitionsview_old;

CREATE OR REPLACE VIEW tootypeattributedefinitionsview_old
AS
SELECT `ad`.`Id` AS `Id`,`ad`.`EnumId` AS `EnumId`,`ad`.`Code` AS `Code`,`ad`.`DisplayName` AS `DisplayName`,`ad`.`AttributeTypeId` AS `AttributeTypeId`,`ad`.`AttributeKindId` AS `AttributeKindId`,`ad`.`DataFormatId` AS `DataFormatId`,`ad`.`ControlTypeId` AS `ControlTypeId`,`ad`.`OverrideTypeId` AS `OverrideTypeId`,`ad`.`TypeName` AS `TypeName`,`ad`.`GroupId` AS `GroupId`,`ad`.`HelpImage` AS `HelpImage`,`ad`.`ProtectionLevel` AS `ProtectionLevel`,`tta`.`ToolTypeId` AS `ToolTypeId`,`tta`.`ParentTypeId` AS `ParentTypeId`,`tta`.`ProcessingTechnology` AS `ProcessingTechnology`,`tta`.`Priority` AS `Priority`,`tta`.`UseLastInsertedAsDefault` AS `UseLastInsertedAsDefault`,`tta`.`LastInsertedValue` AS `LastInsertedValue`,`tta`.`LastInsertedTextValue` AS `LastInsertedTextValue`
FROM (`attributedefinition_old` `ad`
JOIN `tooltypeattribute` `tta` ON(`tta`.`AttributeDefinitionId` = `ad`.`Id` AND `tta`.`ParentTypeId` = `ad`.`ParentTypeId`))
WHERE `ad`.`ControlTypeId` <> 1
ORDER BY `tta`.`Priority`

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

				
COMMIT;	  



