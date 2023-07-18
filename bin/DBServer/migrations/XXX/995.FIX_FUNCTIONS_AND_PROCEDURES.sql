USE machine;

DELIMITER //

UPDATE mysql.proc SET name = 'GetScheduledQuantity_old', specific_name = 'GetScheduledQuantity_old' WHERE name = 'GetScheduledQuantity' //
UPDATE mysql.proc SET name = 'GetAttributeValue_old', specific_name = 'GetAttributeValue_old' WHERE name = 'GetAttributeValue';

CREATE OR REPLACE FUNCTION GetAttributeValue_old(displayName VARCHAR(32), parentId INT, parentTypeId INT)
RETURNS VARCHAR(50) 
BEGIN
	DECLARE attributeDefinitionId BIGINT;
	DECLARE attributeStringValue VARCHAR(50) DEFAULT ('0');
	DECLARE attributeKindId INT;
	DECLARE controlTypeId INT;
	
	SELECT Id, ad.AttributeKindId, ad.ControlTypeId INTO attributeDefinitionId, attributeKindId, controlTypeId 
		FROM attributedefinition_old ad
		WHERE
		ad.DisplayName = displayName AND ad.ParentTypeId = parentTypeId;
	
	IF attributeKindId = 2 OR controlTypeId = 16 THEN
		SELECT 
			COALESCE(av.TextValue, '')
			INTO attributeStringValue
		FROM attributevalue_old av
			WHERE av.ParentId = parentId	
			AND av.ParentTypeId = parentTypeId AND av.AttributeDefinitionId = attributeDefinitionId;	
	ELSE
		SELECT 
			COALESCE(av.Value, '0')
			INTO attributeStringValue
		FROM attributevalue_old av
			WHERE av.ParentId = parentId	AND av.ParentTypeId = parentTypeId AND av.AttributeDefinitionId = attributeDefinitionId;	
	END IF;		
	
	IF attributeKindId = 2 OR controlTypeId = 16 AND attributeStringValue = '0' THEN
		SET attributeStringValue = '';
	END IF;
	
	RETURN attributeStringValue;
END //

CREATE OR REPLACE FUNCTION `GetScheduledQuantity_old`(
	entityId INT,
	entityTypeId INT
	)
RETURNS INT DETERMINISTIC
BEGIN
	DECLARE scheduledQuantity INT;
	DECLARE repetitionAttributeDefinitionId INT;
	
	SET scheduledQuantity = 0;
	SET repetitionAttributeDefinitionId = (SELECT Id FROM attributedefinition_old WHERE ParentTypeId = 512 AND DisplayName = 'RepetitionsNumber');
	
	IF entityTypeId = 256 THEN
		SET scheduledQuantity = (SELECT SUM(TotalQuantity) - SUM(ExecutedQuantity) FROM productionlistview WHERE ProgramId = entityId);	
	END IF;
	
	IF entityTypeId = 1024 THEN
		CREATE TEMPORARY TABLE IF NOT EXISTS ProgramPieceQuantities (Id INT, ProgramId INT, Repetitions INT, TotalQuantity INT);
		
		INSERT INTO ProgramPieceQuantities
		(Id, ProgramId, Repetitions, TotalQuantity)
		SELECT ppl.Id,  ppl.ProgramId, COALESCE(av.Value,1) AS Repetitions, qb.TotalQuantity
			FROM programpiecelink ppl 
			INNER JOIN quantitybacklog qb ON qb.EntityId = ppl.ProgramId AND qb.EntityTypeId = 256
			LEFT JOIN attributevalue_old av ON av.ParentId = ppl.Id AND av.ParentTypeId = 512 AND av.AttributeDefinitionId = repetitionAttributeDefinitionId
		WHERE ppl.pieceId = entityId;
		
		SET @repetitions = 0;
		SET @totalQuantity = 0;
		SET @programId = 0;
		SET @id = 0;
		WHILE EXISTS(SELECT * FROM ProgramPieceQuantities) DO
			SELECT Id, Repetitions,  TotalQuantity, ProgramId 
				INTO @id, @repetitions, @totalQuantity, @programId
			FROM ProgramPieceQuantities LIMIT 1;
			
			SET scheduledQuantity = scheduledQuantity + (@repetitions * @totalQuantity);
			
			DELETE FROM ProgramPieceQuantities WHERE Id = @id;
		END WHILE;

		DROP TEMPORARY TABLE IF EXISTS ProgramPieceQuantities; 
	END IF;
	
	RETURN scheduledQuantity;	
END //

CREATE OR REPLACE FUNCTION GetScheduledQuantity(iEntityId INT)
RETURNS INT
BEGIN
	DECLARE scheduledQuantity INT;
	DECLARE repetitionAttributeDefinitionId INT;
	
	SET scheduledQuantity = 0;
	SET repetitionAttributeDefinitionId = (SELECT Id FROM attributedefinition WHERE DisplayName = 'RepetitionsNumber');
	
	
	RETURN scheduledQuantity;
END //

CREATE OR REPLACE FUNCTION GetCodeFromToolMasterId(toolMasterId INT, toolTypeId INT)
RETURNS VARCHAR(200)
BEGIN
	DECLARE toolCode VARCHAR(200) DEFAULT('');
	DECLARE done INT DEFAULT FALSE;
	DECLARE detailValue VARCHAR(50);
    
	DECLARE curDetails CURSOR FOR 
		SELECT `Value` FROM detailidentifier_old WHERE MasterId = toolMasterId;
    
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	SELECT DISTINCT tt.DisplayName INTO toolCode FROM tooltype tt WHERE tt.Id = toolTypeId;
        
	OPEN curDetails;
	loop_values: LOOP
		FETCH curDetails INTO detailValue;
		IF done THEN
			LEAVE loop_values;
		END IF;	
		
		IF detailValue <> '' THEN
			IF REGEXP_INSTR(detailValue, '^[0-9]+\\.?[0-9]*$') THEN
				SET toolCode = CONCAT(toolCode, '-', FORMAT(detailValue,2));
			ELSE
				SET toolCode = CONCAT(toolCode, '-', detailValue);			
			END IF;	
		END IF;		
	END LOOP;
	CLOSE curDetails;
	
	RETURN toolCode;
END //

CREATE OR REPLACE FUNCTION GetDisplayValueFromToolMasterId(toolMasterId INT, toolTypeId INT, parentId INT)
RETURNS VARCHAR(200)
BEGIN
	DECLARE displayValue VARCHAR(200) DEFAULT('');
	DECLARE done INT DEFAULT FALSE;
	DECLARE detailValue VARCHAR(50);
    DECLARE displayValuePrefix VARCHAR(10) DEFAULT '';
	DECLARE curDetails CURSOR FOR 
	
	SELECT `Value` FROM detailidentifier_old WHERE MasterId = toolMasterId;
    
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	SELECT DISTINCT InternalCode INTO displayValue FROM tooltype 
		WHERE Id = toolTypeId;
    
	IF COALESCE(parentId,0) <> 0 THEN
		SET displayValuePrefix = CONCAT(parentId, '_');
	END IF;     
	
	OPEN curDetails;
	loop_values: LOOP
		FETCH curDetails INTO detailValue;
		IF done THEN
			LEAVE loop_values;
		END IF;	
		
		IF detailValue <> '' AND UPPER(detailValue) <> 'BEVEL' THEN
			IF REGEXP_INSTR(detailValue, '^[0-9]+\\.?[0-9]*$') THEN
				IF CEIL(detailValue) = detailValue THEN
					SET displayValue = CONCAT(displayValue, ' - ', CEIL(detailValue));			
				ELSE
					SET displayValue = CONCAT(displayValue, ' - ', FORMAT(detailValue,2));
				END IF;	
			ELSE
				SET displayValue = CONCAT(displayValue, ' - ', detailValue);			
			END IF;	
		END IF;	
	END LOOP;
	CLOSE curDetails;
	
	RETURN UPPER(CONCAT(displayValuePrefix, displayValue));
END //



DELIMITER ;