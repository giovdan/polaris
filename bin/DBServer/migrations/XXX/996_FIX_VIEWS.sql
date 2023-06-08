USE machine;

START TRANSACTION;

RENAME TABLE IF EXISTS attributedefinitionView TO attributedefinitionView_OLD;
RENAME TABLE IF EXISTS detailidentifiersview TO detailidentifiersview_OLD;

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
		   ad.EnumId
	  FROM detailidentifier di
	  INNER JOIN attributedefinitionlink adl ON adl.Id = di.AttributeDefinitionLinkId
	  INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId
	  ORDER BY adl.EntityTypeId; 

CREATE OR REPLACE VIEW migratedtypes AS
SELECT mt.Id, mt.EntityTypeId, et.DisplayName AS EntityType, mt.ParentTypeId
FROM migratedentity mt
INNER JOIN entitytype et ON et.Id = mt.EntityTypeId;

COMMIT;	  



