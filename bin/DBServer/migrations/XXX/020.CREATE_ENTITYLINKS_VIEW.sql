USE machine;

CREATE OR REPLACE VIEW entitylinksview
AS
SELECT er.DisplayName AS RelatedEntityDisplayName, er.Id AS RelatedEntityId
 , er.`Status` as RelatedEntityStatus, er.EntityTypeId as RelatedEntityTypeId
 , e.DisplayName, e.Id AS EntityId, e.`Status` AS EntityStatus, e.EntityTypeId
 , el.RelationType, el.RowNumber, el.`Level` 
 FROM entitylink el
INNER JOIN entity er ON er.HashCode = el.RelatedEntityHashCode
INNER JOIN entity e ON e.HashCode = el.EntityHashCode
ORDER BY er.Id, el.RowNumber;