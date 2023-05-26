CREATE OR REPLACE VIEW _attributedefinitions
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, ad.DisplayName
, ad.EnumId, ad.DataFormat, ad.AttributeType, ad.AttributeKind, ad.TypeName
, ad.OverrideType, adl.ControlType, adl.HelpImage, adl.IsCodeGenerator, adl.IsSubFilter
, adl.AttributeScopeId, adl.DefaultBehavior, adl.LastInsertedValue, adl.LastInsertedTextValue
, adl.ProtectionLevel, adl.GroupId, adl.Priority
  FROM attributedefinitionlink adl
INNER JOIN _attributedefinition ad ON ad.Id = adl.AttributeDefinitionId;





