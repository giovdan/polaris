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


-- Dump della struttura del database notes
CREATE DATABASE IF NOT EXISTS `notes` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `notes`;

-- Dump della struttura di tabella notes.auditlog
CREATE TABLE IF NOT EXISTS `auditlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TableName` text NOT NULL,
  `SessionId` text DEFAULT NULL,
  `ChangedOn` datetime NOT NULL,
  `ChangedBy` varchar(32) NOT NULL,
  `KeyValue` text NOT NULL,
  `EntityState` varchar(100) NOT NULL,
  `OldValues` text DEFAULT NULL,
  `NewValues` text DEFAULT NULL,
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`),
  KEY `IDX_AuditLog_TableName` (`TableName`(3072))
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella notes.auditlog: ~0 rows (circa)
/*!40000 ALTER TABLE `auditlog` DISABLE KEYS */;
/*!40000 ALTER TABLE `auditlog` ENABLE KEYS */;

-- Dump della struttura di tabella notes.note
CREATE TABLE IF NOT EXISTS `note` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Content` text NOT NULL,
  `ContextId` int(11) NOT NULL,
  `ContextCode` text DEFAULT NULL,
  `IsPublic` bit(1) NOT NULL DEFAULT b'1',
  `UserId` int(11) NOT NULL,
  `Username` varchar(32) NOT NULL,
  `GroupId` int(11) NOT NULL,
  `CreatedBy` varchar(32) NOT NULL DEFAULT 'MITROL',
  `CreatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `UpdatedBy` varchar(32) NOT NULL,
  `UpdatedOn` datetime NOT NULL DEFAULT current_timestamp(),
  `TimeZoneId` text DEFAULT 'W. Europe Standard Time',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella notes.note: ~0 rows (circa)
/*!40000 ALTER TABLE `note` DISABLE KEYS */;
/*!40000 ALTER TABLE `note` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
