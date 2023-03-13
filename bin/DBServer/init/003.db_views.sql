USE _machine;

CREATE OR REPLACE VIEW attributedefinitionView 
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, adl.ControlType, adl.AttributeKind
, adl.DataFormat, adl.GroupId, adl.Priority, ad.DisplayName, ad.HelpImage, ad.AttributeType,ad.TypeName, ad.OverrideType
, ad.Owner, ad.RowVersion
FROM attributedefinition ad
INNER JOIN attributedefinitionlink adl ON adl.AttributeDefinitionId = ad.Id;