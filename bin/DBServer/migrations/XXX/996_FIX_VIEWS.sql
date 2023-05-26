USE machine;

CREATE OR REPLACE VIEW attributedefinitionView 
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, ad.DisplayName
, adl.HelpImage, ad.AttributeType,ad.TypeName
, ad.OverrideType, adl.ControlType, ad.AttributeKind
, ad.DataFormat, adl.ProtectionLevel, adl.GroupId, adl.Priority 
FROM attributedefinition_new ad
INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = ad.Id;


CREATE OR REPLACE VIEW migratedtypes AS
SELECT mt.Id, mt.EntityTypeId, et.DisplayName AS EntityType, mt.ParentTypeId, mt.SubParentTypeId, mt.ProcessingTechnology  
FROM migratedtype mt
INNER JOIN entitytype et ON et.Id = mt.EntityTypeId;

