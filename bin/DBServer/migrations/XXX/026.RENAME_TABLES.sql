USE machine;

RENAME TABLE attributedefinition TO attributedefinition_old;
RENAME TABLE attributevalue TO attributevalue_old;
RENAME TABLE attributedefinitiongrouppriority TO attributedefinitiongrouppriority_old;
RENAME TABLE attributeoverridevalue TO attributeoverridevalue_old;
RENAME TABLE detailidentifier TO detailidentifier_old;

RENAME TABLE _attributedefinition TO attributedefinition;
RENAME TABLE _attributevalue TO attributevalue;
RENAME TABLE _attributedefinitiongrouppriority TO attributedefinitiongrouppriority;
RENAME TABLE _attributeoverridevalue TO attributeoverridevalue;
RENAME TABLE _detailidentifier TO detailidentifier;



