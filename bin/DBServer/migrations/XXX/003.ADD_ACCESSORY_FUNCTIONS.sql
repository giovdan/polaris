USE machine;

DELIMITER //

CREATE OR REPLACE FUNCTION `GetEntityType`(
	`pParentTypeId` INT,
	`pSubParentTypeId` INT,
	`pProcessingTechnology` INT
)
RETURNS INT(11)
BEGIN
	DECLARE pEntityTypeId INT;

	SET pEntityTypeId = 
	CASE 
		WHEN pParentTypeId = 1 THEN pSubParentTypeId
		WHEN pParentTypeId = 2 AND pProcessingTechnology = 1 THEN pSubParentTypeId
		WHEN pParentTypeId = 2 AND pProcessingTechnology = 2 AND pSubParentTypeId = 51 THEN 91 # ToolTS51HPR = 91
		WHEN pParentTypeId = 2 AND pProcessingTechnology = 4 AND pSubParentTypeId = 51 THEN 90 # ToolTS51XPR = 90
		WHEN pParentTypeId = 2 AND pProcessingTechnology = 2 AND pSubParentTypeId = 53 THEN 93 # ToolTS53HPR = 93
		WHEN pParentTypeId = 2 AND pProcessingTechnology = 4 AND pSubParentTypeId = 53 THEN 92 # ToolTS53XPR = 92	
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 32 THEN 132
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 33 THEN 133
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 34 THEN 134
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 35 THEN 135
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 36 THEN 136
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 38 THEN 138
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 39 THEN 139
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 40 THEN 140
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 41 THEN 141
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 51 AND pProcessingTechnology = 1 THEN 151
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 52 AND pProcessingTechnology = 1 THEN 152
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 53 AND pProcessingTechnology = 1 THEN 153
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 54 AND pProcessingTechnology = 1 THEN 154
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 51 AND pProcessingTechnology = 2 THEN 180
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 53 AND pProcessingTechnology = 2 THEN 182
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 51 AND pProcessingTechnology = 4 THEN 181
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 53 AND pProcessingTechnology = 4 THEN 183
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 55 THEN 155
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 56 THEN 156
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 57 THEN 157
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 61 THEN 161
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 62 THEN 162
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 68 THEN 168
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 69 THEN 169
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 70 THEN 170
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 71 THEN 171
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 73 THEN 173
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 74 THEN 174
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 75 THEN 175
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 76 THEN 176
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 77 THEN 177
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 78 THEN 178
		WHEN pParentTypeId = 4 AND pSubParentTypeId = 79 THEN 179
		WHEN pParentTypeId = 8 THEN 107		
		WHEN pParentTypeId = 16 AND pSubParentTypeId = 51 THEN 184
		WHEN pParentTypeId = 16 AND pSubParentTypeId = 52 THEN 185
		WHEN pParentTypeId = 16 AND pSubParentTypeId = 53 THEN 186
		WHEN pParentTypeId = 16 AND pSubParentTypeId = 54 THEN 187		
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 51 AND pProcessingTechnology = 1 THEN 188
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 51 AND pProcessingTechnology = 2 THEN 189
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 51 AND pProcessingTechnology = 4 THEN 190
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 53 AND pProcessingTechnology = 1 THEN 191
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 53 AND pProcessingTechnology = 2 THEN 192
		WHEN pParentTypeId = 32 AND pSubParentTypeId = 53 AND pProcessingTechnology = 4 THEN 193
		WHEN pParentTypeId = 64 AND pSubParentTypeId = 51 AND pProcessingTechnology = 1 THEN 194
		WHEN pParentTypeId = 64 AND pSubParentTypeId = 51 AND pProcessingTechnology = 4 THEN 195
		WHEN pParentTypeId = 64 AND pSubParentTypeId = 53 AND pProcessingTechnology = 1 THEN 196
		WHEN pParentTypeId = 64 AND pSubParentTypeId = 53 AND pProcessingTechnology = 4 THEN 197		
		WHEN pParentTypeId = 128 THEN pParentTypeId	
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 1  THEN 198
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 2  THEN 199
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 3  THEN 200
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 4  THEN 201
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 5  THEN 202
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 6  THEN 203
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 7  THEN 204
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 8  THEN 205
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 9  THEN 206
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 11 THEN 207
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 12 THEN 208
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 13 THEN 209
		WHEN pParentTypeId = 256 AND pSubParentTypeId = 14 THEN 210		
		WHEN pParentTypeId = 512 THEN 121
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 1 THEN 108
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 2 THEN 109
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 3 THEN 110
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 4 THEN 111
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 5 THEN 112
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 6 THEN 113
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 7 THEN 114
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 8 THEN 115
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 9 THEN 116
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 11 THEN 117
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 12 THEN 118
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 13 THEN 119
		WHEN pParentTypeId = 1024 AND pSubParentTypeId = 14 THEN 120
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 1 THEN 94
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 2 THEN 95
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 3 THEN 96
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 4 THEN 97
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 5 THEN 98
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 6 THEN 99
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 7 THEN 100
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 8 THEN 101
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 9 THEN 102
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 11 THEN 103
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 12 THEN 104
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 13 THEN 105
		WHEN pParentTypeId = 2048 AND pSubParentTypeId = 14 THEN 106
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 1 THEN 224
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 2 THEN 225
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 3 THEN 226
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 4 THEN 227
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 5 THEN 228
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 6 THEN 229
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 7 THEN 230
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 8 THEN 231
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 9 THEN 232
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 10 THEN 233
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 11 THEN 234
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 12 THEN 235
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 13 THEN 236
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 14 THEN 237
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 16 THEN 238
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 17 THEN 239
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 18 THEN 240
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 19 THEN 241
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 21 THEN 242
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 22 THEN 243
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 23 THEN 244
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 25 THEN 245
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 26 THEN 246
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 27 THEN 247
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 28 THEN 248
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 29 THEN 249
		WHEN pParentTypeId = 4096 AND pSubParentTypeId = 30 THEN 250
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 1  THEN 211
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 2  THEN 212
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 3  THEN 213
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 4  THEN 214
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 5  THEN 215
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 6  THEN 216
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 7  THEN 217
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 8  THEN 218
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 9  THEN 219
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 11 THEN 220
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 12 THEN 221
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 13 THEN 222
		WHEN pParentTypeId = 8192 AND pSubParentTypeId = 14 THEN 223
		WHEN pParentTypeId = 16384 THEN 252
		WHEN pParentTypeId = 65536 AND pSubParentTypeId = 6 THEN 252
		WHEN pParentTypeId = 65536 AND pSubParentTypeId = 22 THEN 253
		WHEN pParentTypeId = 65536 AND pSubParentTypeId = 23 THEN 254
		ELSE 0
	END;	
		
	RETURN pEntityTypeId;
END //

CREATE OR REPLACE FUNCTION `GetEntityTypeFromOperationType`(iOperationTypeId INT, iParentOperationTypeId INT)
RETURNS INT
BEGIN
	DECLARE pEntityTypeId INT DEFAULT 0;
	
	SET pEntityTypeId = 
	CASE
  		WHEN iOperationTypeId = 1 THEN 224	
        WHEN iOperationTypeId = 2 THEN 225	
        WHEN iOperationTypeId = 3 THEN 226	
        WHEN iOperationTypeId = 4 THEN 227	
        WHEN iOperationTypeId = 5 THEN 228	
        WHEN iOperationTypeId = 6 THEN 229	
        WHEN iOperationTypeId = 7 THEN 230	
        WHEN iOperationTypeId = 8 THEN 231	
        WHEN iOperationTypeId = 9 THEN 232	
        WHEN iOperationTypeId = 10 THEN 233	
        WHEN iOperationTypeId = 11 THEN 234	
        WHEN iOperationTypeId = 12 THEN 235	
        WHEN iOperationTypeId = 13 THEN 236	
        WHEN iOperationTypeId = 14 THEN 237	
        WHEN iOperationTypeId = 15 THEN 255	
        WHEN iOperationTypeId = 16 THEN 238	
        WHEN iOperationTypeId = 17 THEN 239	
        WHEN iOperationTypeId = 18 THEN 240	
        WHEN iOperationTypeId = 19 THEN 241	
        WHEN iOperationTypeId = 22 THEN 243	
        WHEN iOperationTypeId = 23 THEN 244	
        WHEN iOperationTypeId = 24 THEN 242	
        WHEN iOperationTypeId = 25 THEN 245	
        WHEN iOperationTypeId = 26 THEN 246	
			WHEN iOperationTypeId = 27 THEN 256
        WHEN iOperationTypeId = 28 THEN 248		
        WHEN iOperationTypeId = 29 THEN 249	
        WHEN iOperationTypeId = 30 THEN 251	
        WHEN iOperationTypeId = 21 AND iParentOperationTypeId = 6 THEN 252
        WHEN iOperationTypeId = 21 AND iParentOperationTypeId = 22 THEN 253
        WHEN iOperationTypeId = 21 AND iParentOperationTypeId = 23 THEN 254
        ELSE 0
	END;	
	RETURN pEntityTypeId;	
END //

CREATE OR REPLACE FUNCTION `GetAttributeScope`(`iAttributeScopeId` INT)
RETURNS VARCHAR(32)
BEGIN
	DECLARE pAttributeScope VARCHAR(32);
	
	SET pAttributeScope = 
		CASE
			WHEN iAttributeScopeId = 0 THEN 'Optional'
			WHEN iAttributeScopeId = 1 THEN 'Fundamental'
			WHEN iAttributeScopeId = 2 THEN 'Preview'
			ELSE ''
		END;
		
	RETURN pAttributeScope;		
END //

CREATE OR REPLACE FUNCTION `GetAttributeKind`(`iAttributeKindId` INT)
RETURNS ENUM ('Number','String','Enum','Bool','Date')
BEGIN
	DECLARE pAttributeKind ENUM ('Number','String','Enum','Bool','Date');
	
	SET pAttributeKind = 
		CASE
			WHEN iAttributeKindId = 1 THEN 'Number'
			WHEN iAttributeKindId = 2 THEN 'String'
			WHEN iAttributeKindId = 4 THEN 'Enum'
			WHEN iAttributeKindId = 8 THEN 'Bool'
			WHEN iAttributeKindId = 16 THEN 'Date'	
			ELSE ''
		END;
	
	RETURN pAttributeKind;
END //

CREATE OR REPLACE FUNCTION `GetAttributeType`(`iAttributeTypeId` INT)
RETURNS ENUM ('Geometric','Process','Identifier','Generic')
BEGIN
	DECLARE pAttributeType ENUM ('Geometric','Process','Identifier','Generic');
	
	SET pAttributeType = 
		CASE
			WHEN iAttributeTypeId = 1 THEN 'Geometric'
			WHEN iAttributeTypeId = 2 THEN 'Process'
			WHEN iAttributeTypeId = 4 THEN 'Identifier'
			WHEN iAttributeTypeId = 8 THEN 'Generic'
			ELSE ''
		END;
		
	RETURN pAttributeType;	
END //

CREATE OR REPLACE FUNCTION `GetOverrideType`(`iOverrideTypeId` INT)
RETURNS ENUM ('None','DeltaValue','DeltaPercentage')
BEGIN
	DECLARE pOverrideType ENUM ('None','DeltaValue','DeltaPercentage');
	
	SET pOverrideType = 
		CASE
			WHEN iOverrideTypeId = 1 THEN 'None'
			WHEN iOverrideTypeId = 2 THEN 'DeltaValue'
			WHEN iOverrideTypeId = 4 THEN 'DeltaPercentage'
			ELSE 'None'
		END;
		
	RETURN pOverrideType;	
END //

CREATE OR REPLACE FUNCTION `GetControlType`(`iControlTypeId` INT)
RETURNS ENUM ('None','Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue')
BEGIN
	DECLARE pControlType VARCHAR(32);

	SET pControlType = 
		CASE
			WHEN iControlTypeId = 1 THEN 'None'
			WHEN iControlTypeId = 2 THEN 'Edit'
			WHEN iControlTypeId = 4 THEN 'Label'
			WHEN iControlTypeId = 8 THEN 'Combo'			
			WHEN iControlTypeId = 16 THEN 'ListBox'
			WHEN iControlTypeId = 32 THEN 'Check'
			WHEN iControlTypeId = 64 THEN 'Override'						
			WHEN iControlTypeId = 128 THEN 'Image'
			WHEN iControlTypeId = 256 THEN 'MultiValue'							
			ELSE ''
		END;
		
	RETURN pControlType;	
END //

CREATE OR REPLACE FUNCTION GetProcessingTechnology(oldId INT, pParentTypeId INT)
RETURNS INT(11)
BEGIN
	DECLARE pProcessingTechnology INT DEFAULT 1;
	
	IF pParentTypeId = 2 OR pParentTypeId = 4 
		OR pParentTypeId = 16 OR pParentTypeId = 32 THEN
		
		SELECT COALESCE(MAX(ProcessingTechnology),1) INTO pProcessingTechnology FROM 
			tooltypeattribute tta
		INNER JOIN attributevalue av ON av.AttributeDefinitionId = tta.AttributeDefinitionId 
										AND av.ParentTypeId = tta.ParentTypeId
		WHERE av.ParentId = oldId AND av.ParentTypeId = pParentTypeId;
	END IF;
	
	RETURN pProcessingTechnology;
END //

CREATE OR REPLACE FUNCTION GetBevelTypeDisplayValue(iBevelType INT)
RETURNS VARCHAR(32)
BEGIN
	DECLARE pDisplayValue VARCHAR(32) DEFAULT '';
	
	SET pDisplayValue = 
		CASE
			WHEN iBevelType = 0 THEN '*'
			WHEN iBevelType = 1 THEN 'A'
			WHEN iBevelType = 2 THEN 'B'
			WHEN iBevelType = 3 THEN 'K'
			WHEN iBevelType = 4 THEN 'V'						
			WHEN iBevelType = 5 THEN 'Y'
		END ;
		
	RETURN pDisplayValue;											
END //

CREATE OR REPLACE FUNCTION GetSubRangeDisplayValueFromMasterId(iMasterId INT, iSubRangeTypeId INT)
RETURNS VARCHAR(2000)
BEGIN
	DECLARE displayValue VARCHAR(2000) DEFAULT('');
	DECLARE done INT DEFAULT FALSE;
	DECLARE detailValue VARCHAR(50);
   DECLARE pDisplayName VARCHAR(32);
	
	DECLARE curDetails CURSOR FOR 
		SELECT ad.DisplayName, di.`Value` FROM detailidentifier di 
		INNER JOIN attributedefinition ad ON ad.Id = di.AttributeDefinitionId  
		WHERE di.MasterId = iMasterId;
    
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	OPEN curDetails;
	SET displayValue = UCASE(GetSubRangeTypeDisplayName(iSubRangeTypeId));		
	
	loop_values: LOOP
		FETCH curDetails INTO pDisplayName, detailValue;
		IF done THEN
			LEAVE loop_values;
		END IF;	
		
		IF detailValue <> '' THEN
			IF LCASE(pDisplayName) = 'beveltype' THEN
				SET displayValue = CONCAT(displayValue, ' - ', GetBevelTypeDisplayValue(detailValue));
			ELSE
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
		END IF;	
	END LOOP;
	CLOSE curDetails;
	
	RETURN UPPER(displayValue);
END //

USE machine;

DELIMITER //

CREATE OR REPLACE FUNCTION CreateHashCodeByAttributeValues(iEntityTypeId INT, iParentId INT, iParentTypeId INT)
RETURNS CHAR(64)
BEGIN
	DECLARE pStringToHash TEXT DEFAULT ('');
	DECLARE pValue DECIMAL(13,2);
	DECLARE pTextValue TEXT;
	DECLARE pAttributeKind INT;
	DECLARE done BOOLEAN DEFAULT(FALSE);
 	DECLARE pContext TEXT DEFAULT 'Declare cursor for Attributes';
 	
	DECLARE curAttributeValues CURSOR FOR 	
		SELECT av.Value, av.TextValue, ad.AttributeKindId
		FROM attributevalue av 
			INNER JOIN attributedefinition ad ON ad.Id = av.AttributeDefinitionId AND ad.ParentTypeId = av.ParentTypeId
		WHERE av.ParentId = iParentId AND av.ParentTypeId = iParentTypeId;

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'CreateHashCodeByAttributeValues ERROR';
	END;
	
	OPEN curAttributeValues;
	
	SET pContext = 'Initiliaze hashcode';
	SET pStringToHash = CONCAT(iParentId,iParentTypeId, iEntityTypeId);	
	
loop_values: LOOP
		FETCH curAttributeValues INTO pValue, pTextValue, pAttributeKind;	
		
		IF done THEN
			LEAVE loop_values;		
		END IF;

		SET pContext = 'update hashcode';		
		IF pAttributeKind = 2 OR pAttributeKind = 4 THEN
			SET pStringToHash = CONCAT(pStringToHash,pTextValue);
		ELSE
			SET pStringToHash = CONCAT(pStringToHash,pValue);
		END IF;		
		
	END LOOP;
	
	CLOSE curAttributeValues;
	
	SET pContext = 'Return hashcode';
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
	
	SET pStringToHash = CONCAT(iMasterId,iEntityTypeId);

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

CREATE OR REPLACE FUNCTION GetDisplayNameByAttributeValues(iParentId INT, iParentTypeId INT)
RETURNS VARCHAR(500)
BEGIN
	DECLARE pDisplayName VARCHAR(500) DEFAULT ('');
	DECLARE pValue DECIMAL(13,2);
	DECLARE pTextValue TEXT;
	DECLARE pAttributeKind INT;
	DECLARE done BOOLEAN DEFAULT(FALSE);
    DECLARE pContext TEXT DEFAULT 'Declare cursor';
	 
	DECLARE curAttributeValues CURSOR FOR 	
		SELECT av.Value, av.TextValue, ad.AttributeKindId
		FROM attributevalue av 
		INNER JOIN attributedefinition ad ON ad.Id = av.AttributeDefinitionId AND ad.ParentTypeId = av.ParentTypeId
		WHERE av.ParentId = iParentId AND av.ParentTypeId = iParentTypeId
			AND ad.AttributeTypeId = 1
		ORDER BY av.Priority; # Identifiers

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	OPEN curAttributeValues;
	
	SET pDisplayName = '';	
	
loop_values: LOOP
		SET pContext = 'Read attribute data';		
		FETCH curAttributeValues INTO pValue, pTextValue, pAttributeKind;	
		
		IF done THEN
			LEAVE loop_values;		
		END IF;

		SET pContext = 'Merge Display Name Attribute values';				
		IF pDisplayName = '' THEN
			IF pAttributeKind = 2 OR pAttributeKind = 4 THEN
				SET pDisplayName = pTextValue;
			ELSE
				SET pDisplayName = pValue;
			END IF;		
		ELSE
			IF pAttributeKind = 2 OR pAttributeKind = 4 THEN
				SET pDisplayName = CONCAT(pDisplayName, ' - ', pTextValue);
			ELSE
				SET pDisplayName = CONCAT(pDisplayName, ' - ', pValue);
			END IF;			
		END IF;
		
	END LOOP;
	
	CLOSE curAttributeValues;
	
	SET pContext = 'Return Display Name';					
	RETURN pDisplayName;

END //

CREATE OR REPLACE FUNCTION GetDisplayNameForPiece(iPieceId INT)
RETURNS VARCHAR(500)
BEGIN
	DECLARE pDisplayName VARCHAR(500) DEFAULT ('');
	DECLARE pContract VARCHAR(100);
	DECLARE pProject VARCHAR(100);
	DECLARE pDrawing VARCHAR(100);
	DECLARE pAssembly VARCHAR(100);
	DECLARE pPart VARCHAR(100);
	
	SELECT Contract, Project, Drawing, Assembly, Part
		INTO pContract, pProject, pDrawing, pAssembly, pPart 
	FROM piece WHERE Id = iPieceId;
	
	IF COALESCE(pContract,'') <> '' THEN
		SET pDisplayName = pContract;
	END IF;
	
	IF COALESCE(pProject,'') <> '' THEN
		SET pDisplayName = CONCAT(pDisplayName,'_',pProject);
	END IF;

	IF COALESCE(pDrawing,'') <> '' THEN
		SET pDisplayName = CONCAT(pDisplayName,'_',pDrawing);
	END IF;
	
	IF COALESCE(pAssembly,'') <> '' THEN
		SET pDisplayName = CONCAT(pDisplayName,'_',pAssembly);
	END IF;

	IF COALESCE(pPart,'') <> '' THEN
		SET pDisplayName = CONCAT(pDisplayName,'_',pPart);
	END IF;
	
	RETURN pDisplayName;
END //
DELIMITER ;

