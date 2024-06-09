-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-05-28 07:36:42
-- --------------------------------------
-- Server version 8.3.0 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of __efmigrationshistory
-- 

DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table __efmigrationshistory
-- 

/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory`(`MigrationId`,`ProductVersion`) VALUES('20240408121422_CreateUsers','8.0.3'),('20240411151310_CreateStatuses','8.0.3'),('20240411155449_UpdateStatuses','8.0.3'),('20240413161533_CreateTags','8.0.3'),('20240414065234_UpdateRoles','8.0.3'),('20240414065402_UpdateRoles2','8.0.3'),('20240414110524_CreateCategory','8.0.3'),('20240414124734_CreateType','8.0.3'),('20240414142247_CreateServices','8.0.3'),('20240414191749_CreateServicePrices','8.0.3'),('20240415121806_CreateSeasonTickets','8.0.3'),('20240415151415_CreateSeasonTicketsPrice','8.0.3'),('20240415192809_CreateEmployees','8.0.3'),('20240415192923_CreateBranches','8.0.3'),('20240416160525_CreateMasters','8.0.3'),('20240417025400_CreateSchedules','8.0.3'),('20240417041115_UpdateSchedule','8.0.3'),('20240417061733_CreateIntervals','8.0.3'),('20240417081719_CreateClients','8.0.3'),('20240417145226_UpdateRoles3','8.0.3'),('20240418155742_CreateAdmins','8.0.3'),('20240418164710_UpdateRoles4','8.0.3'),('20240418170302_UpdateAdmin2','8.0.3'),('20240419015422_CreatePurchasedSeasonTickets','8.0.3'),('20240419023257_UpdateApplications','8.0.3'),('20240419115234_UpdateApplications2','8.0.3'),('20240517074059_UpdateClients','8.0.3');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

-- 
-- Definition of branches
-- 

DROP TABLE IF EXISTS `branches`;
CREATE TABLE IF NOT EXISTS `branches` (
  `BranchId` int NOT NULL AUTO_INCREMENT,
  `Address` longtext NOT NULL,
  `PhotoPath` longtext NOT NULL,
  PRIMARY KEY (`BranchId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table branches
-- 

/*!40000 ALTER TABLE `branches` DISABLE KEYS */;
INSERT INTO `branches`(`BranchId`,`Address`,`PhotoPath`) VALUES(11,'Тест','2024_04_17_04_57_36_595.jpg'),(12,'Тест2','2024_04_17_05_03_27_975.jpg');
/*!40000 ALTER TABLE `branches` ENABLE KEYS */;

-- 
-- Definition of categories
-- 

DROP TABLE IF EXISTS `categories`;
CREATE TABLE IF NOT EXISTS `categories` (
  `CategoryId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`CategoryId`),
  UNIQUE KEY `IX_Categories_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table categories
-- 

/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories`(`CategoryId`,`Name`) VALUES(3,'ghfgh'),(1,'Создано пользователем');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;

-- 
-- Definition of employees
-- 

DROP TABLE IF EXISTS `employees`;
CREATE TABLE IF NOT EXISTS `employees` (
  `EmployeeId` int NOT NULL AUTO_INCREMENT,
  `Surname` longtext NOT NULL,
  `Name` longtext NOT NULL,
  `Patronymic` longtext,
  `IsWork` tinyint(1) NOT NULL,
  PRIMARY KEY (`EmployeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table employees
-- 

/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees`(`EmployeeId`,`Surname`,`Name`,`Patronymic`,`IsWork`) VALUES(8,'Тестов','Тест','Тестович',1),(9,'Тест2','Тест','Тест',1),(10,'ывталыова','ывталыова','ывталыова',1),(11,'Шпагин','Виталий','Евгеньевич',1),(12,'Шпагин','Виталий','Евгеньевич',1),(13,'Шпагин','Виталий','Евгеньевич',1),(14,'Шпагин','Виталий','Евгеньевич',1);
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;

-- 
-- Definition of masters
-- 

DROP TABLE IF EXISTS `masters`;
CREATE TABLE IF NOT EXISTS `masters` (
  `MasterId` int NOT NULL AUTO_INCREMENT,
  `EmployeeId` int NOT NULL,
  `BranchId` int NOT NULL,
  `PhotoPath` longtext NOT NULL,
  PRIMARY KEY (`MasterId`),
  UNIQUE KEY `IX_Masters_EmployeeId` (`EmployeeId`),
  KEY `IX_Masters_BranchId` (`BranchId`),
  CONSTRAINT `FK_Masters_Branches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `branches` (`BranchId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Masters_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`EmployeeId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table masters
-- 

/*!40000 ALTER TABLE `masters` DISABLE KEYS */;
INSERT INTO `masters`(`MasterId`,`EmployeeId`,`BranchId`,`PhotoPath`) VALUES(9,8,12,'2024_04_17_05_38_31_457.jpg'),(10,9,12,'2024_04_17_05_22_35_366.jpg');
/*!40000 ALTER TABLE `masters` ENABLE KEYS */;

-- 
-- Definition of roles
-- 

DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `RoleId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Title` longtext NOT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table roles
-- 

/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles`(`RoleId`,`Name`,`Title`) VALUES(1,'root','Главный'),(2,'admin','Администратор'),(3,'client','Клиент');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;

-- 
-- Definition of schedules
-- 

DROP TABLE IF EXISTS `schedules`;
CREATE TABLE IF NOT EXISTS `schedules` (
  `ScheduleId` int NOT NULL AUTO_INCREMENT,
  `MasterId` int NOT NULL,
  `Date` datetime(6) NOT NULL,
  PRIMARY KEY (`ScheduleId`),
  KEY `IX_Schedules_MasterId` (`MasterId`),
  CONSTRAINT `FK_Schedules_Masters_MasterId` FOREIGN KEY (`MasterId`) REFERENCES `masters` (`MasterId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table schedules
-- 

/*!40000 ALTER TABLE `schedules` DISABLE KEYS */;
INSERT INTO `schedules`(`ScheduleId`,`MasterId`,`Date`) VALUES(10,9,'2024-04-18 00:00:00.000000'),(11,9,'2024-04-27 00:00:00.000000'),(12,9,'2024-05-02 00:00:00.000000'),(13,10,'2024-04-20 00:00:00.000000'),(14,9,'2024-04-23 00:00:00.000000'),(15,10,'2024-04-23 00:00:00.000000'),(16,9,'2024-04-24 00:00:00.000000'),(17,10,'2024-04-24 00:00:00.000000'),(18,9,'2024-05-17 00:00:00.000000');
/*!40000 ALTER TABLE `schedules` ENABLE KEYS */;

-- 
-- Definition of intervals
-- 

DROP TABLE IF EXISTS `intervals`;
CREATE TABLE IF NOT EXISTS `intervals` (
  `IntervalId` int NOT NULL AUTO_INCREMENT,
  `ScheduleId` int NOT NULL,
  `TimeStart` int NOT NULL,
  `TimeEnd` int NOT NULL,
  PRIMARY KEY (`IntervalId`),
  KEY `IX_Intervals_ScheduleId` (`ScheduleId`),
  CONSTRAINT `FK_Intervals_Schedules_ScheduleId` FOREIGN KEY (`ScheduleId`) REFERENCES `schedules` (`ScheduleId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table intervals
-- 

/*!40000 ALTER TABLE `intervals` DISABLE KEYS */;
INSERT INTO `intervals`(`IntervalId`,`ScheduleId`,`TimeStart`,`TimeEnd`) VALUES(7,11,540,720),(8,11,875,900),(14,10,660,720),(15,10,780,940),(18,12,540,599),(19,12,631,1080),(20,12,600,630),(23,13,540,570),(24,13,571,599),(25,13,631,1080),(26,13,600,630),(27,15,540,1080),(28,14,540,1080),(32,17,540,570),(33,17,571,599),(34,17,641,1080),(35,17,600,640),(36,16,540,599),(38,16,600,630),(39,16,662,1080),(40,16,631,661),(48,18,540,570),(50,18,570,600),(52,18,990,1080),(53,18,960,990),(56,18,630,660),(57,18,600,630),(58,18,660,700),(59,18,740,960),(60,18,700,740);
/*!40000 ALTER TABLE `intervals` ENABLE KEYS */;

-- 
-- Definition of services
-- 

DROP TABLE IF EXISTS `services`;
CREATE TABLE IF NOT EXISTS `services` (
  `ServiceId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` longtext,
  `TimeCost` int unsigned NOT NULL,
  PRIMARY KEY (`ServiceId`),
  UNIQUE KEY `IX_Services_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table services
-- 

/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services`(`ServiceId`,`Name`,`Description`,`TimeCost`) VALUES(18,'Тест1','Тест тест тест',30),(19,'Тест','Тест',40);
/*!40000 ALTER TABLE `services` ENABLE KEYS */;

-- 
-- Definition of seasontickets
-- 

DROP TABLE IF EXISTS `seasontickets`;
CREATE TABLE IF NOT EXISTS `seasontickets` (
  `SeasonTicketId` int NOT NULL AUTO_INCREMENT,
  `ServiceId` int NOT NULL,
  `Sessions` int NOT NULL,
  `ValidityPeriod` int NOT NULL,
  PRIMARY KEY (`SeasonTicketId`),
  KEY `IX_SeasonTickets_ServiceId` (`ServiceId`),
  CONSTRAINT `FK_SeasonTickets_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`ServiceId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table seasontickets
-- 

/*!40000 ALTER TABLE `seasontickets` DISABLE KEYS */;
INSERT INTO `seasontickets`(`SeasonTicketId`,`ServiceId`,`Sessions`,`ValidityPeriod`) VALUES(13,19,3,30),(14,19,5,30),(15,19,7,30),(16,18,3,30),(17,18,5,30),(18,18,7,30);
/*!40000 ALTER TABLE `seasontickets` ENABLE KEYS */;

-- 
-- Definition of seasonticketsprice
-- 

DROP TABLE IF EXISTS `seasonticketsprice`;
CREATE TABLE IF NOT EXISTS `seasonticketsprice` (
  `SeasonTicketPriceId` int NOT NULL AUTO_INCREMENT,
  `SeasonTicketId` int NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`SeasonTicketPriceId`),
  KEY `IX_SeasonTicketsPrice_SeasonTicketId` (`SeasonTicketId`),
  CONSTRAINT `FK_SeasonTicketsPrice_SeasonTickets_SeasonTicketId` FOREIGN KEY (`SeasonTicketId`) REFERENCES `seasontickets` (`SeasonTicketId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table seasonticketsprice
-- 

/*!40000 ALTER TABLE `seasonticketsprice` DISABLE KEYS */;
INSERT INTO `seasonticketsprice`(`SeasonTicketPriceId`,`SeasonTicketId`,`Price`,`DateTime`) VALUES(14,13,1000.00,'2024-05-14 04:38:29.251830'),(15,14,3000.00,'2024-05-14 04:38:41.598593'),(16,15,3000.00,'2024-05-14 04:39:02.406974'),(17,16,123.00,'2024-05-14 04:39:14.813596'),(18,17,1232.00,'2024-05-14 04:39:39.468859'),(19,18,1232.00,'2024-05-14 04:39:55.972533');
/*!40000 ALTER TABLE `seasonticketsprice` ENABLE KEYS */;

-- 
-- Definition of purchasedseasontickets
-- 

DROP TABLE IF EXISTS `purchasedseasontickets`;
CREATE TABLE IF NOT EXISTS `purchasedseasontickets` (
  `PurchasedSeasonTicketId` int NOT NULL AUTO_INCREMENT,
  `SeasonTicketPriceId` int NOT NULL,
  `ClientId` int NOT NULL,
  `Remains` int NOT NULL,
  `DateOfPurchased` datetime(6) NOT NULL,
  `DateOfCombustion` datetime(6) NOT NULL,
  PRIMARY KEY (`PurchasedSeasonTicketId`),
  KEY `IX_PurchasedSeasonTickets_ClientId` (`ClientId`),
  KEY `IX_PurchasedSeasonTickets_SeasonTicketPriceId` (`SeasonTicketPriceId`),
  CONSTRAINT `FK_PurchasedSeasonTickets_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`ClientId`) ON DELETE CASCADE,
  CONSTRAINT `FK_PurchasedSeasonTickets_SeasonTicketsPrice_SeasonTicketPriceId` FOREIGN KEY (`SeasonTicketPriceId`) REFERENCES `seasonticketsprice` (`SeasonTicketPriceId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table purchasedseasontickets
-- 

/*!40000 ALTER TABLE `purchasedseasontickets` DISABLE KEYS */;

/*!40000 ALTER TABLE `purchasedseasontickets` ENABLE KEYS */;

-- 
-- Definition of statuses
-- 

DROP TABLE IF EXISTS `statuses`;
CREATE TABLE IF NOT EXISTS `statuses` (
  `StatusId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`StatusId`),
  UNIQUE KEY `IX_Statuses_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=230 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table statuses
-- 

/*!40000 ALTER TABLE `statuses` DISABLE KEYS */;
INSERT INTO `statuses`(`StatusId`,`Name`) VALUES(228,'fghfgh'),(229,'rtert'),(209,'Тест'),(211,'Тест2'),(213,'Тест3'),(214,'Тест4'),(215,'Тест5'),(227,'Тест7'),(222,'ываыва');
/*!40000 ALTER TABLE `statuses` ENABLE KEYS */;

-- 
-- Definition of tags
-- 

DROP TABLE IF EXISTS `tags`;
CREATE TABLE IF NOT EXISTS `tags` (
  `TagId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`TagId`),
  UNIQUE KEY `IX_Tags_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table tags
-- 

/*!40000 ALTER TABLE `tags` DISABLE KEYS */;
INSERT INTO `tags`(`TagId`,`Name`) VALUES(14,'56756'),(11,'Тест1'),(13,'Тест5');
/*!40000 ALTER TABLE `tags` ENABLE KEYS */;

-- 
-- Definition of types
-- 

DROP TABLE IF EXISTS `types`;
CREATE TABLE IF NOT EXISTS `types` (
  `TypeId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`TypeId`),
  UNIQUE KEY `IX_Type_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table types
-- 

/*!40000 ALTER TABLE `types` DISABLE KEYS */;
INSERT INTO `types`(`TypeId`,`Name`) VALUES(1,'Знакомство'),(4,'Стандартный');
/*!40000 ALTER TABLE `types` ENABLE KEYS */;

-- 
-- Definition of serviceprices
-- 

DROP TABLE IF EXISTS `serviceprices`;
CREATE TABLE IF NOT EXISTS `serviceprices` (
  `ServicePriceId` int NOT NULL AUTO_INCREMENT,
  `ServiceId` int NOT NULL,
  `TypeId` int NOT NULL,
  `DateTime` datetime(6) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  PRIMARY KEY (`ServicePriceId`),
  KEY `IX_ServicePrices_ServiceId` (`ServiceId`),
  KEY `IX_ServicePrices_TypeId` (`TypeId`),
  CONSTRAINT `FK_ServicePrices_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`ServiceId`) ON DELETE CASCADE,
  CONSTRAINT `FK_ServicePrices_Types_TypeId` FOREIGN KEY (`TypeId`) REFERENCES `types` (`TypeId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table serviceprices
-- 

/*!40000 ALTER TABLE `serviceprices` DISABLE KEYS */;
INSERT INTO `serviceprices`(`ServicePriceId`,`ServiceId`,`TypeId`,`DateTime`,`Price`) VALUES(23,18,1,'2024-04-19 15:17:57.557869',155.00),(24,19,1,'2024-04-23 09:20:21.162209',300.00),(25,18,4,'2024-05-14 04:37:43.234762',300.00),(26,19,4,'2024-05-14 04:37:50.324060',600.00);
/*!40000 ALTER TABLE `serviceprices` ENABLE KEYS */;

-- 
-- Definition of applications
-- 

DROP TABLE IF EXISTS `applications`;
CREATE TABLE IF NOT EXISTS `applications` (
  `ApplicationId` int NOT NULL AUTO_INCREMENT,
  `IntervalId` int NOT NULL,
  `CategoryId` int NOT NULL,
  `ServicePriceId` int NOT NULL,
  `ClientId` int NOT NULL,
  `PurchasedSeasonTicketId` int DEFAULT NULL,
  `DateTimeCreated` datetime(6) NOT NULL,
  `DateTimeClosed` datetime(6) DEFAULT NULL,
  `PrepaymentPercentage` int DEFAULT NULL,
  PRIMARY KEY (`ApplicationId`),
  UNIQUE KEY `IX_Applications_IntervalId` (`IntervalId`),
  KEY `IX_Applications_CategoryId` (`CategoryId`),
  KEY `IX_Applications_ClientId` (`ClientId`),
  KEY `IX_Applications_PurchasedSeasonTicketId` (`PurchasedSeasonTicketId`),
  KEY `IX_Applications_ServicePriceId` (`ServicePriceId`),
  CONSTRAINT `FK_Applications_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`CategoryId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Applications_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`ClientId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Applications_Intervals_IntervalId` FOREIGN KEY (`IntervalId`) REFERENCES `intervals` (`IntervalId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Applications_PurchasedSeasonTickets_PurchasedSeasonTicketId` FOREIGN KEY (`PurchasedSeasonTicketId`) REFERENCES `purchasedseasontickets` (`PurchasedSeasonTicketId`),
  CONSTRAINT `FK_Applications_ServicePrices_ServicePriceId` FOREIGN KEY (`ServicePriceId`) REFERENCES `serviceprices` (`ServicePriceId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table applications
-- 

/*!40000 ALTER TABLE `applications` DISABLE KEYS */;
INSERT INTO `applications`(`ApplicationId`,`IntervalId`,`CategoryId`,`ServicePriceId`,`ClientId`,`PurchasedSeasonTicketId`,`DateTimeCreated`,`DateTimeClosed`,`PrepaymentPercentage`) VALUES(1,23,1,23,3,NULL,'2024-04-19 15:27:02.816348',NULL,NULL),(2,26,1,23,3,NULL,'2024-04-19 15:28:10.559058',NULL,NULL),(3,32,1,23,3,NULL,'2024-04-23 11:27:53.293982',NULL,NULL),(4,35,1,24,3,NULL,'2024-04-23 11:28:53.709186',NULL,NULL),(5,38,1,23,3,NULL,'2024-04-23 11:50:34.132649',NULL,NULL),(6,40,1,23,3,NULL,'2024-04-23 11:51:51.430892',NULL,NULL),(8,48,1,23,3,NULL,'2024-05-17 04:11:25.086669',NULL,NULL),(9,50,1,23,3,NULL,'2024-05-17 04:13:21.370186',NULL,NULL),(10,53,1,23,3,NULL,'2024-05-17 05:04:16.711622',NULL,NULL),(11,56,1,23,4,NULL,'2024-05-17 10:45:37.162923',NULL,NULL),(12,57,1,25,3,NULL,'2024-05-17 10:49:16.803501',NULL,NULL),(13,60,1,26,5,NULL,'2024-05-17 16:23:19.766852',NULL,NULL);
/*!40000 ALTER TABLE `applications` ENABLE KEYS */;

-- 
-- Definition of users
-- 

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(64) NOT NULL,
  `PasswordHash` longtext NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `RoleId` int NOT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `IX_Users_Login` (`Login`),
  UNIQUE KEY `IX_Users_Email` (`Email`),
  KEY `IX_Users_RoleId` (`RoleId`),
  CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`RoleId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table users
-- 

/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users`(`UserId`,`Login`,`PasswordHash`,`Email`,`RoleId`) VALUES(1,'Admin','$2a$11$1RD7xY0tH0ke7Xr7B44ZKeKHM1Oxn5ZFemRvEfarLfOlATu7/oMFe','admin@admin.ru',1),(3,'test1','$2a$11$.xSU/MeZ2ZjIHPuJ3cKBHuOeSYpmVcNVWHI1jSTe17tgeGoCrSZBS',NULL,2),(8,'client1','$2a$11$BfTaz2iIKUTYibYndAqwiuOFMCRFXnkA4xqJ/hAVDfBMWkJNQJfDS',NULL,3),(10,'Admin2','$2a$11$n6mUEgfTnndYm1IWVBTphOLJ54ddhH.vqgM4Pj9uZKzsYP2tEoZHm',NULL,2),(22,'Admin4','$2a$11$dX/PukTFK/E0VeFgAM8qKedGuQRAGR4FnTU2iXG1wEwRGh/s8eCdi',NULL,2),(23,'Admin5','$2a$11$Os3vjoR7x.3lTuJOy/HVS.e2nHvy7Gk8GS7B8vI.vsp0ykQuXF7Zm',NULL,2),(24,'Admin6','$2a$11$t/yHhv3QuufjEZ.2hLJV0OdYnIu3zuXPWRRWo2DatsN39ucIpm892',NULL,2);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

-- 
-- Definition of admins
-- 

DROP TABLE IF EXISTS `admins`;
CREATE TABLE IF NOT EXISTS `admins` (
  `AdminId` int NOT NULL AUTO_INCREMENT,
  `BranchId` int NOT NULL,
  `EmployeeId` int NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`AdminId`),
  UNIQUE KEY `IX_Admins_EmployeeId` (`EmployeeId`),
  UNIQUE KEY `IX_Admins_UserId` (`UserId`),
  KEY `IX_Admins_BranchId` (`BranchId`),
  CONSTRAINT `FK_Admins_Branches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `branches` (`BranchId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Admins_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`EmployeeId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Admins_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table admins
-- 

/*!40000 ALTER TABLE `admins` DISABLE KEYS */;
INSERT INTO `admins`(`AdminId`,`BranchId`,`EmployeeId`,`UserId`) VALUES(1,11,10,3),(2,11,11,10),(3,11,12,22),(4,11,13,23),(5,11,14,24);
/*!40000 ALTER TABLE `admins` ENABLE KEYS */;

-- 
-- Definition of clients
-- 

DROP TABLE IF EXISTS `clients`;
CREATE TABLE IF NOT EXISTS `clients` (
  `ClientId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Phone` varchar(255) NOT NULL,
  `UserId` int DEFAULT NULL,
  PRIMARY KEY (`ClientId`),
  UNIQUE KEY `IX_Clients_Phone` (`Phone`),
  UNIQUE KEY `IX_Clients_UserId` (`UserId`),
  CONSTRAINT `FK_Clients_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table clients
-- 

/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients`(`ClientId`,`Name`,`Phone`,`UserId`) VALUES(3,'Тестов','89000001',8),(4,'пвапвап','12312321',NULL),(5,'апрвыаы','123123123123',NULL);
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2024-05-28 07:36:42
-- Total time: 0:0:0:0:133 (d:h:m:s:ms)
