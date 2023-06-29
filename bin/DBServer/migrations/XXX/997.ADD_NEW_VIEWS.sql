CREATE OR REPLACE VIEW EntityLinks
AS
SELECT e.DisplayName AS RelatedEntityDisplayName, ec.DisplayName AS EntityDisplayName
, e.Id AS RelatedEntityId, ec.Id as EntityId
, el.RelatedEntityHashCode, el.EntityHashCode
, el.RelationType, el.RowNumber, el.`Level` FROM entitylink el
INNER JOIN entity e ON e.HashCode = el.RelatedEntityHashCode
INNER JOIN entity ec ON ec.HashCode = el.EntityHashCode;


CREATE OR REPLACE VIEW attributedefinitions
AS
SELECT adl.Id, adl.EntityTypeId, adl.AttributeDefinitionId, ad.DisplayName
, ad.EnumId, ad.DataFormat, ad.AttributeType, ad.AttributeKind, ad.TypeName
, ad.OverrideType, adl.ControlType, adl.HelpImage, adl.IsCodeGenerator, adl.IsSubFilter
, adl.AttributeScopeId, adl.DefaultBehavior, adl.LastInsertedValue, adl.LastInsertedTextValue
, adl.ProtectionLevel, adl.GroupId, adl.Priority
  FROM attributedefinitionlink adl
INNER JOIN attributedefinition ad ON ad.Id = adl.AttributeDefinitionId;




