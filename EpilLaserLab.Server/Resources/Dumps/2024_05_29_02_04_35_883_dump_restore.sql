-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: pkzvezda
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `consigment`
--

DROP TABLE IF EXISTS `consigment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consigment` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consigment`
--

LOCK TABLES `consigment` WRITE;
/*!40000 ALTER TABLE `consigment` DISABLE KEYS */;
INSERT INTO `consigment` VALUES (1,1,'2024-04-05',3),(6,4,'2024-04-11',3);
/*!40000 ALTER TABLE `consigment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `list_repair_work`
--

DROP TABLE IF EXISTS `list_repair_work`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `list_repair_work` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `list_repair_work`
--

LOCK TABLES `list_repair_work` WRITE;
/*!40000 ALTER TABLE `list_repair_work` DISABLE KEYS */;
/*!40000 ALTER TABLE `list_repair_work` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `material_spare`
--

DROP TABLE IF EXISTS `material_spare`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `material_spare` (
  `idmaterial_spare` int NOT NULL AUTO_INCREMENT,
  `name` varchar(70) DEFAULT NULL,
  `id_Type_Material` int DEFAULT NULL,
  PRIMARY KEY (`idmaterial_spare`),
  KEY `fkMaterialType_idx` (`id_Type_Material`),
  CONSTRAINT `fkMaterialType` FOREIGN KEY (`id_Type_Material`) REFERENCES `type_material` (`id_Type_Material`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `material_spare`
--

LOCK TABLES `material_spare` WRITE;
/*!40000 ALTER TABLE `material_spare` DISABLE KEYS */;
INSERT INTO `material_spare` VALUES (3,'WD-1000',1),(4,'WD-800',1);
/*!40000 ALTER TABLE `material_spare` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `personal`
--

DROP TABLE IF EXISTS `personal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `personal` (
  `id_Personal` int NOT NULL AUTO_INCREMENT,
  `FIO` varchar(60) DEFAULT NULL,
  `Telephone` varchar(20) DEFAULT NULL,
  `id_Positions` int DEFAULT NULL,
  PRIMARY KEY (`id_Personal`),
  KEY `fk_positions_idx` (`id_Positions`),
  CONSTRAINT `fk_positions` FOREIGN KEY (`id_Positions`) REFERENCES `positions` (`id_Positions`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `personal`
--

LOCK TABLES `personal` WRITE;
/*!40000 ALTER TABLE `personal` DISABLE KEYS */;
INSERT INTO `personal` VALUES (7,'Юсупов И.А','89113457896',8),(8,'Григорьев А.В','89117485962',10);
/*!40000 ALTER TABLE `personal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `positions`
--

DROP TABLE IF EXISTS `positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `positions` (
  `id_Positions` int NOT NULL AUTO_INCREMENT,
  `name_Positions` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`id_Positions`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `positions`
--

LOCK TABLES `positions` WRITE;
/*!40000 ALTER TABLE `positions` DISABLE KEYS */;
INSERT INTO `positions` VALUES (8,'Наладчик оборудования'),(10,'Механик'),(11,'Электрик');
/*!40000 ALTER TABLE `positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_consigment`
--

DROP TABLE IF EXISTS `product_consigment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_consigment` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_consigment`
--

LOCK TABLES `product_consigment` WRITE;
/*!40000 ALTER TABLE `product_consigment` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_consigment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proekt`
--

DROP TABLE IF EXISTS `proekt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proekt` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proekt`
--

LOCK TABLES `proekt` WRITE;
/*!40000 ALTER TABLE `proekt` DISABLE KEYS */;
INSERT INTO `proekt` VALUES (4,'Замена оборудования',1,1,'2024-04-09','2024-04-09',20),(6,'Кузовной ремонт',4,1,'2024-04-09','2024-04-09',1),(7,'Электродиагностика ',5,1,'2024-04-09','2024-04-09',1),(10,'qwe',1,3,'2024-04-16','2024-04-16',20),(11,'dfgdfgf',5,1,'2024-05-22','2024-05-22',1),(12,'gggg',5,4,'2024-05-22','2024-05-22',1),(13,'qwe',1,5,'2024-05-22','2024-05-22',1);
/*!40000 ALTER TABLE `proekt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proektmanager`
--

DROP TABLE IF EXISTS `proektmanager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proektmanager` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proektmanager`
--

LOCK TABLES `proektmanager` WRITE;
/*!40000 ALTER TABLE `proektmanager` DISABLE KEYS */;
INSERT INTO `proektmanager` VALUES (1,'Игнатьев В.И','89113487596','Ignatiev@mail.ru','202cb962ac59075b964b07152d234b70','1','Василий','Игнатьев','Игоревич'),(4,'Васильев К.А','89117485963','фыв@mail.ru','caf1a3dfb505ffed0d024130f58c5cfa','45','Кирилл','Васильев','Александрович'),(5,'Пахомов А.В','89558968787','qwe@mail.ru','e369853df766fa44e1ed0ff613f563bd','34','Анатолий','Пахомов','Викторович'),(6,'Иванов И.И','89117489563','fghfgh@mail.ru','47245a321ba33de5579d7890d2417c27','555','Иван','Иванов','Иванович');
/*!40000 ALTER TABLE `proektmanager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `provider`
--

DROP TABLE IF EXISTS `provider`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `provider` (
  `id_Provider` int NOT NULL AUTO_INCREMENT,
  `name_Provider` varchar(50) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `OGRN` varchar(13) DEFAULT NULL,
  PRIMARY KEY (`id_Provider`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `provider`
--

LOCK TABLES `provider` WRITE;
/*!40000 ALTER TABLE `provider` DISABLE KEYS */;
INSERT INTO `provider` VALUES (1,'ООО \"Русь\"','891174859236','9876543210123'),(4,'ОАО \"МегаСтрой\"','89117485962','7894561230258');
/*!40000 ALTER TABLE `provider` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repairwork`
--

DROP TABLE IF EXISTS `repairwork`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `repairwork` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repairwork`
--

LOCK TABLES `repairwork` WRITE;
/*!40000 ALTER TABLE `repairwork` DISABLE KEYS */;
INSERT INTO `repairwork` VALUES (4,4,1,1,'2023-09-05','2023-11-01'),(5,6,1,1,'2024-04-11','2024-04-11'),(6,4,1,1,'2024-04-12','2024-04-20'),(7,4,1,1,'2024-04-12','2024-04-12'),(8,4,1,1,'2024-04-12','2024-04-12'),(9,7,1,1,'2024-05-22','2024-05-22'),(10,7,1,4,'2024-05-22','2024-05-22'),(11,7,1,3,'2024-05-22','2024-05-22'),(12,11,1,4,'2024-05-22','2024-05-22'),(13,7,1,4,'2024-05-22','2024-05-22'),(14,10,1,5,'2024-05-22','2024-05-22'),(16,7,1,1,'2024-06-03','2024-06-04');
/*!40000 ALTER TABLE `repairwork` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ship`
--

DROP TABLE IF EXISTS `ship`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ship` (
  `id_Ship` int NOT NULL AUTO_INCREMENT,
  `IMO` varchar(7) DEFAULT NULL,
  `name_Ship` varchar(50) DEFAULT NULL,
  `id_Type_Ship` int DEFAULT NULL,
  `year_Construct` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`id_Ship`),
  KEY `fkShip_idx` (`id_Type_Ship`),
  CONSTRAINT `fkShip` FOREIGN KEY (`id_Type_Ship`) REFERENCES `type_ship` (`id_Type_Ship`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ship`
--

LOCK TABLES `ship` WRITE;
/*!40000 ALTER TABLE `ship` DISABLE KEYS */;
INSERT INTO `ship` VALUES (1,'3439422','Ленин',7,'2000'),(3,'3183554','Правин',7,'1890'),(4,'4564564','авпвапр',7,'2002'),(5,'1231231','RRR',5,'2005');
/*!40000 ALTER TABLE `ship` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status`
--

DROP TABLE IF EXISTS `status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `status` (
  `idstatus` int NOT NULL AUTO_INCREMENT,
  `nameStatus` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idstatus`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status`
--

LOCK TABLES `status` WRITE;
/*!40000 ALTER TABLE `status` DISABLE KEYS */;
INSERT INTO `status` VALUES (1,'Принят в ремонт'),(20,'Завершён');
/*!40000 ALTER TABLE `status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `store`
--

DROP TABLE IF EXISTS `store`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `store` (
  `cellNumber` int NOT NULL AUTO_INCREMENT,
  `idmaterial_spare` int DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  PRIMARY KEY (`cellNumber`),
  KEY `fkiddMaterialSpare_idx` (`idmaterial_spare`),
  CONSTRAINT `fkiddMaterialSpare` FOREIGN KEY (`idmaterial_spare`) REFERENCES `material_spare` (`idmaterial_spare`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `store`
--

LOCK TABLES `store` WRITE;
/*!40000 ALTER TABLE `store` DISABLE KEYS */;
/*!40000 ALTER TABLE `store` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storekeepers`
--

DROP TABLE IF EXISTS `storekeepers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `storekeepers` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storekeepers`
--

LOCK TABLES `storekeepers` WRITE;
/*!40000 ALTER TABLE `storekeepers` DISABLE KEYS */;
INSERT INTO `storekeepers` VALUES (3,'Кухарев А.И','89117458962','cvb@mail.ru','202cb962ac59075b964b07152d234b70','123','Александр','Кухарев','Иванович'),(4,'Латошин Н.К','89117777777','qwe@mail.ru','6512bd43d9caa6e02c990b0a82652dca','11','Николай','Латошин','Кириллович'),(5,'Кириллов И.А','89117485963','hgt@mail.ru','3295c76acbf4caaed33c36b1b5fc2cb1','66','Иван','Кириллов','Анатольевич');
/*!40000 ALTER TABLE `storekeepers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teamrepair`
--

DROP TABLE IF EXISTS `teamrepair`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teamrepair` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teamrepair`
--

LOCK TABLES `teamrepair` WRITE;
/*!40000 ALTER TABLE `teamrepair` DISABLE KEYS */;
INSERT INTO `teamrepair` VALUES (7,4,7,'2024-04-11','2024-04-11'),(8,4,8,'2024-04-11','2024-04-11'),(9,5,8,'2024-04-11','2024-04-11');
/*!40000 ALTER TABLE `teamrepair` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_material`
--

DROP TABLE IF EXISTS `type_material`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type_material` (
  `id_Type_Material` int NOT NULL AUTO_INCREMENT,
  `material` varchar(70) DEFAULT NULL,
  PRIMARY KEY (`id_Type_Material`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_material`
--

LOCK TABLES `type_material` WRITE;
/*!40000 ALTER TABLE `type_material` DISABLE KEYS */;
INSERT INTO `type_material` VALUES (1,'Смазка');
/*!40000 ALTER TABLE `type_material` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_repair_work`
--

DROP TABLE IF EXISTS `type_repair_work`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type_repair_work` (
  `id_Type` int NOT NULL AUTO_INCREMENT,
  `type_Work` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_Type`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_repair_work`
--

LOCK TABLES `type_repair_work` WRITE;
/*!40000 ALTER TABLE `type_repair_work` DISABLE KEYS */;
INSERT INTO `type_repair_work` VALUES (1,'Покраска'),(8,'Замена электроники');
/*!40000 ALTER TABLE `type_repair_work` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type_ship`
--

DROP TABLE IF EXISTS `type_ship`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type_ship` (
  `id_Type_Ship` int NOT NULL AUTO_INCREMENT,
  `type_Ship` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_Type_Ship`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type_ship`
--

LOCK TABLES `type_ship` WRITE;
/*!40000 ALTER TABLE `type_ship` DISABLE KEYS */;
INSERT INTO `type_ship` VALUES (5,'Танкер'),(7,'Рыболовецкое'),(8,'Буксировочное');
/*!40000 ALTER TABLE `type_ship` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-29  2:04:05
