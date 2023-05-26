use machine;

DELIMITER //
CREATE  TRIGGER IF NOT EXISTS trgAttributeValueBEFOREUpdate
BEFORE UPDATE
ON _attributeValue FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
	UPDATE masterentity SET RowVersion = uuid() WHERE Id = new.EntityId;
END //

CREATE  TRIGGER IF NOT EXISTS trgAttributeValueBEFOREInsert
BEFORE INSERT
ON _attributeValue FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
	UPDATE masterentity SET RowVersion = uuid() WHERE Id = new.EntityId;
END //

CREATE  TRIGGER IF NOT EXISTS trgEntityBEFOREUpdate
BEFORE UPDATE
ON entity FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
END //

CREATE  TRIGGER IF NOT EXISTS trgEntityBEFOREInsert
BEFORE INSERT
ON entity FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
END //

DELIMITER ;