-- MySqlBackup.NET 2.3.8.0
-- Dump Time: 2024-05-29 02:41:03
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
-- Definition of positions
-- 

DROP TABLE IF EXISTS `positions`;
CREATE TABLE IF NOT EXISTS `positions` (
  `id_Positions` int NOT NULL AUTO_INCREMENT,
  `name_Positions` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`id_Positions`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table positions
-- 

/*!40000 ALTER TABLE `positions` DISABLE KEYS */;
INSERT INTO `positions`(`id_Positions`,`name_Positions`) VALUES(8,'Наладчик оборудования'),(10,'Механик'),(11,'Электрик');
/*!40000 ALTER TABLE `positions` ENABLE KEYS */;

-- 
-- Definition of personal
-- 

DROP TABLE IF EXISTS `personal`;
CREATE TABLE IF NOT EXISTS `personal` (
  `id_Personal` int NOT NULL AUTO_INCREMENT,
  `FIO` varchar(60) DEFAULT NULL,
  `Telephone` varchar(20) DEFAULT NULL,
  `id_Positions` int DEFAULT NULL,
  PRIMARY KEY (`id_Personal`),
  KEY `fk_positions_idx` (`id_Positions`),
  CONSTRAINT `fk_positions` FOREIGN KEY (`id_Positions`) REFERENCES `positions` (`id_Positions`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table personal
-- 

/*!40000 ALTER TABLE `personal` DISABLE KEYS */;
INSERT INTO `personal`(`id_Personal`,`FIO`,`Telephone`,`id_Positions`) VALUES(7,'Юсупов И.А','89113457896',8),(8,'Григорьев А.В','89117485962',10);
/*!40000 ALTER TABLE `personal` ENABLE KEYS */;

-- 
-- Definition of proektmanager
-- 

DROP TABLE IF EXISTS `proektmanager`;
CREATE TABLE IF NOT EXISTS `proektmanager` (
  `id_Proekt_manager` int NOT NULL AUTO_INCREMENT,
  `FIO` varchar(70) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `password` varchar(70) DEFAULT NULL,
  `login` varchar(30) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_Proekt_manager`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table proektmanager
-- 

/*!40000 ALTER TABLE `proektmanager` DISABLE KEYS */;
INSERT INTO `proektmanager`(`id_Proekt_manager`,`FIO`,`Phone`,`Email`,`password`,`login`,`name`,`surname`,`patronymic`) VALUES(1,'Игнатьев В.И','89113487596','Ignatiev@mail.ru','202cb962ac59075b964b07152d234b70','1','Василий','Игнатьев','Игоревич'),(4,'Васильев К.А','89117485963','фыв@mail.ru','caf1a3dfb505ffed0d024130f58c5cfa','45','Кирилл','Васильев','Александрович'),(5,'Пахомов А.В','89558968787','qwe@mail.ru','e369853df766fa44e1ed0ff613f563bd','34','Анатолий','Пахомов','Викторович'),(6,'Иванов И.И','89117489563','fghfgh@mail.ru','47245a321ba33de5579d7890d2417c27','555','Иван','Иванов','Иванович');
/*!40000 ALTER TABLE `proektmanager` ENABLE KEYS */;

-- 
-- Definition of provider
-- 

DROP TABLE IF EXISTS `provider`;
CREATE TABLE IF NOT EXISTS `provider` (
  `id_Provider` int NOT NULL AUTO_INCREMENT,
  `name_Provider` varchar(50) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `OGRN` varchar(13) DEFAULT NULL,
  PRIMARY KEY (`id_Provider`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table provider
-- 

/*!40000 ALTER TABLE `provider` DISABLE KEYS */;
INSERT INTO `provider`(`id_Provider`,`name_Provider`,`phone`,`OGRN`) VALUES(1,'ООО \"Русь\"','891174859236','9876543210123'),(4,'ОАО \"МегаСтрой\"','89117485962','7894561230258');
/*!40000 ALTER TABLE `provider` ENABLE KEYS */;

-- 
-- Definition of consigment
-- 

DROP TABLE IF EXISTS `consigment`;
CREATE TABLE IF NOT EXISTS `consigment` (
  `id_Consigment` int NOT NULL AUTO_INCREMENT,
  `id_Provider` int DEFAULT NULL,
  `date_Consigment` date DEFAULT NULL,
  `id_Keepers` int DEFAULT NULL,
  PRIMARY KEY (`id_Consigment`),
  KEY `fkProvider_idx` (`id_Provider`),
  KEY `fkKeepeerrs_idx` (`id_Keepers`),
  CONSTRAINT `fkKeepeerrs` FOREIGN KEY (`id_Keepers`) REFERENCES `storekeepers` (`id_Keepers`),
  CONSTRAINT `fkProvider` FOREIGN KEY (`id_Provider`) REFERENCES `provider` (`id_Provider`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table consigment
-- 

/*!40000 ALTER TABLE `consigment` DISABLE KEYS */;
INSERT INTO `consigment`(`id_Consigment`,`id_Provider`,`date_Consigment`,`id_Keepers`) VALUES(1,1,'2024-04-05 00:00:00',3),(6,4,'2024-04-11 00:00:00',3);
/*!40000 ALTER TABLE `consigment` ENABLE KEYS */;

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
-- Definition of status
-- 

DROP TABLE IF EXISTS `status`;
CREATE TABLE IF NOT EXISTS `status` (
  `idstatus` int NOT NULL AUTO_INCREMENT,
  `nameStatus` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idstatus`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table status
-- 

/*!40000 ALTER TABLE `status` DISABLE KEYS */;
INSERT INTO `status`(`idstatus`,`nameStatus`) VALUES(1,'Принят в ремонт'),(20,'Завершён');
/*!40000 ALTER TABLE `status` ENABLE KEYS */;

-- 
-- Definition of proekt
-- 

DROP TABLE IF EXISTS `proekt`;
CREATE TABLE IF NOT EXISTS `proekt` (
  `id_Proekt` int NOT NULL AUTO_INCREMENT,
  `name_Proekt` varchar(60) DEFAULT NULL,
  `id_Proekt_manager` int DEFAULT NULL,
  `id_Ship` int DEFAULT NULL,
  `date_Start` date DEFAULT NULL,
  `date_End` date DEFAULT NULL,
  `idstatus` int DEFAULT NULL,
  PRIMARY KEY (`id_Proekt`),
  KEY `fkManager_idx` (`id_Proekt_manager`),
  KEY `fkShiip_idx` (`id_Ship`),
  KEY `fkStatus_idx` (`idstatus`),
  CONSTRAINT `fkManager` FOREIGN KEY (`id_Proekt_manager`) REFERENCES `proektmanager` (`id_Proekt_manager`),
  CONSTRAINT `fkShiip` FOREIGN KEY (`id_Ship`) REFERENCES `ship` (`id_Ship`),
  CONSTRAINT `fkStatus` FOREIGN KEY (`idstatus`) REFERENCES `status` (`idstatus`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table proekt
-- 

/*!40000 ALTER TABLE `proekt` DISABLE KEYS */;
INSERT INTO `proekt`(`id_Proekt`,`name_Proekt`,`id_Proekt_manager`,`id_Ship`,`date_Start`,`date_End`,`idstatus`) VALUES(4,'Замена оборудования',1,1,'2024-04-09 00:00:00','2024-04-09 00:00:00',20),(6,'Кузовной ремонт',4,1,'2024-04-09 00:00:00','2024-04-09 00:00:00',1),(7,'Электродиагностика ',5,1,'2024-04-09 00:00:00','2024-04-09 00:00:00',1),(10,'qwe',1,3,'2024-04-16 00:00:00','2024-04-16 00:00:00',20),(11,'dfgdfgf',5,1,'2024-05-22 00:00:00','2024-05-22 00:00:00',1),(12,'gggg',5,4,'2024-05-22 00:00:00','2024-05-22 00:00:00',1),(13,'qwe',1,5,'2024-05-22 00:00:00','2024-05-22 00:00:00',1);
/*!40000 ALTER TABLE `proekt` ENABLE KEYS */;

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
-- Definition of storekeepers
-- 

DROP TABLE IF EXISTS `storekeepers`;
CREATE TABLE IF NOT EXISTS `storekeepers` (
  `id_Keepers` int NOT NULL AUTO_INCREMENT,
  `FIO` varchar(70) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(70) DEFAULT NULL,
  `password` varchar(70) DEFAULT NULL,
  `login` varchar(30) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_Keepers`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table storekeepers
-- 

/*!40000 ALTER TABLE `storekeepers` DISABLE KEYS */;
INSERT INTO `storekeepers`(`id_Keepers`,`FIO`,`Phone`,`Email`,`password`,`login`,`name`,`surname`,`patronymic`) VALUES(3,'Кухарев А.И','89117458962','cvb@mail.ru','202cb962ac59075b964b07152d234b70','123','Александр','Кухарев','Иванович'),(4,'Латошин Н.К','89117777777','qwe@mail.ru','6512bd43d9caa6e02c990b0a82652dca','11','Николай','Латошин','Кириллович'),(5,'Кириллов И.А','89117485963','hgt@mail.ru','3295c76acbf4caaed33c36b1b5fc2cb1','66','Иван','Кириллов','Анатольевич');
/*!40000 ALTER TABLE `storekeepers` ENABLE KEYS */;

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
-- Definition of type_material
-- 

DROP TABLE IF EXISTS `type_material`;
CREATE TABLE IF NOT EXISTS `type_material` (
  `id_Type_Material` int NOT NULL AUTO_INCREMENT,
  `material` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`id_Type_Material`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table type_material
-- 

/*!40000 ALTER TABLE `type_material` DISABLE KEYS */;
INSERT INTO `type_material`(`id_Type_Material`,`material`) VALUES(1,'Смазка');
/*!40000 ALTER TABLE `type_material` ENABLE KEYS */;

-- 
-- Definition of material_spare
-- 

DROP TABLE IF EXISTS `material_spare`;
CREATE TABLE IF NOT EXISTS `material_spare` (
  `idmaterial_spare` int NOT NULL AUTO_INCREMENT,
  `name` varchar(70) DEFAULT NULL,
  `id_Type_Material` int DEFAULT NULL,
  PRIMARY KEY (`idmaterial_spare`),
  KEY `fkMaterialType_idx` (`id_Type_Material`),
  CONSTRAINT `fkMaterialType` FOREIGN KEY (`id_Type_Material`) REFERENCES `type_material` (`id_Type_Material`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table material_spare
-- 

/*!40000 ALTER TABLE `material_spare` DISABLE KEYS */;
INSERT INTO `material_spare`(`idmaterial_spare`,`name`,`id_Type_Material`) VALUES(3,'WD-1000',1),(4,'WD-800',1);
/*!40000 ALTER TABLE `material_spare` ENABLE KEYS */;

-- 
-- Definition of product_consigment
-- 

DROP TABLE IF EXISTS `product_consigment`;
CREATE TABLE IF NOT EXISTS `product_consigment` (
  `id_Product` int NOT NULL AUTO_INCREMENT,
  `idmaterial_spare` int DEFAULT NULL,
  `id_Consigment` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_Product`),
  KEY `fkMaterialSpare_idx` (`idmaterial_spare`),
  KEY `fkIdConsigment_idx` (`id_Consigment`),
  CONSTRAINT `fkIdConsigment` FOREIGN KEY (`id_Consigment`) REFERENCES `consigment` (`id_Consigment`),
  CONSTRAINT `fkMaterialSpare` FOREIGN KEY (`idmaterial_spare`) REFERENCES `material_spare` (`idmaterial_spare`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table product_consigment
-- 

/*!40000 ALTER TABLE `product_consigment` DISABLE KEYS */;

/*!40000 ALTER TABLE `product_consigment` ENABLE KEYS */;

-- 
-- Definition of store
-- 

DROP TABLE IF EXISTS `store`;
CREATE TABLE IF NOT EXISTS `store` (
  `cellNumber` int NOT NULL AUTO_INCREMENT,
  `idmaterial_spare` int DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`cellNumber`),
  KEY `fkiddMaterialSpare_idx` (`idmaterial_spare`),
  CONSTRAINT `fkiddMaterialSpare` FOREIGN KEY (`idmaterial_spare`) REFERENCES `material_spare` (`idmaterial_spare`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table store
-- 

/*!40000 ALTER TABLE `store` DISABLE KEYS */;

/*!40000 ALTER TABLE `store` ENABLE KEYS */;

-- 
-- Definition of type_repair_work
-- 

DROP TABLE IF EXISTS `type_repair_work`;
CREATE TABLE IF NOT EXISTS `type_repair_work` (
  `id_Type` int NOT NULL AUTO_INCREMENT,
  `type_Work` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_Type`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table type_repair_work
-- 

/*!40000 ALTER TABLE `type_repair_work` DISABLE KEYS */;
INSERT INTO `type_repair_work`(`id_Type`,`type_Work`) VALUES(1,'Покраска'),(8,'Замена электроники');
/*!40000 ALTER TABLE `type_repair_work` ENABLE KEYS */;

-- 
-- Definition of repairwork
-- 

DROP TABLE IF EXISTS `repairwork`;
CREATE TABLE IF NOT EXISTS `repairwork` (
  `idRepairWork` int NOT NULL AUTO_INCREMENT,
  `id_Proekt` int DEFAULT NULL,
  `id_Type` int DEFAULT NULL,
  `id_Ship` int DEFAULT NULL,
  `dateStart` date DEFAULT NULL,
  `dateEnd` date DEFAULT NULL,
  PRIMARY KEY (`idRepairWork`),
  KEY `fkProekts_idx` (`id_Proekt`),
  KEY `fkShips_idx` (`id_Ship`),
  KEY `fkTypeRepairWork_idx` (`id_Type`),
  CONSTRAINT `fkProekts` FOREIGN KEY (`id_Proekt`) REFERENCES `proekt` (`id_Proekt`),
  CONSTRAINT `fkShips` FOREIGN KEY (`id_Ship`) REFERENCES `ship` (`id_Ship`),
  CONSTRAINT `fkTypeRepairWork` FOREIGN KEY (`id_Type`) REFERENCES `type_repair_work` (`id_Type`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table repairwork
-- 

/*!40000 ALTER TABLE `repairwork` DISABLE KEYS */;
INSERT INTO `repairwork`(`idRepairWork`,`id_Proekt`,`id_Type`,`id_Ship`,`dateStart`,`dateEnd`) VALUES(4,4,1,1,'2023-09-05 00:00:00','2023-11-01 00:00:00'),(5,6,1,1,'2024-04-11 00:00:00','2024-04-11 00:00:00'),(6,4,1,1,'2024-04-12 00:00:00','2024-04-20 00:00:00'),(7,4,1,1,'2024-04-12 00:00:00','2024-04-12 00:00:00'),(8,4,1,1,'2024-04-12 00:00:00','2024-04-12 00:00:00'),(9,7,1,1,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(10,7,1,4,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(11,7,1,3,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(12,11,1,4,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(13,7,1,4,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(14,10,1,5,'2024-05-22 00:00:00','2024-05-22 00:00:00'),(16,7,1,1,'2024-06-03 00:00:00','2024-06-04 00:00:00');
/*!40000 ALTER TABLE `repairwork` ENABLE KEYS */;

-- 
-- Definition of list_repair_work
-- 

DROP TABLE IF EXISTS `list_repair_work`;
CREATE TABLE IF NOT EXISTS `list_repair_work` (
  `idlist_repair` int NOT NULL AUTO_INCREMENT,
  `idmaterial_spare` int DEFAULT NULL,
  `idRepairWork` int DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`idlist_repair`),
  KEY `fkmaterial_idx` (`idmaterial_spare`),
  KEY `fkrepairwork_idx` (`idRepairWork`),
  CONSTRAINT `fkmaterial3` FOREIGN KEY (`idmaterial_spare`) REFERENCES `material_spare` (`idmaterial_spare`),
  CONSTRAINT `fkrepairwork2` FOREIGN KEY (`idRepairWork`) REFERENCES `repairwork` (`idRepairWork`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table list_repair_work
-- 

/*!40000 ALTER TABLE `list_repair_work` DISABLE KEYS */;

/*!40000 ALTER TABLE `list_repair_work` ENABLE KEYS */;

-- 
-- Definition of teamrepair
-- 

DROP TABLE IF EXISTS `teamrepair`;
CREATE TABLE IF NOT EXISTS `teamrepair` (
  `idteamrepair` int NOT NULL AUTO_INCREMENT,
  `idRepairWork` int DEFAULT NULL,
  `id_Personal` int DEFAULT NULL,
  `dateStart` date DEFAULT NULL,
  `dateEnd` date DEFAULT NULL,
  PRIMARY KEY (`idteamrepair`),
  KEY `fkRepairWork_idx` (`idRepairWork`),
  KEY `fkPersonal_idx` (`id_Personal`),
  CONSTRAINT `fkPersonal` FOREIGN KEY (`id_Personal`) REFERENCES `personal` (`id_Personal`),
  CONSTRAINT `fkRepairWork` FOREIGN KEY (`idRepairWork`) REFERENCES `repairwork` (`idRepairWork`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table teamrepair
-- 

/*!40000 ALTER TABLE `teamrepair` DISABLE KEYS */;
INSERT INTO `teamrepair`(`idteamrepair`,`idRepairWork`,`id_Personal`,`dateStart`,`dateEnd`) VALUES(7,4,7,'2024-04-11 00:00:00','2024-04-11 00:00:00'),(8,4,8,'2024-04-11 00:00:00','2024-04-11 00:00:00'),(9,5,8,'2024-04-11 00:00:00','2024-04-11 00:00:00');
/*!40000 ALTER TABLE `teamrepair` ENABLE KEYS */;

-- 
-- Definition of type_ship
-- 

DROP TABLE IF EXISTS `type_ship`;
CREATE TABLE IF NOT EXISTS `type_ship` (
  `id_Type_Ship` int NOT NULL AUTO_INCREMENT,
  `type_Ship` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_Type_Ship`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table type_ship
-- 

/*!40000 ALTER TABLE `type_ship` DISABLE KEYS */;
INSERT INTO `type_ship`(`id_Type_Ship`,`type_Ship`) VALUES(5,'Танкер'),(7,'Рыболовецкое'),(8,'Буксировочное');
/*!40000 ALTER TABLE `type_ship` ENABLE KEYS */;

-- 
-- Definition of ship
-- 

DROP TABLE IF EXISTS `ship`;
CREATE TABLE IF NOT EXISTS `ship` (
  `id_Ship` int NOT NULL AUTO_INCREMENT,
  `IMO` varchar(7) DEFAULT NULL,
  `name_Ship` varchar(50) DEFAULT NULL,
  `id_Type_Ship` int DEFAULT NULL,
  `year_Construct` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id_Ship`),
  KEY `fkShip_idx` (`id_Type_Ship`),
  CONSTRAINT `fkShip` FOREIGN KEY (`id_Type_Ship`) REFERENCES `type_ship` (`id_Type_Ship`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 
-- Dumping data for table ship
-- 

/*!40000 ALTER TABLE `ship` DISABLE KEYS */;
INSERT INTO `ship`(`id_Ship`,`IMO`,`name_Ship`,`id_Type_Ship`,`year_Construct`) VALUES(1,'3439422','Ленин',7,'2000'),(3,'3183554','Правин',7,'1890'),(4,'4564564','авпвапр',7,'2002'),(5,'1231231','RRR',5,'2005');
/*!40000 ALTER TABLE `ship` ENABLE KEYS */;

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
INSERT INTO `users`(`UserId`,`Login`,`PasswordHash`,`Email`,`RoleId`) VALUES(1,'Admin_test','$2a$11$1RD7xY0tH0ke7Xr7B44ZKeKHM1Oxn5ZFemRvEfarLfOlATu7/oMFe','admin@admin.ru',1),(3,'test1','$2a$11$.xSU/MeZ2ZjIHPuJ3cKBHuOeSYpmVcNVWHI1jSTe17tgeGoCrSZBS',NULL,2),(8,'client1','$2a$11$BfTaz2iIKUTYibYndAqwiuOFMCRFXnkA4xqJ/hAVDfBMWkJNQJfDS',NULL,3),(10,'Admin2','$2a$11$n6mUEgfTnndYm1IWVBTphOLJ54ddhH.vqgM4Pj9uZKzsYP2tEoZHm',NULL,2),(22,'Admin4','$2a$11$dX/PukTFK/E0VeFgAM8qKedGuQRAGR4FnTU2iXG1wEwRGh/s8eCdi',NULL,2),(23,'Admin5','$2a$11$Os3vjoR7x.3lTuJOy/HVS.e2nHvy7Gk8GS7B8vI.vsp0ykQuXF7Zm',NULL,2),(24,'Admin6','$2a$11$t/yHhv3QuufjEZ.2hLJV0OdYnIu3zuXPWRRWo2DatsN39ucIpm892',NULL,2);
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


-- Dump completed on 2024-05-29 02:41:03
-- Total time: 0:0:0:0:98 (d:h:m:s:ms)
