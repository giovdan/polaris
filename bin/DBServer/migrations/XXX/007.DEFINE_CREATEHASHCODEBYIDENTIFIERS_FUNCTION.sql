USE machine;

DELIMITER //

CREATE OR REPLACE FUNCTION CreateHashCodeByAttributeValues(iEntityTypeId INT, iParentId INT, iParentTypeId INT)
RETURNS CHAR(64)
BEGIN
	DECLARE pStringToHash TEXT DEFAULT '';
	DECLARE pValue DECIMAL(13,2);
	DECLARE pTextValue TEXT;
	DECLARE pAttributeKind INT;
	DECLARE done BOOLEAN DEFAULT FALSE;
    DECLARE pContext TEXT DEFAULT CONCAT('Errore dichiarazione cursore ', iParentId,',', iParentTypeId);
	
	DECLARE curAttributeValues CURSOR FOR 	
		SELECT av.Value, av.TextValue, ad.AttributeKindId
		FROM attributevalue av 
			INNER JOIN attributedefinition ad ON ad.Id = av.AttributeDefinitionId AND ad.ParentTypeId = av.ParentTypeId
		WHERE av.ParentId = iParentId AND av.ParentTypeId = iParentTypeId;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	OPEN curAttributeValues;
	
	SET pContext = CONCAT('Errore inizializzazione stringa da codificare ', iParentId,',', iParentTypeId);
	SET pStringToHash = CONCAT(iParentId,iParentTypeId,iEntityTypeId);	
	
loop_values: LOOP
		FETCH curAttributeValues INTO pValue, pTextValue, pAttributeKind;	
		
		IF done THEN
			LEAVE loop_values;		
		END IF;
	
		SET pContext = CONCAT('Errore assegnazione valore ', iParentId,',', iParentTypeId,',',pAttributeKind);
		IF pAttributeKind = 2 OR pAttributeKind = 4 THEN
			SET pStringToHash = CONCAT(pStringToHash,pTextValue);
		ELSE
			SET pStringToHash = CONCAT(pStringToHash,pValue);
		END IF;		
	END LOOP;
	
	CLOSE curAttributeValues;
	
	RETURN SHA2(pStringToHash,256);

END //


CREATE OR REPLACE FUNCTION CreateHashCodeByIdentifiers(iEntityTypeId INT, iMasterId INT, iParentMasterId INT)
RETURNS CHAR(64)
BEGIN
	DECLARE pStringToHash TEXT DEFAULT ('');
	DECLARE pValue VARCHAR(4000);
	DECLARE done BOOLEAN DEFAULT(FALSE);
    
	DECLARE curIdentifiers CURSOR FOR 	
	SELECT di.Value FROM detailidentifier di WHERE di.MasterId = iMasterId;
	
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'CreateHashCodeByIdentifiers ERROR';
	END;
	
	OPEN curIdentifiers;
	
	SET pStringToHash = CONCAT(iMasterId, iEntityTypeId);

	IF iParentMasterId > 0 THEN	
		SET pStringToHash = CONCAT(pStringToHash, iParentMasterId);
	END IF;
	
loop_identifiers: LOOP
		FETCH curIdentifiers INTO pValue;	
		
		IF done THEN
			LEAVE loop_identifiers;		
		END IF;
		
		SET pStringToHash = CONCAT(pStringToHash,pValue);
	END LOOP;
	
	CLOSE curIdentifiers;
	
	RETURN SHA2(pStringToHash,256);

END //

DELIMITER ;