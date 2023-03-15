CREATE DATABASE IF NOT EXISTS _machine;

USE _machine;

CREATE TABLE IF NOT EXISTS `entity` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`DisplayName` VARCHAR(32) NOT NULL DEFAULT '',
	`EntityTypeId` INT(11) NOT NULL DEFAULT 0,
	`HashCode` VARCHAR(500) NOT NULL,
	`SecondaryKey` INT(11) NULL,
	`Status` ENUM ('Available','Unavailable','Warning', 'Alarm', 'NoIconToDisplay', 'ToBeDeleted') NOT NULL,
	`CreatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL',
	`CreatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`UpdatedBy` VARCHAR(32) NOT NULL DEFAULT 'MITROL',
	`UpdatedOn` DATETIME NOT NULL DEFAULT current_timestamp(),
	`RowVersion` TEXT NOT NULL DEFAULT uuid(),
	PRIMARY KEY (`Id`),
	INDEX `IDX_Entity_DisplayName` (`DisplayName`, EntityTypeId),
	UNIQUE INDEX `IDX_Entity_HashCode` (`HashCode`)
) ENGINE=InnoDB DEFAULT COLLATE=utf8mb4_bin;

CREATE TABLE IF NOT EXISTS `entityLink` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RelatedEntityId` INT(11) NOT NULL,
  `EntityId` INT(11) NOT NULL,
  `RelationTypeId` INT(11) NOT NULL,  
  `RowNumber` INT(11) NOT NULL DEFAULT(1),
  `Level` INT(11) NOT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (EntityId) REFERENCES entity(id),
  FOREIGN KEY (RelatedEntityId) REFERENCES entity(id)
) ENGINE=INNODB;


CREATE TABLE IF NOT EXISTS `entitytype` (
  `Id` int(11) NOT NULL,
  `DisplayName` VARCHAR(50) NOT NULL,
  `IsManaged` BOOLEAN DEFAULT false,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS `AttributeDefinition` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EnumId` INT(11) NOT NULL DEFAULT '0',
	`Code` VARCHAR(32) NOT NULL,
	`DisplayName` VARCHAR(50) NULL DEFAULT NULL,
	`DataFormat` INT(11) NOT NULL DEFAULT 0,
	`AttributeType` INT(11) NOT NULL,
	`AttributeKind` INT(11) NOT NULL DEFAULT 1,
	`TypeName` TEXT NULL DEFAULT NULL,
	`OverrideType` INT(11) NOT NULL DEFAULT 0,
	PRIMARY KEY (`Id`),
	INDEX `IDX_AttributeDefinition_DisplayName` (`DisplayName`)  
) ENGINE=InnoDB DEFAULT COLLATE=utf8mb4_bin;

CREATE TABLE IF NOT EXISTS `AttributeDefinitionLink`
(
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EntityTypeId` INT(11) NOT NULL,
	`AttributeDefinitionId` INT(11) NOT NULL,
	`ControlType` INT(11) NULL DEFAULT 0,
	`HelpImage` VARCHAR(200) NULL DEFAULT NULL,
	`IsCodeGenerator` BOOLEAN NOT NULL DEFAULT FALSE,	
	`IsSubFilter` BOOLEAN NOT NULL DEFAULT FALSE,
	`AttributeScopeId` ENUM ('Optional', 'Fundamental', 'Preview') NOT NULL DEFAULT 'Optional',	
	`DefaultBehavior` ENUM ('DataDefault', 'LastInserted') DEFAULT 'DataDefault',	
	`LastInsertedValue` DECIMAL(12,6) NULL,
	`LastInsertedTextValue` TEXT NULL,
	`Owner` INT(11) NOT NULL DEFAULT 0,
	`GroupId` INT(11) NOT NULL DEFAULT 0,
	`Priority` INT(11) NOT NULL DEFAULT 0,
	PRIMARY KEY (`Id`),	
	FOREIGN KEY (AttributeDefinitionId) REFERENCES AttributeDefinition(id),
	INDEX `IDX_AttributeDefinitionLink_EntityType` (`EntityTypeId`, AttributeDefinitionId)  		
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;


CREATE TABLE IF NOT EXISTS `AttributeValue` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `EntityId` INT NOT NULL,
  `AttributeDefinitionLinkId` INT NOT NULL,
  `Value` decimal(10,3),
  `TextValue` TEXT, 
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),    
  `UpdatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `UpdatedOn` datetime NOT NULL DEFAULT current_timestamp(),  
  `RowVersion` TEXT NOT NULL DEFAULT uuid(),
  PRIMARY KEY (`Id`),
  FOREIGN KEY (EntityId) REFERENCES entity(id),
  FOREIGN KEY (AttributeDefinitionLinkId) REFERENCES AttributeDefinitionLink(id)
) ENGINE=InnoDB DEFAULT COLLATE=UTF8MB4_BIN;


CREATE TABLE IF NOT EXISTS migrationlog
(
	OldId INT(11),
	OldEntityTypeId INT(11),
	NewId INT(11),
	EntityTypeId INT(11),
	OldTable TEXT,
	IsDone BOOLEAN
);

INSERT INTO entitytype
(DisplayName, Id,  IsManaged)
VALUES
('ProfileL', 1,false),
('ProfileV', 2,false),
('ProfileB', 3,false),
('ProfileI', 4,false),
('ProfileD', 5,false),
('ProfileT', 6,false),
('ProfileU', 7,false),
('ProfileQ', 8,false),
('ProfileC', 9,false),
('ProfileF', 11,false),
('ProfileN', 12,false),
('ProfileP', 13,false),
('ProfileR', 14,false),
('ToolTS15', 15,false),
('ToolTS16', 16,false),
('ToolTS17', 17,false),
('ToolTS18', 18,false),
('ToolTS19', 19,false),
('ToolTS20', 20,false),
('ToolTS32', 32,false),
('ToolTS33', 33,false),
('ToolTS34', 34,false),
('ToolTS35', 35,false),
('ToolTS36', 36,false),
('ToolTS38', 38,false),
('ToolTS39', 39,false),
('ToolTS40', 40,false),
('ToolTS41', 41,false),
('ToolTS50', 50,false),
('ToolTS51', 51,false),
('ToolTS52', 52,false),
('ToolTS53', 53,false),
('ToolTS54', 54,false),
('ToolTS55', 55,false),
('ToolTS56', 56,false),
('ToolTS57', 57,false),
('ToolTS61', 61,false),
('ToolTS62', 62,false),
('ToolTS68', 68,false),
('ToolTS69', 69,false),
('ToolTS70', 70,false),
('ToolTS71', 71,false),
('ToolTS73', 73,false),
('ToolTS74', 74,false),
('ToolTS75', 75,false),
('ToolTS76', 76,false),
('ToolTS77', 77,false),
('ToolTS78', 78,false),
('ToolTS79', 79,false),
('ToolTS80', 80,false),
('ToolTS87', 87,false),
('ToolTS88', 88,false),
('ToolTS89', 89,false),
('StockProfileL', 90,false),
('StockProfileV', 91,false),
('StockProfileB', 92,false),
('StockProfileI', 93,false),
('StockProfileD', 94,false),
('StockProfileT', 95,false),
('StockProfileU', 96,false),
('StockProfileQ', 97,false),
('StockProfileC', 98,false),
('StockProfileF', 99,false),
('StockProfileN', 100,false),
('StockProfileP', 101,false),
('StockProfileR', 102,false),
('ToolHolder', 103,false), 
('ProgramPieceSequence', 104,false),
('ProgramLinearNesting', 105,false),
('ProgramPieceToMeasure', 106,false),
('ProgramPlateNesting', 107,false);	