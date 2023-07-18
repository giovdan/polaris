USE machine;

UPDATE attributedefinition_old ad
INNER JOIN profiletypeattribute pta ON ad.Id = pta.AttributeDefinitionId AND ad.ParentTypeId = pta.ParentTypeId
SET pta.AttributeDefinitionGroupId = ad.GroupId
WHERE ad.ParentTypeId = 2048;