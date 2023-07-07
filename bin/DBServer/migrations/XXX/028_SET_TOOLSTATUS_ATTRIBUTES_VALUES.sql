USE machine;

# Aggiornamento IsStatusAttribute per Drill Tools
UPDATE attributedefinitionlink 
SET IsStatusAttribute = 1
WHERE
	AttributeDefinitionId IN (SELECT Id FROM attributedefinition WHERE DisplayName IN (
							'MaxToolLife',
							'ToolLife',
							'WarningToolLife',
							'ToolLength',
							'AutoSensitiveEnable',
							'ToolEnableA',
							'ToolEnableB',
							'ToolEnableC',
							'ToolEnableD'));
							
# Aggiornamento IsStatusAttribute per Plasma Tools
UPDATE attributedefinitionlink 
SET IsStatusAttribute = 1
WHERE
	AttributeDefinitionId IN (SELECT Id FROM attributedefinition WHERE DisplayName IN (
					'NozzleLifeMaxIgnitions',
                'NozzleLifeIgnitions',
                'NozzleLifeWarningLimitIgnitions'));
                
# Aggiornamento IsStatusAttribute per Oxy Tools
UPDATE attributedefinitionlink 
SET IsStatusAttribute = 1
WHERE
	AttributeDefinitionId IN (SELECT Id FROM attributedefinition WHERE DisplayName IN (
				'NozzleLifeMaxTime'
                'NozzleLifeTime',
                'NozzleLifeWarningTime'));
				
				
# Aggiornamento IsStatusAttribute per Saw Tools
UPDATE attributedefinitionlink 
SET IsStatusAttribute = 1
WHERE
	AttributeDefinitionId IN (SELECT Id FROM attributedefinition WHERE DisplayName IN (
                'MaxBladeLife',
                'BladeLife',
                'WarningBladeLife'));				
