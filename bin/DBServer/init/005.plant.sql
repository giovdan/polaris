-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versione server:              10.6.4-MariaDB - mariadb.org binary distribution
-- S.O. server:                  Win64
-- HeidiSQL Versione:            11.3.0.6338
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dump della struttura del database plant
CREATE DATABASE IF NOT EXISTS `plant` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `plant`;

-- Dump della struttura di tabella plant.plantmachine
CREATE TABLE IF NOT EXISTS `plantmachine` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `Description` text DEFAULT NULL,
  `IP` text NOT NULL,
  `CurrentLoggedUser` varchar(32) DEFAULT NULL,
  `StatusId` int(11) NOT NULL,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `UpdatedOn` datetime DEFAULT current_timestamp(),
  `TimeZoneId` text NOT NULL DEFAULT 'W. Europe Standard Time',
  `PlantName` text NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `PlantMachine_IX_PlantMachine_StatusId` (`StatusId`),
  KEY `PlantMachine_IX_PlantMachine_Name` (`Name`(3072)),
  CONSTRAINT `FK_PM_PlantMachineStatus` FOREIGN KEY (`StatusId`) REFERENCES `plantmachinestatus` (`Id`) ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.plantmachine: ~2 rows (circa)
/*!40000 ALTER TABLE `plantmachine` DISABLE KEYS */;
REPLACE INTO `plantmachine` (`Id`, `Name`, `Description`, `IP`, `CurrentLoggedUser`, `StatusId`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`, `PlantName`) VALUES
	(1, 'GEMINI HPE 1', 'Postazione foratura', ' 192.168.40.88', NULL, 512, 'MITROL', '2021-10-04 08:35:26', 'MITROL', '2021-10-04 08:35:26', 'W. Europe Standard Time', ''),
	(2, 'GEMINI HPE C', 'Postazione taglio', ' 192.168.40.95', NULL, 512, 'MITROL', '2021-10-04 08:35:26', 'MITROL', '2021-10-04 08:35:26', 'W. Europe Standard Time', '');
/*!40000 ALTER TABLE `plantmachine` ENABLE KEYS */;

-- Dump della struttura di tabella plant.plantmachinestatus
CREATE TABLE IF NOT EXISTS `plantmachinestatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `Description` text NOT NULL,
  `DisplayName` varchar(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8193 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.plantmachinestatus: ~14 rows (circa)
/*!40000 ALTER TABLE `plantmachinestatus` DISABLE KEYS */;
REPLACE INTO `plantmachinestatus` (`Id`, `Code`, `Description`, `DisplayName`) VALUES
	(1, 'ConfigurationFault', 'Configurazione non valida', 'ConfigurationFault'),
	(2, 'AuxiliaresOff', 'Ausiliari non attivi', 'AuxilariesOff'),
	(4, 'Standby', 'Standby', 'Standby'),
	(8, 'Emergency', 'Emergenza premuta', 'Emergency'),
	(16, 'Alarm', 'Allarme in corso', 'Alarm'),
	(32, 'Message', 'Messaggio in corso', 'Message'),
	(64, 'Setup', 'Setup in corso', 'Setup'),
	(128, 'ReadyToStart', 'In attesa di START', 'ReadyToStart'),
	(256, 'Running', 'In esecuzione', 'Running'),
	(512, 'OnHold', 'HOLD in corso', 'Hold'),
	(1024, 'PlcNotOk', 'Plc non Ok', 'PlcNotOk'),
	(2048, 'Reset', 'Reset', 'Reset'),
	(4096, 'Manual', 'Manual', 'Manual'),
	(8192, 'Test', 'Test', 'Test');
/*!40000 ALTER TABLE `plantmachinestatus` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehouse
CREATE TABLE IF NOT EXISTS `warehouse` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` text NOT NULL,
  `Description` text NOT NULL,
  `StatusId` int(11) NOT NULL,
  `TypeId` int(11) NOT NULL DEFAULT 1,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT 'MITROL',
  `UpdatedOn` datetime DEFAULT current_timestamp(),
  `TimeZoneId` text NOT NULL DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `FK_WH_Type` (`TypeId`),
  KEY `FK_WH_Status` (`StatusId`),
  KEY `Warehouse_IX_Warehouse_Code` (`Code`(3072)),
  CONSTRAINT `FK_WH_Status` FOREIGN KEY (`StatusId`) REFERENCES `warehousestatus` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_WH_Type` FOREIGN KEY (`TypeId`) REFERENCES `warehousetype` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehouse: ~2 rows (circa)
/*!40000 ALTER TABLE `warehouse` DISABLE KEYS */;
REPLACE INTO `warehouse` (`Id`, `Code`, `Description`, `StatusId`, `TypeId`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) VALUES
	(1, 'ToolCart1', 'Carrello Utensili', 1, 1, 'MITROL', '2021-10-04 08:33:42', 'MITROL', '2021-10-04 08:33:42', 'W. Europe Standard Time'),
	(2, 'Plates', 'Magazzino Piastre', 1, 4, 'MITROL', '2021-10-04 08:33:42', 'MITROL', '2021-10-04 08:33:42', 'W. Europe Standard Time');
/*!40000 ALTER TABLE `warehouse` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehouseentitytype
CREATE TABLE IF NOT EXISTS `warehouseentitytype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `Description` text NOT NULL,
  `DisplayName` varchar(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehouseentitytype: ~3 rows (circa)
/*!40000 ALTER TABLE `warehouseentitytype` DISABLE KEYS */;
REPLACE INTO `warehouseentitytype` (`Id`, `Code`, `Description`, `DisplayName`) VALUES
	(1, 'Tool', 'Utensile', 'Tool'),
	(2, 'ToolHolder', 'Porta utensile', 'ToolHolder'),
	(4, 'StockItem', 'Magazzino materiali', 'Stock');
/*!40000 ALTER TABLE `warehouseentitytype` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehouselink
CREATE TABLE IF NOT EXISTS `warehouselink` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `WarehouseId` int(11) NOT NULL,
  `EntityId` int(11) NOT NULL,
  `EntityCode` text NOT NULL,
  `EntityTypeId` int(11) NOT NULL,
  `Notes` text DEFAULT NULL,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT 'MITROL',
  `UpdatedOn` datetime DEFAULT current_timestamp(),
  `TimeZoneId` text NOT NULL DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `IX_WarehouseLink_WarehouseId` (`WarehouseId`),
  CONSTRAINT `FK_WL_Warehouse_Id` FOREIGN KEY (`WarehouseId`) REFERENCES `warehouse` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehouselink: ~0 rows (circa)
/*!40000 ALTER TABLE `warehouselink` DISABLE KEYS */;
/*!40000 ALTER TABLE `warehouselink` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehousestatus
CREATE TABLE IF NOT EXISTS `warehousestatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `Description` text NOT NULL,
  `DisplayName` varchar(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehousestatus: ~4 rows (circa)
/*!40000 ALTER TABLE `warehousestatus` DISABLE KEYS */;
REPLACE INTO `warehousestatus` (`Id`, `Code`, `Description`, `DisplayName`) VALUES
	(1, 'Ok', 'In funzione', 'Ok'),
	(2, 'Standby', 'In allestimento', 'Standby'),
	(4, 'Broken', 'Rotto', 'Broken'),
	(8, 'Disabled', 'Disabilitato', 'Disabled');
/*!40000 ALTER TABLE `warehousestatus` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehousetype
CREATE TABLE IF NOT EXISTS `warehousetype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `Description` text NOT NULL,
  `DisplayName` varchar(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehousetype: ~3 rows (circa)
/*!40000 ALTER TABLE `warehousetype` DISABLE KEYS */;
REPLACE INTO `warehousetype` (`Id`, `Code`, `Description`, `DisplayName`) VALUES
	(1, 'Cart', 'Carrello', 'Cart'),
	(2, 'Warehouse', 'Magazzino', 'Warehouse'),
	(4, 'Stock', 'Magazzino Materiali', 'Stock');
/*!40000 ALTER TABLE `warehousetype` ENABLE KEYS */;

-- Dump della struttura di tabella plant.warehousetypeentitytypes
CREATE TABLE IF NOT EXISTS `warehousetypeentitytypes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `WarehouseTypeId` int(11) NOT NULL,
  `WarehouseEntityTypeId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WarehouseTypeEntityTypes_WarehouseTypeId` (`WarehouseTypeId`),
  KEY `IX_WarehouseTypeEntityTypes_WarehouseEntityTypeId` (`WarehouseEntityTypeId`),
  CONSTRAINT `FK_WTT_WarehouseEntityTypeId` FOREIGN KEY (`WarehouseEntityTypeId`) REFERENCES `warehouseentitytype` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_WTT_WarehouseTypeId` FOREIGN KEY (`WarehouseTypeId`) REFERENCES `warehousetype` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella plant.warehousetypeentitytypes: ~4 rows (circa)
/*!40000 ALTER TABLE `warehousetypeentitytypes` DISABLE KEYS */;
REPLACE INTO `warehousetypeentitytypes` (`Id`, `WarehouseTypeId`, `WarehouseEntityTypeId`) VALUES
	(1, 1, 1),
	(2, 2, 1),
	(3, 2, 2),
	(4, 4, 4);
/*!40000 ALTER TABLE `warehousetypeentitytypes` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
