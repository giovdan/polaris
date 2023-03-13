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
	FOREIGN KEY (MasterId) REFERENCES MasterIdentifier(Id),
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

CREATE TABLE IF NOT EXISTS `AttributeDefinition` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EnumId` INT(11) NOT NULL DEFAULT '0',
	`Code` VARCHAR(32) NOT NULL,
	`Description` TEXT NOT NULL,
	`DisplayName` VARCHAR(50) NULL DEFAULT NULL,
	`HelpImage` VARCHAR(200) NULL DEFAULT NULL,
	`DataFormat` INT(11) NOT NULL DEFAULT 0,
	`AttributeType` INT(11) NOT NULL,
	`AttributeKind` INT(11) NOT NULL DEFAULT 1,
	`TypeName` TEXT NULL DEFAULT NULL,
	`OverrideType` INT(11) NOT NULL DEFAULT 0,
	`ControlType` INT(11) NULL DEFAULT 0,
	PRIMARY KEY (`Id`),
	INDEX `IDX_AttributeDefinition_DisplayName` (`DisplayName`)  
) ENGINE=InnoDB DEFAULT COLLATE=utf8mb4_bin;

CREATE TABLE IF NOT EXISTS `AttributeDefinitionLink`
(
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`EntityTypeId` INT(11) NOT NULL,
	`ControlType` INT(11) NULL DEFAULT 0,
	`AttributeDefinitionId` INT(11) NOT NULL,
	`IsCodeGenerator` BOOLEAN NOT NULL DEFAULT FALSE,	
	`IsSubFilter` BOOLEAN NOT NULL DEFAULT FALSE,
	`IsQuickAccess` BOOLEAN NOT NULL DEFAULT FALSE,	
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

