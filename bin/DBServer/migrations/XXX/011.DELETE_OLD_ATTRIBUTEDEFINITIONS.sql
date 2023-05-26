USE machine;
DELETE FROM attributevalue WHERE ParentTypeId = 2 AND attributedefinitionId IN (SELECT ID FROM attributedefinition
WHERE DisplayName IN ('RevolutionSpeed', 'FeedRateSpeed'
, 'StartFeedRateSpeed', 'CountersinkRevolutionSpeed', 'CountersinkFeedRateSpeed')
AND ParentTypeId = 2);

DELETE FROM attributedefinition 
WHERE DisplayName IN ('RevolutionSpeed', 'FeedRateSpeed'
, 'StartFeedRateSpeed', 'CountersinkRevolutionSpeed', 'CountersinkFeedRateSpeed')
AND ParentTypeId = 2;