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


-- Dump della struttura del database accounting
CREATE DATABASE IF NOT EXISTS `accounting` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `accounting`;

-- Dump della struttura di tabella accounting.group
CREATE TABLE IF NOT EXISTS `group` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `DisplayName` varchar(50) NOT NULL,
  `Description` text NOT NULL,
  `IsFicep` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  KEY `IDX_Group_DisplayName` (`DisplayName`),
  KEY `IDX_Group_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.group: ~4 rows (circa)
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
REPLACE INTO `group` (`Id`, `Code`, `DisplayName`, `Description`, `IsFicep`) VALUES
	(1, 'ADMINS', 'ADMINS', 'Gruppo Amministratori FICEP', 1),
	(2, 'SUPERUSERS', 'SUPERUSERS', 'Gruppo Amministratori IMPIANTO', 0),
	(3, 'USERS', 'USERS', 'Gruppo Operatori IMPIANTO', 0),
	(4, 'BOOTUSERS', 'BOOTUSERS', 'Gruppo utenti per boot servizi', 0);
/*!40000 ALTER TABLE `group` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.grouppermission
CREATE TABLE IF NOT EXISTS `grouppermission` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` int(11) DEFAULT NULL,
  `PermissionId` int(11) DEFAULT NULL,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `FK_GP_Permission` (`PermissionId`),
  KEY `IDX_GroupPermission_GP` (`GroupId`,`PermissionId`),
  CONSTRAINT `FK_GP_Group` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_GP_Permission` FOREIGN KEY (`PermissionId`) REFERENCES `permission` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=73 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.grouppermission: ~72 rows (circa)
/*!40000 ALTER TABLE `grouppermission` DISABLE KEYS */;
REPLACE INTO `grouppermission` (`Id`, `GroupId`, `PermissionId`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) VALUES
	(1, 1, 1, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(2, 1, 2, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(3, 1, 3, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(4, 1, 4, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(5, 1, 5, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(6, 1, 6, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(7, 1, 7, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(8, 1, 8, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(9, 1, 9, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(10, 1, 10, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(11, 1, 11, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(12, 1, 12, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(13, 1, 13, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(14, 1, 14, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(15, 1, 15, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(16, 1, 16, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(17, 1, 17, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(18, 1, 18, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(19, 1, 19, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(20, 1, 20, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(21, 1, 21, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(22, 1, 22, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(23, 1, 23, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(24, 1, 24, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(25, 1, 25, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(26, 1, 26, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(27, 1, 27, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(28, 1, 28, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(29, 1, 29, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(30, 1, 30, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(31, 1, 31, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(32, 1, 32, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(33, 1, 33, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(34, 2, 1, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(35, 2, 3, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(36, 2, 5, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(37, 2, 7, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(38, 2, 8, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(39, 2, 10, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(40, 2, 11, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(41, 2, 12, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(42, 2, 13, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(43, 2, 18, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(44, 2, 20, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(45, 2, 21, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(46, 2, 22, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(47, 2, 24, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(48, 2, 27, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(49, 2, 28, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(50, 2, 29, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(51, 2, 30, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(52, 2, 31, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(53, 2, 32, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(54, 2, 33, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(55, 3, 1, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(56, 3, 5, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(57, 3, 8, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(58, 3, 10, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(59, 3, 13, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(60, 3, 18, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(61, 3, 22, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(62, 3, 29, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(63, 3, 30, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(64, 3, 31, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(65, 3, 32, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(66, 4, 2, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(67, 4, 20, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(68, 4, 21, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(69, 4, 25, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(70, 4, 26, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(71, 4, 27, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(72, 4, 28, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time');
/*!40000 ALTER TABLE `grouppermission` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.permission
CREATE TABLE IF NOT EXISTS `permission` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(100) NOT NULL,
  `DisplayName` varchar(50) NOT NULL,
  `Description` text NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_Permission_DisplayName` (`DisplayName`),
  KEY `IDX_Permission_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.permission: ~33 rows (circa)
/*!40000 ALTER TABLE `permission` DISABLE KEYS */;
REPLACE INTO `permission` (`Id`, `Code`, `DisplayName`, `Description`) VALUES
	(1, 'VIEW_USERS', 'VIEW_USERS', 'Permesso per consultare la lista di tutti gli utenti'),
	(2, 'CAN_DO_BOOT', 'CAN_DO_BOOT', 'Permesso per eseguire il boot dei microservizi'),
	(3, 'USER_MANAGEMENT', 'USER_MANAGEMENT', 'Permesso per creare, modificare e cancellare gli utenti'),
	(4, 'GROUP_MANAGEMENT', 'GROUP_MANAGEMENT', 'Permesso per creare, modificare i permessi e cancellare un gruppo'),
	(5, 'TOOL_MANAGEMENT', 'TOOL_MANAGEMENT', 'Permesso per gestire gli utensili'),
	(6, 'SEED_USERS', 'SEED_USERS', 'Permesso per effettuare l\'import degli utenti da file json'),
	(7, 'SEED_TOOLS', 'SEED_TOOLS', 'Permesso per effettuare l\'import degli utensili da file json'),
	(8, 'TOOLHODER_MANAGEMENT', 'TOOLHODER_MANAGEMENT', 'Permesso per gestire i toolholders'),
	(9, 'SESSIONS_MANAGEMENT', 'SESSIONS_MANAGEMENT', 'Permesso per consultare la lista delle sessioni'),
	(10, 'USER_CHANGEPASSWORD', 'USER_CHANGEPASSWORD', 'Permesso per cambiare la password'),
	(11, 'MATERIAL_MANAGEMENT', 'MATERIAL_MANAGEMENT', 'Permesso per gestire i materiali'),
	(12, 'UPLOAD_FNC', 'UPLOAD_FNC', 'Permesso per gestire l\'import da file di tipo FNC'),
	(13, 'PROFILES_MANAGEMENT', 'PROFILES_MANAGEMENT', 'Permesso per gestire i profili'),
	(14, 'EVENTSLOG_VIEW', 'EVENTSLOG_VIEW', 'Permesso per consultare il log degli eventi'),
	(15, 'PLANT_MANAGEMENT', 'PLANT_MANAGEMENT', 'Permesso per la gestione dell\'anagrafica macchine impianto'),
	(16, 'LOCALIZATION_CULTURE_MANAGEMENT', 'LOCALIZATION_CULTURE_MANAGEMENT', 'Permesso per aggiungere una nuova culture'),
	(17, 'LOCALIZATION_TRANSLATION_MANAGEMENT', 'LOCALIZATION_TRANSLATION_MANAGEMENT', 'Permesso per la gestione delle traduzioni'),
	(18, 'PARAMETERS_MANAGEMENT', 'PARAMETERS_MANAGEMENT', 'Permesso per la gestione dei parametri'),
	(19, 'PARAMETERS_REMOVEALL', 'PARAMETERS_REMOVEALL', 'Permesso per eliminare tutti i parametri'),
	(20, 'JSON_IMPORT', 'JSON_IMPORT', 'Permesso per gestire l\'import in formato json'),
	(21, 'JSON_EXPORT', 'JSON_EXPORT', 'Permesso per effettuare l\'export in formato json'),
	(22, 'SETUP_MANAGEMENT', 'SETUP_MANAGEMENT', 'Permesso per gestire le azione di setup'),
	(23, 'SETUPSTATUS_UPDATE', 'SETUPSTATUS_UPDATE', 'Permesso per forzare l\'aggiornamento dello stato del setup'),
	(24, 'SLOT_MANAGEMENT', 'SLOT_MANAGEMENT', 'Permesso per la gestione dell\'abilitazione/disabilitazione slot'),
	(25, 'GET_CONFIGURATION', 'GET_CONFIGURATION', 'Permesso per recuperare la configurazione della macchina'),
	(26, 'RESET_CACHE', 'RESET_CACHE', 'Permesso per resettare cache di sistema'),
	(27, 'TOOLTABLE_MANAGEMENT', 'TOOLTABLE_MANAGEMENT', 'Permesso per la gestione delle tabelle'),
	(28, 'PROGRAM_MANAGEMENT', 'PROGRAM_MANAGEMENT', 'Permesso per la gestione dei programmi'),
	(29, 'PRODUCTIONQUEUE_MANAGEMENT', 'PRODUCTIONQUEUE_MANAGEMENT', 'Permesso per la gestione della coda di produzione'),
	(30, 'PIECE_MANAGEMENT', 'PIECE_MANAGEMENT', 'Permesso per la gestione dei pezzi'),
	(31, 'STOCKITEM_MANAGEMENT', 'STOCKITEM_MANAGEMENT', 'Permesso per la gestione del magazzino materiali'),
	(32, 'NOTES_MANAGEMENT', 'NOTES_MANAGEMENT', 'Permesso per la gestione delle note'),
	(33, 'MAINTENANCE_MANAGEMENT', 'MAINTENANCE_MANAGEMENT', 'Permesso per la gestione degli interventi di manutenzione');
/*!40000 ALTER TABLE `permission` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.sessionlog
CREATE TABLE IF NOT EXISTS `sessionlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SessionId` text NOT NULL,
  `AccessToken` text NOT NULL,
  `RefreshToken` text NOT NULL,
  `UserName` varchar(400) CHARACTER SET utf8mb3 NOT NULL,
  `UserId` int(11) NOT NULL,
  `MachineName` varchar(100) NOT NULL,
  `IssuedAt` datetime NOT NULL DEFAULT current_timestamp(),
  `LoggedOutAt` datetime DEFAULT NULL,
  `ExpiredOn` datetime DEFAULT NULL,
  `RefreshExpiredOn` datetime DEFAULT NULL,
  `ForceLogOut` bit(1) NOT NULL DEFAULT b'0',
  `SessionInfo` text DEFAULT NULL,
  `Culture` varchar(10) NOT NULL DEFAULT 'it-IT',
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `IDX_SessionLog_SessionLog_UserId` (`UserId`),
  KEY `IDX_SessionLog_Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=272 DEFAULT CHARSET=latin1;

-- Dump della struttura di tabella accounting.slavegroup
CREATE TABLE IF NOT EXISTS `slavegroup` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MasterId` int(11) NOT NULL,
  `SlaveId` int(11) NOT NULL,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT 'MITROL',
  `UpdatedOn` datetime DEFAULT current_timestamp(),
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `IX_SlaveGroup_MasterId_SlaveId` (`MasterId`,`SlaveId`),
  KEY `IX_SlaveGroup_SlaveId` (`SlaveId`),
  CONSTRAINT `FK_SG_Group` FOREIGN KEY (`SlaveId`) REFERENCES `group` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.slavegroup: ~6 rows (circa)
/*!40000 ALTER TABLE `slavegroup` DISABLE KEYS */;
REPLACE INTO `slavegroup` (`Id`, `MasterId`, `SlaveId`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) VALUES
	(1, 1, 4, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(2, 2, 3, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(3, 1, 3, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(4, 2, 2, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(5, 1, 2, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(6, 1, 1, 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time');
/*!40000 ALTER TABLE `slavegroup` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.user
CREATE TABLE IF NOT EXISTS `user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) CHARACTER SET utf8mb3 NOT NULL,
  `LastName` varchar(200) CHARACTER SET utf8mb3 NOT NULL,
  `UserName` varchar(400) CHARACTER SET utf8mb3 NOT NULL,
  `Password` text NOT NULL,
  `DefaultCulture` text NOT NULL DEFAULT 'it-IT',
  `UserStatusId` int(11) NOT NULL DEFAULT 1,
  `IsSystemUser` bit(1) NOT NULL DEFAULT b'0',
  `ConversionSystem` int(11) DEFAULT 1,
  `MenuPosition` int(11) DEFAULT 1,
  `WidgetConfiguration` blob DEFAULT NULL,
  `CreatedBy` varchar(32) CHARACTER SET utf8mb3 NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) CHARACTER SET utf8mb3 DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `FK_UserStatus` (`UserStatusId`),
  CONSTRAINT `FK_UserStatus` FOREIGN KEY (`UserStatusId`) REFERENCES `userstatus` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.user: ~9 rows (circa)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
REPLACE INTO `user` (`Id`, `FirstName`, `LastName`, `UserName`, `Password`, `DefaultCulture`, `UserStatusId`, `IsSystemUser`, `ConversionSystem`, `MenuPosition`, `WidgetConfiguration`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) VALUES
	(1, 'boot', 'user', 'boot.user', 'esImz+LhP3iMn1+MI3RJIg==', 'it-IT', 1, b'1', 1, 1, NULL, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(2, 'admin', 'FICEP', 'admin', '/Lj3AhOUtKcSqbj+f64Jzg==', 'it-IT', 1, b'1', 1, 1, _binary 0x5b007b002200540079007000650022003a003500300030002c002200500061006700650022003a0031002c00220052006f00770022003a0031002c00220043006f006c0075006d006e0022003a0031002c0022005700690064007400680022003a0031002c00220048006500690067006800740022003a0032007d002c007b002200540079007000650022003a003400300030002c002200500061006700650022003a0031002c00220052006f00770022003a0031002c00220043006f006c0075006d006e0022003a0032002c0022005700690064007400680022003a0031002c00220048006500690067006800740022003a0032007d002c007b002200540079007000650022003a003300310030002c002200500061006700650022003a0031002c00220052006f00770022003a0033002c00220043006f006c0075006d006e0022003a0031002c0022005700690064007400680022003a0032002c00220048006500690067006800740022003a0031007d005d00, 'MITROL', '2021-09-29 12:31:42', 'admin', '2021-10-04 08:47:40', 'W. Europe Standard Time'),
	(3, 'super', 'user', 'super', '/Lj3AhOUtKcSqbj+f64Jzg==', 'it-IT', 1, b'1', 1, 1, NULL, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(4, 'user', 'user', 'user', '/Lj3AhOUtKcSqbj+f64Jzg==', 'it-IT', 1, b'1', 1, 1, NULL, 'MITROL', '2021-09-29 12:31:42', NULL, NULL, 'W. Europe Standard Time'),
	(5, 'STEEL', 'PROJECT', 'steel.project', 'i5qrJByD9FDj5oN75IetMg==', 'fr-FR', 1, b'1', 0, 0, NULL, 'boot.user', '2021-09-29 14:08:22', 'boot.user', '2021-09-29 14:08:22', 'W. Europe Standard Time'),
	(6, 'Giovanni', 'D\'Antonio', 'giovanni.dantonio', '1Q/h0raf9H7CmPDVYSrEEA==', 'en-GB', 1, b'0', 0, 0, _binary 0x5b007b002200540079007000650022003a003200300030002c002200500061006700650022003a0031002c00220052006f00770022003a0031002c00220043006f006c0075006d006e0022003a0031002c0022005700690064007400680022003a0032002c00220048006500690067006800740022003a0031002c0022004c0069006d006900740022003a0034007d002c007b002200540079007000650022003a003100300030002c002200500061006700650022003a0031002c00220052006f00770022003a0032002c00220043006f006c0075006d006e0022003a0031002c0022005700690064007400680022003a0032002c00220048006500690067006800740022003a0031002c0022004c0069006d006900740022003a0034002c0022005400790070006500460069006c0074006500720022003a0033007d005d00, 'admin', '2021-10-04 07:53:59', 'giovanni.dantonio', '2021-10-04 14:35:48', 'W. Europe Standard Time'),
	(7, 'Maurizio', 'Macchi', 'maurizio.macchi', '/Lj3AhOUtKcSqbj+f64Jzg==', 'it-IT', 1, b'0', 0, 0, NULL, 'admin', '2021-10-04 08:39:19', 'null.user', '2021-10-04 08:39:41', 'W. Europe Standard Time'),
	(8, 'Simona', 'Bosetti', 'simona.bosetti', '/Lj3AhOUtKcSqbj+f64Jzg==', 'it-IT', 1, b'0', 0, 0, NULL, 'giovanni.dantonio', '2021-10-04 13:27:14', 'giovanni.dantonio', '2021-10-04 13:27:14', 'W. Europe Standard Time'),
	(9, 'Jean AndrÃ¨ Marcel', 'Debembecher', 'jeanandrÃ¨marcel.debembecher', '/Lj3AhOUtKcSqbj+f64Jzg==', 'fr-FR', 1, b'0', 0, 0, _binary 0x5b007b002200540079007000650022003a003100300030002c002200500061006700650022003a0031002c00220052006f00770022003a0031002c00220043006f006c0075006d006e0022003a0031002c0022005700690064007400680022003a0032002c00220048006500690067006800740022003a0032002c0022004c0069006d006900740022003a0036002c0022005400790070006500460069006c0074006500720022003a0033007d005d00, 'giovanni.dantonio', '2021-10-04 13:28:18', 'admin', '2021-10-06 13:40:25', 'W. Europe Standard Time');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.usergroup
CREATE TABLE IF NOT EXISTS `usergroup` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) DEFAULT NULL,
  `GroupId` int(11) DEFAULT NULL,
  `IsEnabled` bit(1) NOT NULL DEFAULT b'1',
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) DEFAULT 'MITROL',
  `UpdatedOn` datetime DEFAULT current_timestamp(),
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `FK_UG_Group` (`GroupId`),
  KEY `IDX_UserGroup_UserId_GroupId` (`UserId`,`GroupId`),
  CONSTRAINT `FK_UG_Group` FOREIGN KEY (`GroupId`) REFERENCES `group` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FK_UG_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.usergroup: ~9 rows (circa)
/*!40000 ALTER TABLE `usergroup` DISABLE KEYS */;
REPLACE INTO `usergroup` (`Id`, `UserId`, `GroupId`, `IsEnabled`, `CreatedBy`, `CreatedOn`, `UpdatedBy`, `UpdatedOn`, `TimeZoneId`) VALUES
	(1, 1, 4, b'1', 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(2, 2, 1, b'1', 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(3, 3, 2, b'1', 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(4, 4, 3, b'1', 'MITROL', '2021-09-29 12:31:42', 'MITROL', '2021-09-29 12:31:42', 'W. Europe Standard Time'),
	(5, 5, 1, b'0', 'boot.user', '2021-09-29 14:08:22', 'boot.user', '2021-09-29 14:08:22', 'W. Europe Standard Time'),
	(6, 6, 1, b'0', 'admin', '2021-10-04 07:53:59', 'admin', '2021-10-04 07:53:59', 'W. Europe Standard Time'),
	(7, 7, 1, b'0', 'admin', '2021-10-04 08:39:19', 'admin', '2021-10-04 08:39:19', 'W. Europe Standard Time'),
	(8, 8, 1, b'0', 'giovanni.dantonio', '2021-10-04 13:27:14', 'giovanni.dantonio', '2021-10-04 13:27:14', 'W. Europe Standard Time'),
	(9, 9, 1, b'0', 'giovanni.dantonio', '2021-10-04 13:28:18', 'giovanni.dantonio', '2021-10-04 13:28:18', 'W. Europe Standard Time');
/*!40000 ALTER TABLE `usergroup` ENABLE KEYS */;

-- Dump della struttura di tabella accounting.userstatus
CREATE TABLE IF NOT EXISTS `userstatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(32) NOT NULL,
  `Description` text NOT NULL,
  `DisplayName` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserStatus_IDX_DisplayName` (`DisplayName`),
  KEY `UserStatus_IDX_Code` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella accounting.userstatus: ~3 rows (circa)
/*!40000 ALTER TABLE `userstatus` DISABLE KEYS */;
REPLACE INTO `userstatus` (`Id`, `Code`, `Description`, `DisplayName`) VALUES
	(1, 'Active', 'Utente in stato attivo', 'A'),
	(2, 'Locked', 'Utente lockato', 'L'),
	(4, 'Disabled', 'Utente disattivato', 'D');
/*!40000 ALTER TABLE `userstatus` ENABLE KEYS */;

-- Dump della struttura di vista accounting.latestloginview
-- Creazione di una tabella temporanea per risolvere gli errori di dipendenza della vista

CREATE OR REPLACE VIEW `latestloginview` AS SELECT u.FirstName, u.LastName, u.UserName, u.DefaultCulture, g.Code as GroupCode, sl.UserId, sl.IssuedAt, sl.TimeZoneId, sl.MachineName
	FROM SessionLog sl
	INNER JOIN User u ON u.Id = sl.UserId
	INNER JOIN UserGroup ug ON ug.UserId = u.Id
	INNER JOIN `Group` g ON g.Id = ug.GroupId
	WHERE LoggedOutAt IS NOT NULL
	AND LOWER(sl.UserName) <> 'boot.user' ;

-- Dump della struttura di vista accounting.usersview
-- Rimozione temporanea di tabella e creazione della struttura finale della vista
CREATE OR REPLACE VIEW `usersview` AS WITH LastAccess AS (
SELECT UserId, IssuedAt as LastAccess FROM SessionLog
GROUP BY UserId)
SELECT u.Id, u.FirstName, u.LastName,u.DefaultCulture, u.UserName,
    u.UserStatusId, la.LastAccess, u.IsSystemUser, u.CreatedOn, u.UpdatedOn, ug.GroupId, g.Code as GroupCode, g.Description as GroupDescription
FROM
    User u
	LEFT JOIN LastAccess la ON la.UserId = u.Id
	INNER JOIN UserGroup ug ON ug.UserId = u.Id
	INNER JOIN `Group` g ON g.Id = ug.GroupId ;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
