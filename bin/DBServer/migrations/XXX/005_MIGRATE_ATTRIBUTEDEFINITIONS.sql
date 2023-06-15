USE Machine;

DELIMITER //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLinks()
BEGIN
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'AddAttributeDefinitionLinks ERROR';
	END;
	
	INSERT INTO attributedefinitionlink
	 (EntityTypeId, AttributeDefinitionId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel, GroupId, Priority)	
	SELECT ptad.EntityTypeId, ad.Id, ptad.IsCodeGenerator, ptad.IsSubFilter, ptad.AttributeScopeId
		  , ptad.LastInsertedValue, ptad.LastInsertedTextValue, ptad.DefaultBehavior
		  , ptad.AttributeType, ptad.ControlType, ptad.HelpImage
		  , ptad.ProtectionLevel, ptad.GroupId, ptad.Priority 
	FROM migratedattribute ptad
	INNER JOIN attributedefinition ad ON ad.EnumId = ptad.EnumId
	WHERE ptad.EntityTypeId > 0 AND NOT EXISTS 
		(SELECT Id FROM attributedefinitionlink 
			WHERE AttributeDefinitionId = ad.Id AND EntityTypeId = ptad.EntityTypeId);		
END //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLinkFromProfileType(pParentTypeId INT)
BEGIN
	DECLARE pContractEnumId INT;
	DECLARE pProjectEnumId INT;
	DECLARE pDrawingEnumId INT;
	DECLARE pAssemblyEnumId INT;
	DECLARE pPartEnumId INT;
	DECLARE pOriginsCalculatedEnumId INT;
	DECLARE pExecutionDateEnumId INT;
	DECLARE pContext TEXT DEFAULT 'Cursor declaration';
	DECLARE pEntityTypeId INT;
			
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;
	
	SET pContext = 'ERROR => Insert into migratedAttribute';
	INSERT INTO migratedAttribute
	 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
	SELECT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
		, ad.ParentTypeId
		, pta.ProfileTypeId as SubParentTypeId
		, ad.EnumId
		, pta.IsCodeGenerator, 0 AS IsSubFilter
		, 'Fundamental' AS AttributeScopeId, pta.LastInsertedValue, pta.LastInsertedTextValue
		, CASE 
			WHEN pta.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
			ELSE 'DataDefault' 
		END AS DefaultBehavior	
		, GetAttributeType(ad.AttributeTypeId)
		, GetControlType(ad.ControlTypeId) AS ControlType, ad.HelpImage, ad.ProtectionLevel
		, pta.AttributeDefinitionGroupId, pta.Priority, 'Default' AS ProcessingTechnology
	FROM profiletypeattribute pta 
	INNER JOIN attributedefinition_old ad ON ad.Id = pta.AttributeDefinitionId AND ad.ParentTypeId = pta.ParentTypeId
	WHERE ad.ParentTypeId = pParentTypeId
	AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId AND SubParentTypeId = pta.ProfileTypeId
					AND EnumId = ad.EnumId);
	
			
	IF pParentTypeId = 1024 THEN
		SET pContractEnumId = 390;
		SET pProjectEnumId = 391;
		SET pDrawingEnumId = 392;
		SET pAssemblyEnumId = 393;
		SET pPartEnumId = 394;
										
		SET pContext = 'ERROR => Insert into migratedAttribute Contract identifier';
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT DISTINCT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
				, pParentTypeId
				, pta.ProfileTypeId
				, pContractEnumId
				, TRUE AS IsCodeGenerator
				, FALSE AS IsSubFilter
				, 'Fundamental' AS AttributeScopeId
				, 0 AS LastInsertedValue, '' AS LastInsertedTextValue
				, 'DataDefault' AS DefaultBehavior
				, 'Generic' AS AttributeType
				, 'Label' AS ControlType
				, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
				, 3 AS GroupId
				, 1 AS Priority
				, 1 AS ProcessingTechnology
				FROM profiletypeattribute pta
				WHERE pta.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = pParentTypeId AND SubParentTypeId = pta.ProfileTypeId
								AND EnumId = pContractEnumId);				
								
		SET pContext = 'ERROR => Insert into migratedAttribute Project identifier';								
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT DISTINCT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
				, pParentTypeId
				, pta.ProfileTypeId
				, pProjectEnumId
				, true
				, false
				, 'Fundamental' AS AttributeScopeId
				, 0 AS LastInsertedValue, '' AS LastInsertedTextValue
				, 'DataDefault' AS DefaultBehavior
				, 'Generic' AS AttributeType
				, 'Label' AS ControlType
				, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
				, 3 AS GroupId
				, 2 AS Priority
				, 1 AS ProcessingTechnology
				FROM profiletypeattribute pta
				WHERE pta.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = pParentTypeId AND SubParentTypeId = pta.ProfileTypeId
								AND EnumId = pProjectEnumId);				
								
		SET pContext = 'ERROR => Insert into migratedAttribute Drawing identifier';								
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT DISTINCT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
				, pParentTypeId
				, pta.ProfileTypeId
				, pDrawingEnumId
				, true
				, false
				, 'Fundamental' AS AttributeScopeId
				, 0 AS LastInsertedValue, '' AS LastInsertedTextValue
				, 'DataDefault' AS DefaultBehavior
				, 'Generic' AS AttributeType
				, 'Label' AS ControlType
				, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
				, 3 AS GroupId
				, 3 AS Priority
				, 1 AS ProcessingTechnology
				FROM profiletypeattribute pta
				WHERE pta.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = pParentTypeId 
								AND SubParentTypeId = pta.ProfileTypeId
								AND EnumId = pDrawingEnumId);				
								
		SET pContext = 'ERROR => Insert into migratedAttribute Assembly identifier';								
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT DISTINCT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
				, pParentTypeId
				, pta.ProfileTypeId
				, pAssemblyEnumId
				, true
				, false
				, 'Fundamental' AS AttributeScopeId
				, 0 AS LastInsertedValue, '' AS LastInsertedTextValue
				, 'DataDefault' AS DefaultBehavior
				, 'Generic' AS AttributeType
				, 'Label' AS ControlType
				, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
				, 3 AS GroupId
				, 4 AS Priority
				, 1 AS ProcessingTechnology
				FROM profiletypeattribute pta
				WHERE pta.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = pParentTypeId 
								AND SubParentTypeId = pta.ProfileTypeId
								AND EnumId = pAssemblyEnumId);				
								
		SET pContext = 'ERROR => Insert into migratedAttribute Part identifier';								
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT DISTINCT GetEntityType(pParentTypeId, pta.ProfileTypeId, 1) AS EntitytypeId
				, pParentTypeId
				, pta.ProfileTypeId
				, pPartEnumId
				, true
				, false
				, 'Fundamental' AS AttributeScopeId
				, 0 AS LastInsertedValue, '' AS LastInsertedTextValue
				, 'DataDefault' AS DefaultBehavior
				, 'Generic' AS AttributeType
				, 'Label' AS ControlType
				, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
				, 3 AS GroupId
				, 5 AS Priority
				, 1 AS ProcessingTechnology
				FROM profiletypeattribute pta
				WHERE pta.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = pParentTypeId 
								AND SubParentTypeId = pta.ProfileTypeId
								AND EnumId = pPartEnumId);																																				
	END IF;					
	
	IF pParentTypeId = 8192 THEN
		SET pContext = 'ERROR => Insert into migratedAttribute OriginsCalculated attribute';								
		SET pOriginsCalculatedEnumId = 395;
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT GetEntityType(pParentTypeId, p.Id, 1) AS EntityTypeId, pParentTypeId, p.Id
			, pOriginsCalculatedEnumId, false AS IsCodeGenerator, false AS IsSubFilter
			, 'Optional' AS AttributeScopeId, 0 AS LastInsertedValue
			, '' AS LastInsertedTextValue, 'DataDefault' AS DefaultBehavior
			, 'Generic' AS AttributeType, 'Label' AS ControlType
			, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
			, 4 AS GroupId, 999 AS Priority
			, 1 AS ProcessingTechnology
			FROM 
			profiletype p;			  	
			
		SET pContext = 'ERROR => Insert into migratedAttribute ExecutionDate attribute';								
		SET pExecutionDateEnumId = 396;
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)	
		SELECT GetEntityType(pParentTypeId, p.Id, 1) AS EntityTypeId, pParentTypeId, p.Id
			, pExecutionDateEnumId, false AS IsCodeGenerator, false AS IsSubFilter
			, 'Optional' AS AttributeScopeId, 0 AS LastInsertedValue
			, '' AS LastInsertedTextValue, 'DataDefault' AS DefaultBehavior
			, 'Generic' AS AttributeType, 'Label' AS ControlType
			, NULL AS HelpImage, 'ReadOnly' AS ProtectionLevel
			, 4 AS GroupId, 999 AS Priority
			, 1 AS ProcessingTechnology
			FROM 
			profiletype p;			  	
			
	END IF;
END //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLinkFromToolType(pParentTypeId INT)
BEGIN
DECLARE pContext TEXT DEFAULT 'Error => Prima di ogni operazione';

DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;

	SET pContext = 'AddAttributeDefinitionLinkFromToolType Error => Inserimento migratedAttribute WHERE ToolTypeId NOT IN (51,53)';	
	INSERT INTO migratedAttribute
	 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
		SELECT GetEntityType(pParentTypeId, tta.ToolTypeId, tta.ProcessingTechnology) AS EntitytypeId
			, ad.ParentTypeId
			, tta.ToolTypeId AS SubParentTypeId
			, ad.EnumId AS EnumId
			, tta.IsCodeGenerator, 0 AS IsSubFilter
			, GetAttributeScope(tta.AttributeScopeId) AS AttributeScopeId
			, tta.LastInsertedValue, tta.LastInsertedTextValue
			, CASE 
				WHEN tta.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
				ELSE 'DataDefault' 
			END AS DefaultBehavior	
			, GetAttributeType(ad.AttributeTypeId)
			, GetControlType(ad.ControlTypeId) AS ControlType, ad.HelpImage AS HelpImage, ad.ProtectionLevel AS ProtecetionLevel
			, ad.GroupId AS AttributeDefinitionGroupId, tta.Priority
			, CASE
				WHEN tta.ProcessingTechnology = 1 THEN 'Default'
				WHEN tta.ProcessingTechnology = 2 THEN 'PlasmaHPR'
				WHEN tta.ProcessingTechnology = 4 THEN 'PlasmaXPR'								
			END AS ProcessingTechnology	
		FROM tooltypeattribute tta 
		INNER JOIN attributedefinition_old ad ON ad.Id = tta.AttributeDefinitionId AND ad.ParentTypeId = tta.ParentTypeId
		WHERE tta.ParentTypeId = pParentTypeId	
		AND GetEntityType(pParentTypeId, tta.ToolTypeId, tta.ProcessingTechnology) > 0
		AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId AND SubParentTypeId = tta.ToolTypeId
					AND EnumId = ad.EnumId)
		AND tta.ToolTypeId NOT IN (51,53);
	
		SET pContext = 'AddAttributeDefinitionLinkFromToolType Error => Inserimento migratedAttribute WHERE tta.ToolTypeId IN (51,53) AND tta.ProcessingTechnology = 1';	
		# Gestione particolare per i tool di tipo PLASMA o MARCATURA PLASMA (TS51 e TS53)
		# Inserisco gli attributi con Processing Technology = 'Default'
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
			SELECT GetEntityType(pParentTypeId, tta.ToolTypeId, tta.ProcessingTechnology) AS EntitytypeId
				, ad.ParentTypeId
				, tta.ToolTypeId AS SubParentTypeId
				, ad.EnumId AS EnumId
				, tta.IsCodeGenerator, 0 AS IsSubFilter
				, GetAttributeScope(tta.AttributeScopeId) AS AttributeScopeId
				, tta.LastInsertedValue, tta.LastInsertedTextValue
				, CASE 
					WHEN tta.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
					ELSE 'DataDefault' 
				END AS DefaultBehavior	
				, GetAttributeType(ad.AttributeTypeId)
				, GetControlType(ad.ControlTypeId) AS ControlType, ad.HelpImage AS HelpImage, ad.ProtectionLevel AS ProtecetionLevel
				, ad.GroupId AS AttributeDefinitionGroupId, tta.Priority
				, 'Default' AS ProcessingTechnology
			FROM tooltypeattribute tta 
			INNER JOIN attributedefinition_old ad ON ad.Id = tta.AttributeDefinitionId AND ad.ParentTypeId = tta.ParentTypeId
			WHERE tta.ParentTypeId = pParentTypeId	AND tta.ToolTypeId IN (51,53) 
			AND tta.ProcessingTechnology = 1
			AND GetEntityType(pParentTypeId, tta.ToolTypeId, tta.ProcessingTechnology) > 0			
			AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId AND SubParentTypeId = tta.ToolTypeId
						AND EnumId = ad.EnumId);		
		
		# Inserisco gli attributi con Processing Technology = 'HPR'
		SET pContext = 'AddAttributeDefinitionLinkFromToolType Error => Inserimento migratedAttribute WHERE tta.ToolTypeId IN (51,53) AND tta.ProcessingTechnology IN (1,2)';	
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
			SELECT GetEntityType(pParentTypeId, tta.ToolTypeId, 2) AS EntitytypeId
				, ad.ParentTypeId
				, tta.ToolTypeId AS SubParentTypeId
				, ad.EnumId AS EnumId
				, tta.IsCodeGenerator, 0 AS IsSubFilter
				, GetAttributeScope(tta.AttributeScopeId) AS AttributeScopeId
				, tta.LastInsertedValue, tta.LastInsertedTextValue
				, CASE 
					WHEN tta.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
					ELSE 'DataDefault' 
				END AS DefaultBehavior	
				, GetAttributeType(ad.AttributeTypeId)
				, GetControlType(ad.ControlTypeId) AS ControlType, ad.HelpImage AS HelpImage, ad.ProtectionLevel AS ProtecetionLevel
				, ad.GroupId AS AttributeDefinitionGroupId, tta.Priority
				, CASE
					WHEN tta.ProcessingTechnology = 1 THEN 'Default'
					WHEN tta.ProcessingTechnology = 2 THEN 'PlasmaHPR'
					WHEN tta.ProcessingTechnology = 4 THEN 'PlasmaXPR'								
				END AS ProcessingTechnology	
			FROM tooltypeattribute tta 
			INNER JOIN attributedefinition_old ad ON ad.Id = tta.AttributeDefinitionId AND ad.ParentTypeId = tta.ParentTypeId
			WHERE tta.ParentTypeId = pParentTypeId	AND tta.ToolTypeId IN (51,53) 
			AND tta.ProcessingTechnology IN (1,2)
			AND GetEntityType(pParentTypeId, tta.ToolTypeId, 2) > 0
			AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId AND SubParentTypeId = tta.ToolTypeId
						AND EnumId = ad.EnumId AND EntityTypeId = GetEntityType(pParentTypeId, tta.ToolTypeId, 2));		
						
		# Inserisco gli attributi con Processing Technology = 'XPR'
		SET pContext= 'AddAttributeDefinitionLinkFromToolType Error => Inserimento migratedAttribute WHERE tta.ToolTypeId IN (51,53) AND tta.ProcessingTechnology IN (1,4)';			
		INSERT INTO migratedAttribute
		 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
			  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
			  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
			SELECT GetEntityType(pParentTypeId, tta.ToolTypeId, 4) AS EntitytypeId
				, ad.ParentTypeId
				, tta.ToolTypeId AS SubParentTypeId
				, ad.EnumId AS EnumId
				, tta.IsCodeGenerator, 0 AS IsSubFilter
				, GetAttributeScope(tta.AttributeScopeId) AS AttributeScopeId
				, tta.LastInsertedValue, tta.LastInsertedTextValue
				, CASE 
					WHEN tta.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
					ELSE 'DataDefault' 
				END AS DefaultBehavior	
				, GetAttributeType(ad.AttributeTypeId)
				, GetControlType(ad.ControlTypeId) AS ControlType, ad.HelpImage AS HelpImage, ad.ProtectionLevel AS ProtecetionLevel
				, ad.GroupId AS AttributeDefinitionGroupId, tta.Priority
				, CASE
					WHEN tta.ProcessingTechnology = 1 THEN 'Default'
					WHEN tta.ProcessingTechnology = 2 THEN 'PlasmaHPR'
					WHEN tta.ProcessingTechnology = 4 THEN 'PlasmaXPR'								
				END AS ProcessingTechnology	
			FROM tooltypeattribute tta 
			INNER JOIN attributedefinition_old ad ON ad.Id = tta.AttributeDefinitionId AND ad.ParentTypeId = tta.ParentTypeId
			WHERE tta.ParentTypeId = pParentTypeId	AND tta.ToolTypeId IN (51,53) 
			AND tta.ProcessingTechnology IN (1,4)
			AND GetEntityType(pParentTypeId, tta.ToolTypeId, tta.ProcessingTechnology) > 0			
			AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId AND SubParentTypeId = tta.ToolTypeId
						AND EnumId = ad.EnumId AND EntityTypeId = GetEntityType(pParentTypeId, tta.ToolTypeId, 4));									
END //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLinkFromOperationType(pParentTypeId INT)
BEGIN
DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'AddAttributeDefinitionLinkFromOperationType ERROR';
	END;
		
	INSERT INTO migratedAttribute
	 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel,  GroupId, Priority, ProcessingTechnology)
	SELECT GetEntityType(pParentTypeId, ota.OperationTypeId, 0) AS EntitytypeId
			, ad.ParentTypeId
			, ota.OperationTypeId AS SubParentTypeId
			, ad.EnumId
			, ota.IsCodeGenerator, 0 AS IsSubFilter
			, GetAttributeScope(ota.AttributeScopeId) AS AttributeScopeId
			, ota.LastInsertedValue, ota.LastInsertedTextValue
			, CASE 
				WHEN ota.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
				ELSE 'DataDefault' 
			END AS DefaultBehavior	
			, GetAttributeType(ad.AttributeTypeId)
			, GetControlType(ad.ControlTypeId), ad.HelpImage AS HelpImage, ad.ProtectionLevel
			, ota.AttributeDefinitionGroupId, ota.Priority
			, 'Default' AS ProcessingTechnology	
		FROM operationtypeattribute ota 
		INNER JOIN attributedefinition_old ad ON ad.Id = ota.AttributeDefinitionId
		WHERE ad.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId 
					AND SubParentTypeId = ota.OperationTypeId
					AND EnumId = ad.EnumId);
END //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLinkFromParentOperationType(pParentTypeId INT)
BEGIN
	DECLARE pContext TEXT DEFAULT 'Errore creazione cursore';
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;
	END;

	SET pContext = 'Errore inserimento migratedAttribute';	
	INSERT INTO migratedAttribute
	 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
	SELECT GetEntityType(pParentTypeId, ota.ParentOperationTypeId, 0) AS EntitytypeId
			, ad.ParentTypeId
			, CONCAT(ota.ParentOperationTypeId, ota.OperationTypeId) AS SubParentTypeId
			, ad.EnumId
			, ota.IsCodeGenerator, 0 AS IsSubFilter
			, GetAttributeScope(ota.AttributeScopeId) AS AttributeScopeId
			, ota.LastInsertedValue, ota.LastInsertedTextValue
			, CASE 
				WHEN ota.UseLastInsertedAsDefault = 1 THEN 'LastInserted'
				ELSE 'DataDefault' 
			END AS DefaultBehavior	
			, GetAttributeType(ad.AttributeTypeId)
			, GetControlType(ad.ControlTypeId), ad.HelpImage AS HelpImage, ad.ProtectionLevel
			, ota.AttributeDefinitionGroupId, ota.Priority
			, 'Default' AS ProcessingTechnology	
		FROM operationtypeattribute ota 
		INNER JOIN attributedefinition_old ad ON ad.Id = ota.AttributeDefinitionId
		WHERE ad.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId 
					AND SubParentTypeId = CONCAT(ota.ParentOperationTypeId, ota.OperationTypeId)
					AND EnumId = ad.EnumId)
		ORDER BY ota.OperationTypeId, ota.ParentOperationTypeId, ota.Priority;
	
END //

CREATE OR REPLACE PROCEDURE AddAttributeDefinitionLink(pParentTypeId INT)
BEGIN
	DECLARE pEntityTypeId INT;
	DECLARE pGroupId INT;	
	DECLARE pContext TEXT DEFAULT 'Errore in ingresso';
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = pContext;	
	END;

	SET pContext = 'Errore => Recupero EntityType';
	SET pEntityTypeId = GetEntityType(pParentTypeId,0,0);

	SET pContext = 'Errore => Inserimento migratedAttribute';	
	INSERT INTO migratedAttribute
	 (EntityTypeId, ParentTypeId, SubParentTypeId, EnumId, IsCodeGenerator, IsSubFilter, AttributeScopeId,
		  LastInsertedValue, LastInsertedTextValue, DefaultBehavior, AttributeType, ControlType
		  , HelpImage, ProtectionLevel, GroupId, Priority, ProcessingTechnology)
		SELECT pEntityTypeId AS EntitytypeId, ad.ParentTypeId, 0 AS SubParentTypeId
			,ad.EnumId
			,1 AS IsCodeGenerator, 0 AS IsSubFilter
			,'Fundamental' as AttributeScopeId, 0 as LastInsertedValue, '' as LastInsertedTextValue
			,'DataDefault' AS DefaultBehavior	
			,GetAttributeType(ad.AttributeTypeId)
			,GetControlType(ad.ControlTypeId), ad.HelpImage, ad.ProtectionLevel
			,ad.GroupId, ad.Priority
			,'Default' AS ProcessingTechnology
		FROM attributedefinition_old ad 
		WHERE ad.ParentTypeId = pParentTypeId
				AND NOT EXISTS (SELECT Id FROM migratedattribute WHERE ParentTypeId = ad.ParentTypeId 
									AND EnumId = ad.EnumId);
END //

CREATE OR REPLACE PROCEDURE `MigrateAttributeDefinitions`(pParentTypeId INT)
BEGIN
	DECLARE pAttributeType ENUM ('Geometric','Process','Identifier','Generic');
	DECLARE pAttributeKind ENUM ('Number','String','Enum','Bool','Date');
	DECLARE pOverrideType ENUM ('None','DeltaValue','DeltaPercentage');
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	 BEGIN
 	     ROLLBACK;
	     SHOW ERRORS;  
	 END; 

START TRANSACTION;	 

	INSERT INTO attributedefinition
	(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
	SELECT  ad.EnumId,ad.DisplayName,ad.DataFormatId, GetAttributeType(ad.AttributeTypeId), 
			  GetAttributeKind(ad.AttributeKindId), ad.TypeName, GetOverrideType(ad.OverrideTypeId)
	FROM attributedefinition_old ad 
	WHERE ad.ParentTypeId = pParentTypeId 
	AND NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = ad.EnumId AND DisplayName = ad.DisplayName);							 

	IF pParentTypeId = 1024 THEN
		SET pAttributeType = GetAttributeType(2);
		SET pAttributeKind = GetAttributeKind(4);
		SET pOverrideType =  GetOverrideType(1);
		
		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 390, 'Contract', 17, pAttributeType, pAttributeKind, NULL, pOverrideType	FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 390 AND DisplayName = 'Contract');		

		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 391, 'Project', 17, pAttributeType, pAttributeKind, NULL, pOverrideType FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 391 AND DisplayName = 'Project');
		
		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 392, 'Drawing', 17, pAttributeType, pAttributeKind, NULL, pOverrideType FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 392 AND DisplayName = 'Drawing');

		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 393, 'Assembly', 17, pAttributeType, pAttributeKind, NULL, pOverrideType FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 393 AND DisplayName = 'Assembly');
		
		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 394, 'Part', 17, pAttributeType, pAttributeKind, NULL, pOverrideType FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 394 AND DisplayName = 'Part');								
	END IF;

	IF pParentTypeId = 8192 THEN
		SET pAttributeType = GetAttributeType(8);
		SET pAttributeKind = GetAttributeKind(8);
		SET pOverrideType =  GetOverrideType(1);
				
		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 395, 'OriginsCalculated', 17, pAttributeType, pAttributeKind, NULL, pOverrideType
			FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 395 AND DisplayName = 'OriginsCalculated');		
		INSERT INTO attributedefinition
		(`EnumId`,`DisplayName`,`DataFormat`,`AttributeType`,`AttributeKind`,`TypeName`,`OverrideType`)
		SELECT 396, 'ExecutionDate', 17, pAttributeType, pAttributeKind, NULL, pOverrideType
			FROM DUAL
		WHERE NOT EXISTS (SELECT Id FROM attributedefinition WHERE EnumId = 395 AND DisplayName = 'ExecutionDate');				
	END IF;
	
	#INSERIMENTO LINKS
	CASE
		WHEN pParentTypeId IN (1,256,1024,2048,8192) THEN CALL AddAttributeDefinitionLinkFromProfileType(pParentTypeId);
		WHEN pParentTypeId IN (2, 4, 16, 32, 64) THEN CALL AddAttributeDefinitionLinkFromToolType(pParentTypeId); 														
		WHEN pParentTypeId IN (8, 128, 512, 16384) THEN CALL AddAttributeDefinitionLink(pParentTypeId); 														
		WHEN pParentTypeId = 4096 THEN CALL AddAttributeDefinitionLinkFromOperationType(pParentTypeId); 		
		WHEN pParentTypeId = 65536 THEN CALL AddAttributeDefinitionLinkFromParentOperationType(pParentTypeId);												
	END CASE;
	
	
	CALL AddAttributeDefinitionLinks();						  
	
	DELETE FROM migratedattribute WHERE EntityTypeId = 0;
	UPDATE migratedAttribute SET MigrationStatus = 'DefinitionMigrated' WHERE ParentTypeId = pParentTypeId;
COMMIT;			  	
END //