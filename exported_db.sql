CREATE DATABASE  IF NOT EXISTS `music` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `music`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: music
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `albums`
--

DROP TABLE IF EXISTS `albums`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `albums` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titlu` varchar(100) DEFAULT NULL,
  `an_lansare` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `albums`
--

LOCK TABLES `albums` WRITE;
/*!40000 ALTER TABLE `albums` DISABLE KEYS */;
INSERT INTO `albums` VALUES (1,'The Marshall Mathers LP',2000),(2,'Revival',2017),(3,'All Eyez on Me',1996),(4,'Me Against the World',1995),(5,'Onoarea Inainte de Toate',2015),(6,'Alin Durerea',2016),(7,'Binecuvantat',2017),(8,'Arca 11',2023),(9,'PartyPackz+',2023),(10,'Imagine',1971),(11,'Bohemian Rhapsody',1975),(12,'Thriller',1982),(13,'Dangerous',1991),(14,'Cântec de leagăn',1938),(15,'Lume, Lume',1940);
/*!40000 ALTER TABLE `albums` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `albums_musicians`
--

DROP TABLE IF EXISTS `albums_musicians`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `albums_musicians` (
  `id_muzician` int NOT NULL,
  `id_album` int NOT NULL,
  PRIMARY KEY (`id_muzician`,`id_album`),
  KEY `id_album` (`id_album`),
  CONSTRAINT `albums_musicians_ibfk_1` FOREIGN KEY (`id_muzician`) REFERENCES `musicians` (`id`),
  CONSTRAINT `albums_musicians_ibfk_2` FOREIGN KEY (`id_album`) REFERENCES `albums` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `albums_musicians`
--

LOCK TABLES `albums_musicians` WRITE;
/*!40000 ALTER TABLE `albums_musicians` DISABLE KEYS */;
INSERT INTO `albums_musicians` VALUES (1,1),(1,2),(2,3),(2,4),(3,5),(3,6),(3,7),(4,8),(5,9),(6,10),(7,11),(7,12),(8,13),(9,14),(9,15);
/*!40000 ALTER TABLE `albums_musicians` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `musicians`
--

DROP TABLE IF EXISTS `musicians`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `musicians` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nume` varchar(100) DEFAULT NULL,
  `gen_muzical` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `musicians`
--

LOCK TABLES `musicians` WRITE;
/*!40000 ALTER TABLE `musicians` DISABLE KEYS */;
INSERT INTO `musicians` VALUES (1,'Eminem','Rap'),(2,'2Pac','Rap'),(3,'El Nino','Hip Hop'),(4,'Noua Unșpe','Trap'),(5,'Aerozen','Trap'),(6,'John Lennon','Rock'),(7,'Queen','Rock'),(8,'Michael Jackson','Pop'),(9,'Maria Tănase','Folk');
/*!40000 ALTER TABLE `musicians` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-28 22:33:56
