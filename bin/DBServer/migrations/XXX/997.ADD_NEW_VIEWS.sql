CREATE OR REPLACE VIEW EntityLinks
AS
SELECT e.DisplayName AS RelatedEntityDisplayName, ec.DisplayName AS EntityDisplayName
, e.Id AS RelatedEntityId, ec.Id as EntityId
, el.RelatedEntityHashCode, el.EntityHashCode
, el.RelationType, el.RowNumber, el.`Level` FROM entitylink el
INNER JOIN entity e ON e.HashCode = el.RelatedEntityHashCode
INNER JOIN entity ec ON ec.HashCode = el.EntityHashCode;


