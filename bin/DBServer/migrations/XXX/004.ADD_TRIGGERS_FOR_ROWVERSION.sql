use machine;

DELIMITER //
CREATE OR REPLACE TRIGGER  trgAttributeValueBEFOREUpdate
BEFORE UPDATE
ON attributevalue FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
	UPDATE entity SET RowVersion = uuid() WHERE Id = new.EntityId;
END //

CREATE OR REPLACE TRIGGER trgAttributeValueBEFOREInsert
BEFORE INSERT
ON attributevalue FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
	UPDATE entity SET RowVersion = uuid() WHERE Id = new.EntityId;
END //

CREATE OR REPLACE TRIGGER trgEntityBEFOREUpdate
BEFORE UPDATE
ON entity FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
END //

CREATE OR REPLACE TRIGGER trgEntityBEFOREInsert
BEFORE INSERT
ON entity FOR EACH ROW
BEGIN
	SET new.RowVersion = uuid();
END //

DELIMITER ;