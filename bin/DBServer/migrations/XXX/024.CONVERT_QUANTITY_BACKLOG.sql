USE machine;

INSERT INTO `quantitybacklog` 
(`EntityId`, `TotalQuantity`, `ExecutedQuantity`, `QuantityTobeLoaded`, `QuantityLoaded`
	, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) 
SELECT me.EntityId, qbo.TotalQuantity, qbo.ExecutedQuantity, qbo.QuantityTobeLoaded, qbo.QuantityLoaded
, qbo.CreatedBy, qbo.CreatedOn, qbo.UpdatedBy, qbo.UpdatedOn, qbo.TimeZoneId
FROM quantitybacklog_old qbo 
INNER JOIN migratedentity me ON me.ParentId = qbo.EntityId AND me.ParentTypeId = qbo.EntityTypeId;



