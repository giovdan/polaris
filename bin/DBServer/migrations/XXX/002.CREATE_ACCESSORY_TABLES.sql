USE machine;

CREATE TABLE IF NOT EXISTS `attributedefinition` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EnumId` INT(11) NOT NULL DEFAULT '0',
	`DisplayName` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
	`DataFormat` INT(11) NOT NULL DEFAULT '0',
	`AttributeType` ENUM ('Geometric', 'Process', 'Identifier', 'Generic') NOT NULL,
	`AttributeKind` ENUM ('Number','String','Enum','Bool','Date') NOT NULL,
	`TypeName` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
	`OverrideType` ENUM ('None','DeltaValue','DeltaPercentage') NOT NULL DEFAULT 'None',
	PRIMARY KEY (`Id`) USING BTREE,
	INDEX `IDX_AttributeDefinition_DisplayName` (`DisplayName`) USING BTREE,
	INDEX `IDX_AttributeDefinition_EnumId_DisplayName` (`EnumId`, `DisplayName`) USING BTREE
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;

CREATE TABLE IF NOT EXISTS `attributedefinitionlink` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EntityTypeId` INT(11) NOT NULL,
	`AttributeDefinitionId` INT(11) NOT NULL,
	`AttributeType` ENUM ('Geometric', 'Process', 'Identifier', 'Generic') NOT NULL DEFAULT 'Generic',	
	`ControlType` ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue') NULL,
	`HelpImage` VARCHAR(200) NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
	`IsCodeGenerator` TINYINT(1) NOT NULL DEFAULT '0',
	`IsSubFilter` TINYINT(1) NOT NULL DEFAULT '0',
	`AttributeScopeId` ENUM('Optional','Fundamental','Preview') NOT NULL DEFAULT 'Optional' COLLATE 'utf8mb4_bin',
	`DefaultBehavior` ENUM('DataDefault','LastInserted') NULL DEFAULT 'DataDefault' COLLATE 'utf8mb4_bin',
	`LastInsertedValue` DECIMAL(12,6) NULL DEFAULT NULL,
	`LastInsertedTextValue` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
	`ProtectionLevel` ENUM('Critical','High','Medium','Normal','ReadOnly') NOT NULL DEFAULT 'Normal',
	`GroupId` INT(11) NOT NULL DEFAULT '0',
	`Priority` INT(11) NOT NULL DEFAULT '0',
	PRIMARY KEY (`Id`) USING BTREE,
	INDEX `AttributeDefinitionId` (`AttributeDefinitionId`) USING BTREE,
	INDEX `IDX_AttributeDefinitionLink_EntityType` (`EntityTypeId`, `AttributeDefinitionId`) USING BTREE,
	CONSTRAINT `attributedefinitionlink_ibfk_1` FOREIGN KEY (`AttributeDefinitionId`) REFERENCES `machine`.`_attributedefinition` (`Id`) ON UPDATE RESTRICT ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;

CREATE TABLE IF NOT EXISTS `attributedefinitiongrouppriority` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`AttributeDefinitionGroupId` INT(11) NULL DEFAULT NULL,
	`EntityTypeId` INT(11) NULL DEFAULT NULL,
	`Priority` INT(11) NOT NULL DEFAULT '999',
	`CreatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_unicode_ci',
	`CreatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`UpdatedBy` VARCHAR(32) NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_unicode_ci',
	`UpdatedOn` DATETIME NULL DEFAULT current_timestamp(),
	`TimeZoneId` MEDIUMTEXT NULL DEFAULT 'W. Europe Standard Time' COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (`Id`) USING BTREE,
	INDEX `FK_ADGP_EntityTypeId` (`EntityTypeId`) USING BTREE,
	CONSTRAINT `_attributedefinitiongrouppriority_ibkf_1` FOREIGN KEY (`EntityTypeId`) REFERENCES `machine`.`entitytype` (`Id`) ON UPDATE RESTRICT ON DELETE CASCADE 	
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;


CREATE TABLE IF NOT EXISTS `attributevalue` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EntityId` INT(11) NOT NULL,
	`AttributeDefinitionLinkId` INT(11) NOT NULL,
	`DataFormatId` INT(11) NOT NULL,
	`Value` DECIMAL(12,7) NULL DEFAULT NULL,
	`TextValue` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_bin',
	`Priority` INT(11) NOT NULL DEFAULT 999,
	`CreatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_bin',
	`CreatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`UpdatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_bin',
	`UpdatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`TimeZoneId` MEDIUMTEXT NULL DEFAULT 'W. Europe Standard Time' COLLATE 'utf8mb4_unicode_ci',	
	`RowVersion` TEXT NOT NULL COLLATE 'utf8mb4_bin',
	PRIMARY KEY (`Id`) USING BTREE,
	INDEX `EntityId` (`EntityId`) USING BTREE,
	INDEX `AttributeDefinitionLinkId` (`AttributeDefinitionLinkId`) USING BTREE,
	CONSTRAINT `attributevalue_ibfk_1` FOREIGN KEY (`EntityId`) REFERENCES `machine`.`entity` (`Id`),
	CONSTRAINT `attributevalue_ibfk_2` FOREIGN KEY (`AttributeDefinitionLinkId`) REFERENCES `machine`.`attributedefinitionlink` (`Id`)
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;

CREATE TABLE IF NOT EXISTS `attributeoverridevalue` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`AttributeValueId` INT(11) NOT NULL,
	`OverrideType` INT(11) NOT NULL,
	`Value` DECIMAL(12,6) NOT NULL,
	`CreatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_unicode_ci',
	`CreatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`UpdatedBy` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8mb4_unicode_ci',
	`UpdatedOn` DATETIME NULL DEFAULT NULL,
	`TimeZoneId` MEDIUMTEXT NULL DEFAULT 'W. Europe Standard Time' COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (`Id`) USING BTREE,
	INDEX `IDX_AOV_AttributeValueId_1` (`AttributeValueId`) USING BTREE,
	CONSTRAINT `FK_AOV_AttributeValue_1` FOREIGN KEY (`AttributeValueId`) REFERENCES `_attributevalue` (`Id`) 
			ON UPDATE NO ACTION ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;

CREATE TABLE IF NOT EXISTS `detailidentifier` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`HashCode` CHAR(64) NOT NULL,
	`AttributeDefinitionLinkId` INT(11) NOT NULL,
	`Value` TEXT NOT NULL,
	`Priority` INT NOT NULL DEFAULT 999,
	`CreatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_unicode_ci',
	`CreatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`UpdatedBy` VARCHAR(32) NULL DEFAULT 'MITROL' COLLATE 'utf8mb4_unicode_ci',
	`UpdatedOn` DATETIME NULL DEFAULT current_timestamp(),
	`TimeZoneId` MEDIUMTEXT NULL DEFAULT 'W. Europe Standard Time' COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (`Id`) USING BTREE,
	CONSTRAINT `detailidentifier_fk` FOREIGN KEY (`AttributeDefinitionLinkId`) REFERENCES `machine`.`attributedefinitionlink` (`Id`)
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;


CREATE TABLE IF NOT EXISTS migratedAttribute 
(
	`Id` int(11) NOT NULL AUTO_INCREMENT,
	EntityTypeId INT NOT NULL,
	ParentTypeId INT NOT NULL,
	SubParentTypeId INT NOT NULL,
	EnumId INT NOT NULL, 
	IsCodeGenerator BOOLEAN, 
	IsSubFilter BOOLEAN, 
	AttributeScopeId ENUM ('Optional','Fundamental','Preview'), 
	LastInsertedValue DECIMAL(12,6) NULL, 
	LastInsertedTextValue TEXT NULL, 
	DefaultBehavior ENUM ('DataDefault','LastInserted'), 
	`AttributeType` ENUM ('Geometric', 'Process', 'Identifier', 'Generic') NOT NULL DEFAULT 'Generic',		
	ControlType ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue') , 
	HelpImage TEXT, 
	`ProtectionLevel` ENUM('Critical','High','Medium','Normal','ReadOnly'), 
	GroupId INT, 
	Priority INT, 
	ProcessingTechnology ENUM ('Default','PlasmaHPR', 'PlasmaXPR') NOT NULL DEFAULT 'Default',
	MigrationStatus ENUM ('None','DefinitionMigrated', 'Migrated') NOT NULL DEFAULT 'None',
	PRIMARY KEY (`Id`),
	FOREIGN KEY (EntityTypeId) REFERENCES entitytype(id)
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;


CREATE TABLE IF NOT EXISTS migratedEntity
(
	`Id` int(11) NOT NULL AUTO_INCREMENT,
	EntityTypeId INT NOT NULL,
	ParentTypeId INT NOT NULL,
	ParentId INT NOT NULL,
	EntityId INT NOT NULL,
	PRIMARY KEY (`Id`),
	FOREIGN KEY (EntityId) REFERENCES entity(id)
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;

