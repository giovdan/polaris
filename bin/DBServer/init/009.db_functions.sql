DELIMITER //

CREATE OR REPLACE FUNCTION _machine.GetEntityType(pParentTypeId INT, pSubParentTypeId INT, pProcessingTechnology INT)
RETURNS INT
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
		WHEN pParentTypeId = 512 THEN 125
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
		ELSE 0
	END;	
		
	RETURN pEntityTypeId;
END; //

DELIMITER ;