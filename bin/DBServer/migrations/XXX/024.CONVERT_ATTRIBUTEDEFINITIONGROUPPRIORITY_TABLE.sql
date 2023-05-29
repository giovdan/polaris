USE machine;

INSERT INTO _attributedefinitiongrouppriority
(EntityTypeId, AttributeDefinitionGroupId, Priority, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
SELECT GetEntityType(ParentTypeId, SubParentTypeId, 1) AS EntityTypeId, agp.Priority, agp.AttributeDefinitionGroupId 
		, agp.CreatedBy, agp.CreatedOn, agp.UpdatedBy, agp.UpdatedOn
FROM attributedefinitiongrouppriority agp;