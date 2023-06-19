USE machine;

START TRANSACTION;

RENAME TABLE IF EXISTS attributedefinitionView TO attributedefinitionView_OLD;
RENAME TABLE IF EXISTS detailidentifiersview TO detailidentifiersview_OLD;
RENAME TABLE IF EXISTS toollifeattributesview TO toollifeattributesview_OLD;
RENAME TABLE IF EXISTS ToolStatusAttributesView TO ToolStatusAttributesView_OLD;
RENAME TABLE IF EXISTS toolrangeminrequiredconsoleview TO toolrangeminrequiredconsoleview_OLD;

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

CREATE OR REPLACE VIEW migratedtypes AS
SELECT mt.Id, mt.EntityTypeId, et.DisplayName AS EntityType, mt.ParentTypeId
FROM migratedentity mt
INNER JOIN entitytype et ON et.Id = mt.EntityTypeId;

SELECT `av`.`Id` AS `Id`,`av`.EntityId,`av`.`DataFormatId` AS `DataFormatId`
	,`av`.`Priority`,`av`.`Value`,`av`.`TextValue`
	,`av`.`AttributeDefinitionLinkId`,`ad`.`EnumId`
	,`ad`.`DisplayName` AS `DisplayName`,`ad`.`AttributeType`
	,`adl`.`ControlType`,`ad`.`AttributeKind`
	,`adl`.`ProtectionLevel` ,`adl`.`GroupId` AS `GroupId`, 1 AS PlantUnitId
FROM attributevalue av
INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
INNER JOIN attributedefinition `ad` ON `ad`.`Id` = adl.AttributeDefinitionId
WHERE `ad`.`DisplayName` in ('MaxToolLife','ToolLife','WarningToolLife','ToolLength','AutoSensitiveEnable'
		,'ToolEnableA','ToolEnableB','ToolEnableC','ToolEnableD')
UNION		
SELECT `av`.`Id` AS `Id`,`av`.EntityId,`av`.`DataFormatId` AS `DataFormatId`
	,`av`.`Priority`,`av`.`Value`,`av`.`TextValue`
	,`av`.`AttributeDefinitionLinkId`,`ad`.`EnumId`
	,`ad`.`DisplayName` AS `DisplayName`,`ad`.`AttributeType`
	,`adl`.`ControlType`,`ad`.`AttributeKind`
	,`adl`.`ProtectionLevel` ,`adl`.`GroupId` AS `GroupId`, 2 AS PlantUnitId
FROM attributevalue av
INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
INNER JOIN attributedefinition `ad` ON `ad`.`Id` = adl.AttributeDefinitionId
WHERE `ad`.`DisplayName` in ('NozzleLifeMaxIgnitions','NozzleLifeIgnitions','NozzleLifeWarningLimitIgnitions','ToolEnableC','ToolEnableD')
UNION
SELECT `av`.`Id` AS `Id`,`av`.EntityId,`av`.`DataFormatId` AS `DataFormatId`
	,`av`.`Priority`,`av`.`Value`,`av`.`TextValue`
	,`av`.`AttributeDefinitionLinkId`,`ad`.`EnumId`
	,`ad`.`DisplayName` AS `DisplayName`,`ad`.`AttributeType`
	,`adl`.`ControlType`,`ad`.`AttributeKind`
	,`adl`.`ProtectionLevel` ,`adl`.`GroupId` AS `GroupId`, 512 AS PlantUnitId
FROM attributevalue av
INNER JOIN attributedefinitionlink adl ON adl.Id = av.AttributeDefinitionLinkId
INNER JOIN attributedefinition `ad` ON `ad`.`Id` = adl.AttributeDefinitionId
WHERE `ad`.`DisplayName` in ('BladeLife','MaxBladeLife','WarningBladeLife');

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

COMMIT;	  



