USE machine;

DELIMITER //

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