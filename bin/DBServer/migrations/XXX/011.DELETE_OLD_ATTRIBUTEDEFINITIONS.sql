USE machine;
DELETE FROM attributevalue_old WHERE ParentTypeId = 2 AND attributedefinitionId IN (SELECT ID FROM attributedefinition
WHERE DisplayName IN ('RevolutionSpeed', 'FeedRateSpeed'
, 'StartFeedRateSpeed', 'CountersinkRevolutionSpeed', 'CountersinkFeedRateSpeed')
AND ParentTypeId = 2);

DELETE FROM attributedefinition_old 
WHERE DisplayName IN ('RevolutionSpeed', 'FeedRateSpeed'
, 'StartFeedRateSpeed', 'CountersinkRevolutionSpeed', 'CountersinkFeedRateSpeed')
AND ParentTypeId = 2;