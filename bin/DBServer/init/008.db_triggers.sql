use _machine;

DELIMITER //
CREATE TRIGGER IF NOT EXISTS trgAttributeValueBEFOREUpdate
BEFORE UPDATE
ON attributevalue FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
	UPDATE entity SET RowVersion = uuid() WHERE Id = new.EntityId;
END //

CREATE TRIGGER IF NOT EXISTS trgEntityBEFOREInsert
BEFORE INSERT
ON entity FOR EACH ROW
BEGIN
	SET new.Status = 'Available';
END //

CREATE TRIGGER IF NOT EXISTS trgEntityBEFOREUpdate
BEFORE UPDATE
ON entity FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
END //


DELIMITER ;