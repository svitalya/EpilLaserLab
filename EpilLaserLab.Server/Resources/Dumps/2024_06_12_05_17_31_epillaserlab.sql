-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-06-12 05:17:31
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
INSERT INTO `__efmigrationshistory`(`MigrationId`,`ProductVersion`) VALUES('20240408121422_CreateUsers','8.0.3'),('20240411151310_CreateStatuses','8.0.3'),('20240411155449_UpdateStatuses','8.0.3'),('20240413161533_CreateTags','8.0.3'),('20240414065234_UpdateRoles','8.0.3'),('20240414065402_UpdateRoles2','8.0.3'),('20240414110524_CreateCategory','8.0.3'),('20240414124734_CreateType','8.0.3'),('20240414142247_CreateServices','8.0.3'),('20240414191749_CreateServicePrices','8.0.3'),('20240415121806_CreateSeasonTickets','8.0.3'),('20240415151415_CreateSeasonTicketsPrice','8.0.3'),('20240415192809_CreateEmployees','8.0.3'),('20240415192923_CreateBranches','8.0.3'),('20240416160525_CreateMasters','8.0.3'),('20240417025400_CreateSchedules','8.0.3'),('20240417041115_UpdateSchedule','8.0.3'),('20240417061733_CreateIntervals','8.0.3'),('20240417081719_CreateClients','8.0.3'),('20240417145226_UpdateRoles3','8.0.3'),('20240418155742_CreateAdmins','8.0.3'),('20240418164710_UpdateRoles4','8.0.3'),('20240418170302_UpdateAdmin2','8.0.3'),('20240419015422_CreatePurchasedSeasonTickets','8.0.3'),('20240419023257_UpdateApplications','8.0.3'),('20240419115234_UpdateApplications2','8.0.3'),('20240517074059_UpdateClients','8.0.3'),('20240531040526_DropTagsAndStatuses','8.0.3');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table branches
-- 

/*!40000 ALTER TABLE `branches` DISABLE KEYS */;
INSERT INTO `branches`(`BranchId`,`Address`,`PhotoPath`) VALUES(1,'ул Пушкина 23','2024_06_05_07_21_20_481.jpg'),(2,'ул Карл Маркса 53','2024_06_10_06_56_59_358.jpg'),(3,'ул Ленина 23','2024_06_10_06_57_22_284.jpg');
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table categories
-- 

/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories`(`CategoryId`,`Name`) VALUES(3,'Завершено'),(5,'Клиент не пришел'),(1,'Создано пользователем');
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table employees
-- 

/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees`(`EmployeeId`,`Surname`,`Name`,`Patronymic`,`IsWork`) VALUES(1,'Сергеев','Иван',NULL,1),(2,'Иванов','Иван','Иванович',1),(3,'Петров','Петр','Петрович',1),(4,'Фамилия','Имя',NULL,1),(5,'Биценко','Максим','Сергеевич',1),(6,'Юношев','Николай','',1),(7,'Мощалгин','Михаил','',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table masters
-- 

/*!40000 ALTER TABLE `masters` DISABLE KEYS */;
INSERT INTO `masters`(`MasterId`,`EmployeeId`,`BranchId`,`PhotoPath`) VALUES(1,1,1,'2024_06_05_07_21_58_493.jpg'),(2,3,3,'2024_06_10_06_59_28_800.jpg'),(3,4,2,'2024_06_10_06_59_52_709.jpg'),(4,5,2,'2024_06_10_07_00_30_303.jpg');
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table schedules
-- 

/*!40000 ALTER TABLE `schedules` DISABLE KEYS */;
INSERT INTO `schedules`(`ScheduleId`,`MasterId`,`Date`) VALUES(1,1,'2024-06-05 00:00:00.000000'),(2,1,'2024-06-06 00:00:00.000000'),(3,1,'2024-06-07 00:00:00.000000'),(4,1,'2024-06-08 00:00:00.000000'),(5,1,'2024-06-09 00:00:00.000000'),(6,1,'2024-06-10 00:00:00.000000'),(7,1,'2024-06-11 00:00:00.000000'),(8,1,'2024-06-12 00:00:00.000000'),(9,2,'2024-06-10 00:00:00.000000'),(10,3,'2024-06-10 00:00:00.000000'),(11,4,'2024-06-10 00:00:00.000000');
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
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table intervals
-- 

/*!40000 ALTER TABLE `intervals` DISABLE KEYS */;
INSERT INTO `intervals`(`IntervalId`,`ScheduleId`,`TimeStart`,`TimeEnd`) VALUES(4,4,540,1080),(5,1,600,1080),(6,1,540,600),(7,3,580,1080),(8,3,540,580),(9,2,580,1080),(10,2,540,580),(13,5,540,580),(15,5,580,620),(16,5,660,1080),(17,5,620,660),(18,6,540,1080),(19,7,540,1080),(20,8,540,1080),(21,9,540,1080),(23,11,540,1080),(25,10,540,580),(27,10,580,620),(28,10,660,1080),(29,10,620,660);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table services
-- 

/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services`(`ServiceId`,`Name`,`Description`,`TimeCost`) VALUES(1,'Зона S','Описание',40),(2,'Зона M','',50),(3,'Зона L','',60);
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table seasontickets
-- 

/*!40000 ALTER TABLE `seasontickets` DISABLE KEYS */;
INSERT INTO `seasontickets`(`SeasonTicketId`,`ServiceId`,`Sessions`,`ValidityPeriod`) VALUES(1,3,3,30),(2,3,5,30),(3,3,7,30),(4,2,3,30),(5,2,5,30),(6,2,7,30);
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table seasonticketsprice
-- 

/*!40000 ALTER TABLE `seasonticketsprice` DISABLE KEYS */;
INSERT INTO `seasonticketsprice`(`SeasonTicketPriceId`,`SeasonTicketId`,`Price`,`DateTime`) VALUES(1,1,15200.00,'2024-06-05 07:17:48.697523'),(2,1,11100.00,'2024-06-05 07:18:08.669805'),(3,1,33300.00,'2024-06-05 07:18:26.188809'),(4,2,53500.00,'2024-06-05 07:18:43.057296'),(5,3,67200.00,'2024-06-05 07:19:03.119937'),(6,4,42200.00,'2024-06-05 07:19:32.607196'),(7,5,67500.00,'2024-06-05 07:20:02.320972'),(8,6,83300.00,'2024-06-05 07:20:22.244652');
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
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table purchasedseasontickets
-- 

/*!40000 ALTER TABLE `purchasedseasontickets` DISABLE KEYS */;
INSERT INTO `purchasedseasontickets`(`PurchasedSeasonTicketId`,`SeasonTicketPriceId`,`ClientId`,`Remains`,`DateOfPurchased`,`DateOfCombustion`) VALUES(15,5,1,7,'2024-06-09 00:00:00.000000','2024-07-10 00:00:00.000000'),(16,3,2,3,'2024-06-10 00:00:00.000000','2024-07-11 00:00:00.000000'),(17,7,1,5,'2024-06-10 00:00:00.000000','2024-07-11 00:00:00.000000'),(18,6,7,3,'2024-06-10 00:00:00.000000','2024-07-11 00:00:00.000000');
/*!40000 ALTER TABLE `purchasedseasontickets` ENABLE KEYS */;

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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table serviceprices
-- 

/*!40000 ALTER TABLE `serviceprices` DISABLE KEYS */;
INSERT INTO `serviceprices`(`ServicePriceId`,`ServiceId`,`TypeId`,`DateTime`,`Price`) VALUES(1,1,1,'2024-06-05 07:15:11.142759',1800.00),(2,1,4,'2024-06-05 07:15:11.142806',3600.00),(3,2,1,'2024-06-05 07:16:09.959624',6300.00),(4,2,4,'2024-06-05 07:16:09.963019',12000.00),(5,3,1,'2024-06-05 07:16:27.334502',8800.00),(6,3,4,'2024-06-05 07:16:27.336238',16800.00),(7,1,4,'2024-06-09 10:10:39.454714',3699.00);
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table applications
-- 

/*!40000 ALTER TABLE `applications` DISABLE KEYS */;
INSERT INTO `applications`(`ApplicationId`,`IntervalId`,`CategoryId`,`ServicePriceId`,`ClientId`,`PurchasedSeasonTicketId`,`DateTimeCreated`,`DateTimeClosed`,`PrepaymentPercentage`) VALUES(1,6,1,5,1,NULL,'2024-05-09 11:07:55.565239','2024-05-09 11:07:55.565239',NULL),(2,8,1,2,2,NULL,'2024-06-06 07:01:19.141750','2024-06-06 07:01:19.141750',NULL),(3,10,1,1,3,NULL,'2024-06-06 07:31:33.554167','2024-06-06 07:31:33.554167',NULL),(4,13,5,1,6,NULL,'2024-06-09 10:15:20.555827','2024-06-09 10:15:20.555827',NULL),(5,15,5,1,1,NULL,'2024-06-09 10:21:48.415796','2024-06-09 10:21:48.415796',NULL),(6,17,5,1,7,NULL,'2024-06-09 10:22:01.816740','2024-06-09 10:22:01.816740',NULL),(7,25,3,1,2,NULL,'2024-06-10 07:03:11.851706',NULL,NULL),(8,27,3,1,1,NULL,'2024-06-10 07:03:23.594188',NULL,NULL),(9,29,3,1,3,NULL,'2024-06-10 07:03:35.392134',NULL,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table users
-- 

/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users`(`UserId`,`Login`,`PasswordHash`,`Email`,`RoleId`) VALUES(1,'Admin','$2a$11$mifR/K8TVeTfGSh5v80Tv..X4VRdPuw7l5LNnw8UafXq6d6NMwxki',NULL,1),(3,'Ivan','$2a$11$Am8uD6sMmYE8IJ0Wnq03GO/gfHeDHyQw88VgzhiJKt02qtxQ6NJly',NULL,2),(4,'KarlMarksa','$2a$11$zBe2r9oyWwtQL.pZ8kKknuAecO/l0aZ6QGNM4rvbqcKkirqcDjyQ2',NULL,2),(5,'Lenina','$2a$11$p66n8DKXSLQ/KD3khrFl3O4x7lfRlEEdDgDXFVwJBm4iHptNaNIBC',NULL,2);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table admins
-- 

/*!40000 ALTER TABLE `admins` DISABLE KEYS */;
INSERT INTO `admins`(`AdminId`,`BranchId`,`EmployeeId`,`UserId`) VALUES(1,1,2,3),(2,2,6,4),(3,3,7,5);
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table clients
-- 

/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients`(`ClientId`,`Name`,`Phone`,`UserId`) VALUES(1,'Игорь','79000000001',NULL),(2,'Виталий','79000000002',NULL),(3,'Влад','79000000003',NULL),(4,'Ян','79000000004',NULL),(5,'Петр','79000000005',NULL),(6,'Иван','79000000006',NULL),(7,'Михаил','79000000007',NULL);
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2024-06-12 05:17:31
-- Total time: 0:0:0:0:130 (d:h:m:s:ms)
