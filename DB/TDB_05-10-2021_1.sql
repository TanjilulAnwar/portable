/*
SQLyog Community v13.1.7 (64 bit)
MySQL - 8.0.23 : Database - tdb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`tdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `tdb`;

/*Table structure for table `account_control` */

DROP TABLE IF EXISTS `account_control`;

CREATE TABLE `account_control` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  `key` varchar(2) DEFAULT NULL,
  `ac_type` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `account_control` */

insert  into `account_control`(`id`,`name`,`key`,`ac_type`) values 
(1,'Assets','A','Dr.'),
(2,'Liabilities','L','Cr.'),
(3,'Capital','C','Cr.'),
(4,'Expenditure','E','Dr.'),
(5,'Income','I','Cr.');

/*Table structure for table `accounts_group` */

DROP TABLE IF EXISTS `accounts_group`;

CREATE TABLE `accounts_group` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ac_group_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_group_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `control_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;

/*Data for the table `accounts_group` */

insert  into `accounts_group`(`id`,`ac_group_id`,`ac_group_name`,`ac_type`,`control_type`,`description`,`comments`,`client_code`) values 
(1,'01','Current Assets','Dr.','A','Provides financial information to management by researching and analyzing accounting data. Provides financial information to management by researching and analyzing accounting data','op','01'),
(2,'02','Fixed Assets','Dr.','A','Provides financial information to management by researching and analyzing accounting data. Provides financial information to management by researching and analyzing accounting data','op','01'),
(3,'03','Current Liabilities','Cr.','L','Provides financial information to management by researching and analyzing accounting data','op','01'),
(4,'04','Non-Current Liabilities','Cr.','L','Provides financial information to management by researching and analyzing accounting data','op','01'),
(5,'05','Owner\'s Equity','Cr.','C','Provides financial information to management by researching and analyzing accounting data','op','01'),
(7,'06','Sales Income','Cr.','I','Provides financial information to management by researching and analyzing accounting data','op','01'),
(8,'07','Others Income','Cr.','I','Provides financial information to management by researching and analyzing accounting data','op','01'),
(9,'08','General Expenses','Dr.','E','Provides financial information to management by researching and analyzing accounting data','op','01'),
(10,'09','Others Expenses','Dr.','E','Provides financial information to management by researching and analyzing accounting data','op','01'),
(11,'10','Director\'s Investment','Cr.','C','Provides financial information to management by researching and analyzing accounting data','op','01'),
(27,'11','Non-Current Assets','Dr.','A','n/a','n/a','01'),
(28,'12','Investment','Cr.','C','n/a','n/a','01');

/*Table structure for table `accounts_head` */

DROP TABLE IF EXISTS `accounts_head`;

CREATE TABLE `accounts_head` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ac_head_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_head_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_group_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_name_head_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_group_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_status` tinyint(1) DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `control_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `main_sub` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=124 DEFAULT CHARSET=utf8;

/*Data for the table `accounts_head` */

insert  into `accounts_head`(`id`,`ac_head_id`,`ac_head_name`,`description`,`ac_group_name`,`ac_name_head_id`,`ac_group_id`,`ac_type`,`ac_status`,`client_code`,`control_type`,`trade_code`,`main_sub`) values 
(94,'01020000','Cash & Cash Equivalents','Cash','Current Assets','01020000','01','Dr.',1,'01','A','0101','M'),
(95,'03010000','Accounts Payable','n/a','Current Liabilities','03010000','03','Cr.',1,'01','L','0101','M'),
(97,'02010000','Supplies','n/a','Fixed Assets','02010000','02','Dr.',1,'01','A','0101','M'),
(98,'06010000','Sales Income','n/a','Sales Income','06010000','06','Cr.',1,'01','I','0101','M'),
(99,'08010000','Inventory Cost','n/a','General Expenses','08010000','08','Dr.',1,'01','E','0101','M'),
(100,'08020000','Appliance Bill','n/a','General Expenses','08020000','08','Dr.',1,'01','E','0101','M'),
(101,'03020000','Loans and Borrowings','n/a','Current Liabilities','03020000','03','Cr.',1,'01','L','0101','M'),
(102,'08030000','Rent','n/a','General Expenses','08030000','08','Dr.',1,'01','E','0101','M'),
(103,'03030000','Payroll/Salary','N/A','General Expenses','03030000','08','Cr.',1,'01','E','0101','M'),
(104,'11010000','Accounts Recievable','n/a','Non-Current Assets','11010000','11','Dr.',1,'01','A','0101','M'),
(105,'06020000','Returned Goods','n/a','Sales Income','06020000','06','Cr.',1,'01','I','0101','M'),
(106,'08040000','Customer Returns','n/a','General Expenses','08040000','08','Dr.',1,'01','E','0101','M'),
(107,'12010000','Director\'s Investment','n/a','Investment','12010000','12','Cr.',1,'01','C','0101','M'),
(108,'01030000','United Commercial Bank Ltd','Bank Acccount','Current Assets','01030000','01','Dr.',1,'01','A','0101','M'),
(109,'01040000','NCC Bank','Bank Account','Current Assets','01040000','01','Dr.',1,'01','A','0101','M'),
(110,'01050000','Dutch-Bangla Bank','DBBL','Current Assets','01050000','01','Dr.',1,'01','A','0101','M'),
(111,'01060000','Islami Bank Bangladesh Limited (IBBL)','Bank Account','Current Assets','01060000','01','Dr.',1,'01','A','0101','M'),
(112,'01070000','Janata Bank','Bank Account','Current Assets','01070000','01','Dr.',1,'01','A','0101','M'),
(113,'01080000','Prime Bank Limited','Bank Account','Current Assets','01080000','01','Dr.',1,'01','A','0101','M'),
(114,'08050000','Logistics Service','N/A','General Expenses','08050000','08','Dr.',1,'01','E','0101','M'),
(119,'08050001','Delivery','N/A','General Expenses','08050000','08','Dr.',1,'01','E','0101','S'),
(120,'10010000','Shareholder\'s contribution ','N/A','Director\'s Investment','10010000','10','Cr.',1,'01','C','0101','M'),
(121,'02010001','Equipments','N/A','Fixed Assets','02010000','02','Dr.',1,'01','A','0101','S'),
(122,'08010001','Medicine Storage','n/a','General Expenses','08010000','08','Dr.',1,'01','E','0101','S'),
(123,'03020001','Bank Loans','n/a','Current Liabilities','03020000','03','Cr.',1,'01','L','0101','S');

/*Table structure for table `aspnetroleclaims` */

DROP TABLE IF EXISTS `aspnetroleclaims`;

CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetroleclaims` */

/*Table structure for table `aspnetroles` */

DROP TABLE IF EXISTS `aspnetroles`;

CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetroles` */

insert  into `aspnetroles`(`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`) values 
('3aca9f88-3a91-484a-9c23-d140037d503c','f6Kk5PFjmKIOF5kjgfiRXi2rZWrNhKpPPS2KCqzDE78=','F6KK5PFJMKIOF5KJGFIRXI2RZWRNHKPPPS2KCQZDE78=','1328dee4-2415-40ca-a044-e5431c35f452'),
('698b343e-23d3-48d7-ac13-1e9ad3a9e800','0mpzQu32z7ZIBvRSIwWkwH6RumaFdAzXZTK72kyPcxk=','0MPZQU32Z7ZIBVRSIWWKWH6RUMAFDAZXZTK72KYPCXK=','0eb078f0-6950-48ed-a356-cf9a9afdffe0'),
('9bdf8489-43ee-4b09-b4bd-3d331868aaf5','qBRYexmd0uEzk4JcsYaq1moowa1rscOjXDPLhLV/t7w=','QBRYEXMD0UEZK4JCSYAQ1MOOWA1RSCOJXDPLHLV/T7W=','ea47ad7d-3a36-4c3d-b2bb-19eb66bfa426'),
('ebfdb49e-15dc-41c4-b5dd-7983002941d9','vssQFOBH2Oi1cnNULlE/ShttO3SIE1rSnQzKet++FTU=','VSSQFOBH2OI1CNNULLE/SHTTO3SIE1RSNQZKET++FTU=','d5f97b84-618f-4723-91af-38da0481d061'),
('eed4c4d3-3f26-4653-9d5f-b8a7fd4ebdec','hLNKRZ0S2LVT+XIHhMz9FmFubj42XIVAU0x8zEVwJtY=','HLNKRZ0S2LVT+XIHHMZ9FMFUBJ42XIVAU0X8ZEVWJTY=','0913980b-2d44-42ce-80a1-138d91fa86f6');

/*Table structure for table `aspnetuserclaims` */

DROP TABLE IF EXISTS `aspnetuserclaims`;

CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserclaims` */

/*Table structure for table `aspnetuserlogins` */

DROP TABLE IF EXISTS `aspnetuserlogins`;

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserlogins` */

/*Table structure for table `aspnetuserroles` */

DROP TABLE IF EXISTS `aspnetuserroles`;

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserroles` */

insert  into `aspnetuserroles`(`UserId`,`RoleId`) values 
('685c0f3a-bdc0-4ab8-8dcb-17468e4b80d1','698b343e-23d3-48d7-ac13-1e9ad3a9e800'),
('cba2485a-752d-488d-8d6e-c03642e7af90','698b343e-23d3-48d7-ac13-1e9ad3a9e800'),
('00ff38c1-f564-4a57-9ffb-898a5dd1fb7f','9bdf8489-43ee-4b09-b4bd-3d331868aaf5'),
('445c39db-d1d7-47f3-a8c5-d6a1807f77d5','9bdf8489-43ee-4b09-b4bd-3d331868aaf5'),
('7353822d-ce51-478c-ac0e-8ecf4e82c8ad','ebfdb49e-15dc-41c4-b5dd-7983002941d9'),
('abaa4059-e2be-4747-be2a-29ac4568a438','ebfdb49e-15dc-41c4-b5dd-7983002941d9'),
('d870f273-3380-47ed-a0ec-c5cabf960270','eed4c4d3-3f26-4653-9d5f-b8a7fd4ebdec');

/*Table structure for table `aspnetusers` */

DROP TABLE IF EXISTS `aspnetusers`;

CREATE TABLE `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetusers` */

insert  into `aspnetusers`(`Id`,`UserName`,`NormalizedUserName`,`Email`,`NormalizedEmail`,`EmailConfirmed`,`PasswordHash`,`SecurityStamp`,`ConcurrencyStamp`,`PhoneNumber`,`PhoneNumberConfirmed`,`TwoFactorEnabled`,`LockoutEnd`,`LockoutEnabled`,`AccessFailedCount`) values 
('00ff38c1-f564-4a57-9ffb-898a5dd1fb7f','01700000000','01700000000','ik.na@diabetic.com','IK.NA@DIABETIC.COM',0,'AQAAAAEAACcQAAAAEDZyX88TANF7rHGrGK89l9LDZc8tN81QHG1pHxKxJjQR1T/038mKFuNspsLXw1qPTQ==','MQPOJASJD44DQ4QU2F5JXH54VA4GTLAY','1c69809d-a415-425c-9545-395d00eb87c1','01700000000',0,0,NULL,1,0),
('445c39db-d1d7-47f3-a8c5-d6a1807f77d5','01521208467','01521208467','hasan@gmail.com','HASAN@GMAIL.COM',0,'AQAAAAEAACcQAAAAEMVAtbvrfaTYIJ8bksFYQhdXsISZewQ+LfmpqUV7pcyPCE4QPr6vxtG5AT/N/KDM8g==','AVF2OVQV37N37QL7G24EYHZRIJNOIEPQ','ca30066e-0ff2-4e04-9ad2-8f9a53113266','01521208467',0,0,NULL,1,0),
('685c0f3a-bdc0-4ab8-8dcb-17468e4b80d1','01521208468','01521208468','jerin.hasan098@gmail.com','JERIN.HASAN098@GMAIL.COM',0,'AQAAAAEAACcQAAAAEJQ5bbc9LP5TC7dObD/oNIGOVAHgfUZ1xX0FJw6RcEiZqfiNnmBhZWv74y2qli2eyA==','AH4NS36W75NDMPJSNZE3RID7Q3GGFTJY','9a6be83c-2ed4-49d5-8482-a2dd65dc3bba','01521208468',0,0,NULL,1,0),
('7353822d-ce51-478c-ac0e-8ecf4e82c8ad','admin','ADMIN','admin@pos.com','ADMIN@POS.COM',0,'AQAAAAEAACcQAAAAEIbIlVGJcIdsInLiPgjpCXZbwNeE1K3Mb5c7JTHMXSqhKAJY9Kttd8YVwAJn41QkUQ==','2FDFR4WRI2QAHLCFFCHLQC4YSGVKVDJ6','41c70b99-3d3e-4bb3-a44b-db6055ac03b7','12345678910',0,0,NULL,1,0),
('abaa4059-e2be-4747-be2a-29ac4568a438','01959658965','01959658965','taw@fiqur.com','TAW@FIQUR.COM',0,'AQAAAAEAACcQAAAAECiequbf+8uKS48Q9v//2KRaSMDzfZkd63RNV8pxAcfvvTzHpVjm3axMhUo1VFaRWg==','U3LSJKYI6ELX3MRLDB63LS4IUANZ2M52','4fe2ba61-eea7-4348-bfbb-248fb3936d70','01959658965',0,0,NULL,1,0),
('cba2485a-752d-488d-8d6e-c03642e7af90','01976821178','01976821178','tanj@mail.com','TANJ@MAIL.COM',0,'AQAAAAEAACcQAAAAEJ7sQFEXANb37jUIunY8y2U2eTd/5PcCCAkSHDEqFtKl6OEyrTa0oQcY7cnNfhNITg==','25BGYJZ5BUDVPLTEV4NB76MIENEWEA4C','6ec9ac10-5e78-4ff4-bf24-cdd5af277891','01976821178',0,0,NULL,1,0),
('d870f273-3380-47ed-a0ec-c5cabf960270','sysadmin','SYSADMIN','pos.admin@nice.com','POS.ADMIN@NICE.COM',0,'AQAAAAEAACcQAAAAELgqs7A7duLRj92rrQs1t1KxKguCILwkBHQQg2zOnJa0Sq41OqJug1u/rNvRhfwM3g==','TKKW2IEJZDEUDJNM5HSRT2N3GIK4FJOF','f0622752-d62c-4b6e-bd11-cff71655d789','01111111111',0,0,NULL,1,0);

/*Table structure for table `aspnetusertokens` */

DROP TABLE IF EXISTS `aspnetusertokens`;

CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetusertokens` */

/*Table structure for table `category_info` */

DROP TABLE IF EXISTS `category_info`;

CREATE TABLE `category_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8;

/*Data for the table `category_info` */

insert  into `category_info`(`id`,`code`,`name`,`description`,`client_code`,`trade_code`) values 
(49,'0018','APPLIANCES','Appliances','01','0101'),
(50,'0019','ARTS, CRAFTS, & SEWING','Arts, Crafts, & Sewing','01','0101'),
(51,'0020','BOOKS','Books','01','0101'),
(52,'0021','CELL PHONES & ACCESSORIES','Cell Phones & Accessories','01','0101'),
(53,'0022','CLOTHING, SHOES AND JEWELRY','Clothing, Shoes and Jewelry','01','0101'),
(54,'0023','ELECTRONICS','Electronics','01','0101'),
(55,'0024','GARDEN & OUTDOOR','Garden & Outdoor','01','0101'),
(56,'0025','OFFICE PRODUCTS','Office Products','01','0101'),
(57,'0026','TOOLS & HOME IMPROVEMENT','Tools & Home Improvement','01','0101'),
(58,'0027','GROCERY & GOURMET FOOD','Grocery & Gourmet Food','01','0101'),
(59,'0028','HOME & KITCHEN','Home & Kitchen','01','0101'),
(60,'0029','HEALTH, HOUSEHOLD & BABY CARE','Health, Household & Baby Care','01','0101'),
(61,'0030','MUSICAL INSTRUMENTS','Musical Instruments','01','0101');

/*Table structure for table `client` */

DROP TABLE IF EXISTS `client`;

CREATE TABLE `client` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `division` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `district` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `thana` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `zipcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `logo` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `client` */

insert  into `client`(`id`,`name`,`code`,`description`,`phone`,`address`,`division`,`district`,`thana`,`email`,`zipcode`,`logo`) values 
(1,'TR ENTERPRISE','01','TR ENTERPRISE','548825417','76/B TEJTURI BAZAR, WEST PANTHAPATH','DHAKA','DHAKA','KARWANBAZAR','admin@email.com','3500','/images/client/01/logo/01.svg');

/*Table structure for table `customers_info` */

DROP TABLE IF EXISTS `customers_info`;

CREATE TABLE `customers_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `company` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `loyalty_point` double(10,2) DEFAULT '0.00',
  `division` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `district` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `thana` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `mobile` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `customer_type` tinyint(1) DEFAULT '1',
  `remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_date` date DEFAULT '1000-01-01',
  `entry_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `patient_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

/*Data for the table `customers_info` */

insert  into `customers_info`(`id`,`code`,`name`,`company`,`address`,`loyalty_point`,`division`,`district`,`thana`,`mobile`,`email`,`customer_type`,`remarks`,`entry_date`,`entry_by`,`client_code`,`trade_code`,`patient_id`) values 
(16,'CUS00002','MD SHAHIN',NULL,'56,DASTANA',0.00,'DHAKA','KISHOREGONJ','ITNA','01236658987',NULL,1,'n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','0101',NULL),
(17,'CUS00003','MD KHALEQ',NULL,'56, AGRABAD',0.00,'SYLHET','HABIGANJ','AJMIRIGANJ','01254586595',NULL,1,'n/A','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','0101',NULL);

/*Table structure for table `expire_log` */

DROP TABLE IF EXISTS `expire_log`;

CREATE TABLE `expire_log` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `quantity` double(10,2) DEFAULT '0.00',
  `expire_date` datetime DEFAULT '0001-01-01 00:00:00',
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

/*Data for the table `expire_log` */

insert  into `expire_log`(`id`,`product_code`,`product_name`,`quantity`,`expire_date`,`batch_no`,`client_code`,`trade_code`) values 
(11,'00170001','GREEN GIANT ORGANIC SALT FREE SWEETCORN 160G',200.00,'2021-12-19 00:00:00','4534-VFD','01','0101'),
(12,'00290010','PARACHUTE ADVANSED COOLING HAIR OIL',100.00,'2021-09-30 00:00:00','5642-5667-56','01','0101'),
(13,'00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML',1000.00,'2022-01-16 00:00:00','546-453567','01','0101'),
(14,'00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM',500.00,'2022-09-20 00:00:00','89-54-453434','01','0101'),
(15,'00290009','SAVLON ANTIBACTERIAL WET WIPES 20PCS',1000.00,'2022-02-20 00:00:00','566-78967-67457','01','0101');

/*Table structure for table `ledger_detail` */

DROP TABLE IF EXISTS `ledger_detail`;

CREATE TABLE `ledger_detail` (
  `id` int NOT NULL AUTO_INCREMENT,
  `transaction_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `transaction_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `accounts_head_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `accounts_head_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `invoice` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_date` date DEFAULT NULL,
  `customer_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `customer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `cr_discount_percent` double(10,2) DEFAULT '0.00',
  `cr_discount` double(10,2) DEFAULT '0.00',
  `cr_amount` double(10,2) DEFAULT '0.00',
  `cr_total` double(10,2) DEFAULT '0.00',
  `dr_discount_percent` double(10,2) DEFAULT '0.00',
  `dr_discount` double(10,2) DEFAULT '0.00',
  `dr_amount` double(10,2) DEFAULT '0.00',
  `dr_total` double(10,2) DEFAULT '0.00',
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_time` datetime DEFAULT '1000-01-01 10:55:24',
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trx_info` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `status` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=103 DEFAULT CHARSET=utf8;

/*Data for the table `ledger_detail` */

insert  into `ledger_detail`(`id`,`transaction_type`,`transaction_id`,`accounts_head_name`,`accounts_head_id`,`invoice`,`entry_date`,`customer_name`,`supplier_name`,`customer_code`,`supplier_code`,`cr_discount_percent`,`cr_discount`,`cr_amount`,`cr_total`,`dr_discount_percent`,`dr_discount`,`dr_amount`,`dr_total`,`user_id`,`client_code`,`entry_time`,`trade_code`,`trx_info`,`status`,`description`) values 
(96,'United Commercial Bank Ltd','32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,8050000.00,8050000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.','RECIEVED',NULL),
(97,'Cash & Cash Equivalents','a6727f2b-8654-4ec3-9ae4-e6fa279c41c6','Cash & Cash Equivalents','01020000','0101000000034','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,4000000.00,4000000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:13:34','0101','Dr.','RECIEVED',NULL),
(98,'CONTRA','da62a1c9-9c14-44d9-8c86-b00d6d010044','Dutch-Bangla Bank','01050000','0101000000039','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,1200000.00,1200000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:32:13','0101','Dr.','DEPOSIT',NULL),
(99,'CONTRA','da62a1c9-9c14-44d9-8c86-b00d6d010044','United Commercial Bank Ltd','01030000','0101000000039','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,1200000.00,1200000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:32:13','0101','Cr.','WITHDRAWN',NULL),
(100,'Dutch-Bangla Bank','3c466c96-9e58-4eba-b79a-604874039b55','Dutch-Bangla Bank','01050000','0101000000040','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,88000.00,88000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:33:07','0101','Cr.','PAID',NULL),
(101,'CONTRA','e2cd0010-9819-4dac-b9bc-f46c3bfbac49','United Commercial Bank Ltd','01030000','0101000000041','2021-09-26',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,500.00,500.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:15:04','0101','Dr.','DEPOSIT',NULL),
(102,'CONTRA','e2cd0010-9819-4dac-b9bc-f46c3bfbac49','Cash & Cash Equivalents','01020000','0101000000041','2021-09-26',NULL,NULL,NULL,NULL,0.00,0.00,500.00,500.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:15:04','0101','Cr.','WITHDRAWN',NULL);

/*Table structure for table `manufacturers_info` */

DROP TABLE IF EXISTS `manufacturers_info`;

CREATE TABLE `manufacturers_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `contact_person` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `brand` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `comments` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_date` date DEFAULT '1000-01-01',
  `entry_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `status` tinyint(1) DEFAULT '1',
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

/*Data for the table `manufacturers_info` */

insert  into `manufacturers_info`(`id`,`code`,`name`,`address`,`contact_person`,`email`,`phone`,`brand`,`comments`,`entry_date`,`entry_by`,`status`,`client_code`,`trade_code`) values 
(6,'MAN00004','DEFAULT','DEFAULT','DEFAULT','a@default.com','01111111111','DEFAULT','Default','2021-09-19','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(7,'MAN00005','MARICO','65,FULLER ROAD','MARICO','sales@marico.com','01789597856','MARICO','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(8,'MAN00006','UNILEVER','UNILEVER BUILDING','UNILEVER','sales@unilever.com','215444444444','UNILEVER','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(9,'MAN00007','PRAN-RFL GROUP','PRAN-RFL GROUP TOWER','PRAN-RFL GROUP','sales@pran.com','0111111111','PRAN-RFL GROUP','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(10,'MAN00008','M. M. ISPAHANI LIMITED','N/A','M. M. ISPAHANI LIMITED','sales@ispahani.com','05244444444','M. M. ISPAHANI LIMITED','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(11,'MAN00009','SQUARE TOILETRIES','N/A','SQUARE TOILETRIES','sales@sqr.com.bd','98766456345','SQUARE TOILETRIES','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(12,'MAN00010','ACI LTD.','ACI LTD. TOWER','ACI LTD.','sals@aci.com','4636563563','ACI LTD.','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(13,'MAN00011','BASHUNDHARA GROUP LTD.','BASHUNDHARA GROUP LTD. TWER','BASHUNDHARA GROUP LTD.','sales@bdhara.com','67575673464','BASHUNDHARA GROUP LTD.','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(14,'MAN00012','PARTEX GROUP','PARTEX GROUP BUILDING','PARTEX GROUP','sales@partex.mail.com','5476755477','PARTEX GROUP','n/a','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101'),
(15,'MAN00013','DANISH GROUP','DANISH GROUP','DANISH GROUP','sale@danish.com','5684568567','DANISH GROUP','Danish group','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',1,'01','0101');

/*Table structure for table `pos_log` */

DROP TABLE IF EXISTS `pos_log`;

CREATE TABLE `pos_log` (
  `id` int NOT NULL AUTO_INCREMENT,
  `supplier_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `customer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `manufacturer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `category_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `invoice_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `transaction_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

/*Data for the table `pos_log` */

insert  into `pos_log`(`id`,`supplier_code`,`customer_code`,`manufacturer_code`,`category_code`,`product_code`,`invoice_no`,`transaction_id`,`client_code`,`trade_code`) values 
(17,'03012005','CUS00003','MAN00013','0030','00270005','0101000000043',NULL,'01','0101');

/*Table structure for table `product_event_info` */

DROP TABLE IF EXISTS `product_event_info`;

CREATE TABLE `product_event_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `transaction_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_head_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ac_head_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `transaction_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `invoice` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_date` date DEFAULT NULL,
  `customer_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `customer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `cr_discount_percent` double(10,2) DEFAULT '0.00',
  `cr_discount` double(10,2) DEFAULT '0.00',
  `cr_amount` double(10,2) DEFAULT '0.00',
  `cr_total` double(10,2) DEFAULT '0.00',
  `dr_discount_percent` double(10,2) DEFAULT '0.00',
  `dr_discount` double(10,2) DEFAULT '0.00',
  `dr_amount` double(10,2) DEFAULT '0.00',
  `dr_total` double(10,2) DEFAULT '0.00',
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_time` datetime DEFAULT '1000-01-01 10:55:24',
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trx_info` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `returned` tinyint(1) DEFAULT '0',
  `purchase_invoice` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `status` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `grand_total` double(10,2) DEFAULT '0.00',
  `description` varchar(2000) DEFAULT NULL,
  `ref_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `label` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=564 DEFAULT CHARSET=utf8;

/*Data for the table `product_event_info` */

insert  into `product_event_info`(`id`,`transaction_id`,`ac_head_name`,`ac_head_id`,`transaction_type`,`invoice`,`entry_date`,`customer_name`,`supplier_name`,`customer_code`,`supplier_code`,`cr_discount_percent`,`cr_discount`,`cr_amount`,`cr_total`,`dr_discount_percent`,`dr_discount`,`dr_amount`,`dr_total`,`user_id`,`client_code`,`entry_time`,`trade_code`,`trx_info`,`returned`,`purchase_invoice`,`status`,`grand_total`,`description`,`ref_no`,`label`) values 
(533,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','Shareholder\'s contribution ','10010000','Shareholder\'s contribution ','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,5800000.00,5800000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Cr.',0,NULL,'RECIEVED',0.00,'Initial investment by SUPER X Group','INV994954345','Shareholder\'s contribution '),
(534,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,5800000.00,5800000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.',0,NULL,'RECIEVED',0.00,'Initial investment by SUPER X Group','INV994954345','Shareholder\'s contribution '),
(535,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','Director\'s Investment','12010000','Director\'s Investment','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,1600000.00,1600000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Cr.',0,NULL,'RECIEVED',0.00,'Initial investment by Shafiqul Islam','INV994954345','Director\'s Investment'),
(536,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,1600000.00,1600000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.',0,NULL,'RECIEVED',0.00,'Initial investment by Shafiqul Islam','INV994954345','Director\'s Investment'),
(537,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','Director\'s Investment','12010000','Director\'s Investment','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,200000.00,200000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Cr.',0,NULL,'RECIEVED',0.00,'Initial investment by Matiur Rahman','INV994954345','Director\'s Investment'),
(538,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,200000.00,200000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.',0,NULL,'RECIEVED',0.00,'Initial investment by Matiur Rahman','INV994954345','Director\'s Investment'),
(539,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','Director\'s Investment','12010000','Director\'s Investment','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,200000.00,200000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Cr.',0,NULL,'RECIEVED',0.00,'Initial investment by Sanjida Rahman','INV994954345','Director\'s Investment'),
(540,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,200000.00,200000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.',0,NULL,'RECIEVED',0.00,'Initial investment by Sanjida Rahman','INV994954345','Director\'s Investment'),
(541,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','Director\'s Investment','12010000','Director\'s Investment','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,250000.00,250000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Cr.',0,NULL,'RECIEVED',0.00,'Initial investment by Abdul Karim','INV994954345','Director\'s Investment'),
(542,'32d33ee5-014a-4dc4-88f0-58cabfb6e3e5','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000033','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,250000.00,250000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:11:25','0101','Dr.',0,NULL,'RECIEVED',0.00,'Initial investment by Abdul Karim','INV994954345','Director\'s Investment'),
(543,'a6727f2b-8654-4ec3-9ae4-e6fa279c41c6','Cash & Cash Equivalents','01020000','Cash & Cash Equivalents','0101000000034','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,4000000.00,4000000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:13:34','0101','Dr.',0,NULL,'RECIEVED',0.00,'Loan from ASHA VENTURE','REC554654654','Loans and Borrowings'),
(544,'a6727f2b-8654-4ec3-9ae4-e6fa279c41c6','Loans and Borrowings','03020000','Loans and Borrowings','0101000000034','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,4000000.00,4000000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:13:34','0101','Cr.',0,NULL,'RECIEVED',0.00,'Loan from ASHA VENTURE','REC554654654','Loans and Borrowings'),
(545,'9108baeb-882e-4774-8b89-7194238eb9f3','United Commercial Bank Ltd','01030000','United Commercial Bank Ltd','0101000000035','2021-09-20',NULL,'SAGAR TRADERS',NULL,'03012005',15.00,44550.00,252450.00,252450.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','0001-01-01 00:00:00','0101','Cr.',0,'INV-4954','PAID',297000.00,NULL,NULL,'Inventory Cost'),
(546,'9108baeb-882e-4774-8b89-7194238eb9f3','Inventory Cost','08010000','Inventory Cost','0101000000035','2021-09-20',NULL,'SAGAR TRADERS',NULL,'03012005',0.00,0.00,0.00,0.00,15.00,44550.00,252450.00,252450.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','0001-01-01 00:00:00','0101','Dr.',0,'INV-4954','PAID',297000.00,NULL,NULL,'Inventory Cost'),
(547,'1dcaa3e5-1e71-454d-8e0a-d70342c9bb08','Sales Income','06010000','Sales Income','0101000000036','2021-09-20','MD SHAHIN',NULL,'CUS00002',NULL,10.00,13500.00,121500.00,121500.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:22:51','0101','Cr.',0,NULL,'PAID',135000.00,NULL,NULL,'Sales Income'),
(548,'1dcaa3e5-1e71-454d-8e0a-d70342c9bb08','Cash & Cash Equivalents','01020000','Cash & Cash Equivalents','0101000000036','2021-09-20','MD SHAHIN',NULL,'CUS00002',NULL,0.00,0.00,0.00,0.00,10.00,13500.00,121500.00,121500.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:22:51','0101','Dr.',0,NULL,'RECIEVED',135000.00,NULL,NULL,'Sales Income'),
(549,'7d74e7dd-fe6a-43de-a298-58bb631119c6','Sales Income','06010000','Sales Income','0101000000037','2021-09-20','MD KHALEQ',NULL,'CUS00003',NULL,10.00,9673.00,87057.00,87057.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:31:15','0101','Cr.',0,NULL,'PAID',96730.00,NULL,NULL,'Sales Income'),
(550,'7d74e7dd-fe6a-43de-a298-58bb631119c6','Cash & Cash Equivalents','01020000','Cash & Cash Equivalents','0101000000037','2021-09-20','MD KHALEQ',NULL,'CUS00003',NULL,0.00,0.00,0.00,0.00,10.00,9673.00,50000.00,50000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:31:15','0101','Dr.',0,NULL,'RECIEVED',96730.00,NULL,NULL,'Sales Income'),
(551,'7d74e7dd-fe6a-43de-a298-58bb631119c6','Accounts Recievable','11010000','Accounts Recievable','0101000000038','2021-09-20','MD KHALEQ',NULL,'CUS00003',NULL,0.00,0.00,0.00,0.00,10.00,9673.00,37057.00,37057.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:31:15','0101','Dr.',0,NULL,'DUE',96730.00,NULL,NULL,'Sales Income'),
(552,'da62a1c9-9c14-44d9-8c86-b00d6d010044','Dutch-Bangla Bank','01050000','CONTRA','0101000000039','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,1200000.00,1200000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:32:13','0101','Dr.',0,NULL,'DEPOSIT',0.00,'UCB TO DBBL','CONTRA4953-064','CONTRA'),
(553,'da62a1c9-9c14-44d9-8c86-b00d6d010044','United Commercial Bank Ltd','01030000','CONTRA','0101000000039','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,1200000.00,1200000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:32:13','0101','Cr.',0,NULL,'WITHDRAWN',0.00,'UCB TO DBBL','CONTRA4953-064','CONTRA'),
(554,'3c466c96-9e58-4eba-b79a-604874039b55','Rent','08030000','Rent','0101000000040','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,78000.00,78000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:33:07','0101','Dr.',0,NULL,'PAID',0.00,'N/A','RENT48959','Rent'),
(555,'3c466c96-9e58-4eba-b79a-604874039b55','Dutch-Bangla Bank','01050000','Dutch-Bangla Bank','0101000000040','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,78000.00,78000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:33:07','0101','Cr.',0,NULL,'PAID',0.00,'N/A','RENT48959','Rent'),
(556,'3c466c96-9e58-4eba-b79a-604874039b55','Appliance Bill','08020000','Appliance Bill','0101000000040','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,10000.00,10000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:33:07','0101','Dr.',0,NULL,'PAID',0.00,'N/A','RENT48959','Appliance Bill'),
(557,'3c466c96-9e58-4eba-b79a-604874039b55','Dutch-Bangla Bank','01050000','Dutch-Bangla Bank','0101000000040','2021-09-20',NULL,NULL,NULL,NULL,0.00,0.00,10000.00,10000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-20 16:33:07','0101','Cr.',0,NULL,'PAID',0.00,'N/A','RENT48959','Appliance Bill'),
(558,'e2cd0010-9819-4dac-b9bc-f46c3bfbac49','United Commercial Bank Ltd','01030000','CONTRA','0101000000041','2021-09-26',NULL,NULL,NULL,NULL,0.00,0.00,0.00,0.00,0.00,0.00,500.00,500.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:15:04','0101','Dr.',0,NULL,'DEPOSIT',0.00,NULL,'765-OIIKLM','CONTRA'),
(559,'e2cd0010-9819-4dac-b9bc-f46c3bfbac49','Cash & Cash Equivalents','01020000','CONTRA','0101000000041','2021-09-26',NULL,NULL,NULL,NULL,0.00,0.00,500.00,500.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:15:04','0101','Cr.',0,NULL,'WITHDRAWN',0.00,NULL,'765-OIIKLM','CONTRA'),
(560,'e1c4688d-f151-4986-8871-3bc9d97193c7','Sales Income','06010000','Sales Income','0101000000042','2021-09-26','G. CUSTOMER',NULL,'CUS0000X',NULL,0.00,0.00,27000.00,27000.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:48:41','0101','Cr.',0,NULL,'PAID',27000.00,NULL,NULL,'Sales Income'),
(561,'e1c4688d-f151-4986-8871-3bc9d97193c7','Cash & Cash Equivalents','01020000','Cash & Cash Equivalents','0101000000042','2021-09-26','G. CUSTOMER',NULL,'CUS0000X',NULL,0.00,0.00,0.00,0.00,0.00,0.00,27000.00,27000.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-09-26 19:48:41','0101','Dr.',0,NULL,'RECIEVED',27000.00,NULL,NULL,'Sales Income'),
(562,'5c5e0f58-3a3b-4a9a-8942-139452f5a5d7','Sales Income','06010000','Sales Income','0101000000043','2021-10-05','G. CUSTOMER',NULL,'CUS0000X',NULL,0.00,0.00,12030.00,12030.00,0.00,0.00,0.00,0.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-10-05 20:21:18','0101','Cr.',0,NULL,'PAID',12030.00,NULL,NULL,'Sales Income'),
(563,'5c5e0f58-3a3b-4a9a-8942-139452f5a5d7','Cash & Cash Equivalents','01020000','Cash & Cash Equivalents','0101000000043','2021-10-05','G. CUSTOMER',NULL,'CUS0000X',NULL,0.00,0.00,0.00,0.00,0.00,0.00,12030.00,12030.00,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','01','2021-10-05 20:21:18','0101','Dr.',0,NULL,'RECIEVED',12030.00,NULL,NULL,'Sales Income');

/*Table structure for table `product_info` */

DROP TABLE IF EXISTS `product_info`;

CREATE TABLE `product_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `dosage` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `strength` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_unit` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `quantity_in` double(10,2) DEFAULT '0.00',
  `quantity_out` double(10,2) DEFAULT '0.00',
  `quantity` double(10,2) DEFAULT '0.00',
  `unit_price` double(10,2) DEFAULT '0.00',
  `mrp_price` double(10,2) DEFAULT '0.00',
  `description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `reorder_level` double(10,2) DEFAULT '0.00',
  `subcategory` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `subcategory_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `category` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `category_code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `status` tinyint(1) DEFAULT '1',
  `manufacturer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `manufacturer` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `barcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `last_expire_date` date DEFAULT '1000-01-01',
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;

/*Data for the table `product_info` */

insert  into `product_info`(`id`,`product_code`,`product_name`,`dosage`,`strength`,`product_type`,`product_unit`,`quantity_in`,`quantity_out`,`quantity`,`unit_price`,`mrp_price`,`description`,`reorder_level`,`subcategory`,`subcategory_code`,`category`,`category_code`,`status`,`manufacturer_code`,`manufacturer`,`batch_no`,`entry_by`,`client_code`,`barcode`,`last_expire_date`,`trade_code`) values 
(33,'00290001','CLEAR MEN SHAMPOO DEEP CLEANSE',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,300.00,350.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290001','0001-01-01','0101'),
(34,'00290002','CLEAR MEN ANTI DANDRUFF SHAMPOO COOL SPORT MENTHOL',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,300.00,350.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290002','0001-01-01','0101'),
(35,'00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM',NULL,NULL,NULL,'Pc',500.00,231.00,269.00,150.00,230.00,NULL,0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER','89-54-453434',NULL,'01','00290003','2022-09-20','0101'),
(36,'00290004','SUNSILK HAIR FALL SOLUTION CONDITIONER 80ML',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,40.00,65.00,NULL,0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290004','0001-01-01','0101'),
(37,'00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML',NULL,NULL,NULL,'Pc',1000.00,620.00,380.00,150.00,270.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER','546-453567',NULL,'01','00290005','2022-01-16','0101'),
(38,'00290006','LACTOGEN 3 FOLLOW UP FORMULA TIN (13-24 MONTHS) 400GM',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,500.00,620.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290006','0001-01-01','0101'),
(39,'00290007','OLITALIA OLIVE OIL 100ML',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,150.00,195.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290007','0001-01-01','0101'),
(40,'00290008','KODOMO BABY TOOTHPASTE GRAPE 80GM',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,67.00,150.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER',NULL,NULL,'01','00290008','0001-01-01','0101'),
(41,'00290009','SAVLON ANTIBACTERIAL WET WIPES 20PCS',NULL,NULL,NULL,'Pc',1000.00,500.00,500.00,60.00,85.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00006','UNILEVER','566-78967-67457',NULL,'01','00290009','2022-02-20','0101'),
(42,'00290010','PARACHUTE ADVANSED COOLING HAIR OIL',NULL,NULL,NULL,'Pc',100.00,50.00,50.00,120.00,160.00,'n/a',0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00005','MARICO','5642-5667-56',NULL,'01','00290010','2021-09-30','0101'),
(43,'00180001','ELECTRIC KETTLE 1.8 LIT (3601788)',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,800.00,1380.00,'n/a',0.00,NULL,NULL,'APPLIANCES','0018',1,'MAN00004','DEFAULT',NULL,NULL,'01','00180001','0001-01-01','0101'),
(44,'00290011','HIMALAYA CLEAR COMPLEXION WHITENING FACE WASH 150ML^',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,137.00,275.00,NULL,0.00,NULL,NULL,'HEALTH, HOUSEHOLD & BABY CARE','0029',1,'MAN00004','DEFAULT',NULL,NULL,'01','00290011','0001-01-01','0101'),
(45,'00270001','LIFE DARK SOYA SAUCE (BIG) 410ML',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,250.00,313.00,'n/a',0.00,NULL,NULL,'GROCERY & GOURMET FOOD','0027',1,'MAN00004','DEFAULT',NULL,NULL,'01','00270001','0001-01-01','0101'),
(46,'00210001','PRAN SPECIAL TOAST 300G',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,65.00,95.00,'n/a',0.00,NULL,NULL,'CELL PHONES & ACCESSORIES','0021',1,'MAN00004','DEFAULT',NULL,NULL,'01','00210001','0001-01-01','0101'),
(47,'00270002','IFAD ORANGE BISCUIT 190GM',NULL,NULL,NULL,'Pc',0.00,0.00,0.00,20.00,35.00,'n/a',0.00,NULL,NULL,'GROCERY & GOURMET FOOD','0027',1,'MAN00004','DEFAULT',NULL,NULL,'01','00270002','0001-01-01','0101'),
(48,'00270003','IFAD ORANGE BISCUIT 300GM',NULL,NULL,NULL,NULL,0.00,0.00,0.00,40.00,50.00,'n/a',0.00,NULL,NULL,'GROCERY & GOURMET FOOD','0027',1,'MAN00004','DEFAULT',NULL,NULL,'01','00270003','0001-01-01','0101'),
(49,'00270004','BISK CLUB SUGAR FREE CRACKER BISCUIT 200±20 GM',NULL,NULL,NULL,NULL,0.00,0.00,0.00,34.00,50.00,'n/a',0.00,NULL,NULL,'GROCERY & GOURMET FOOD','0027',1,'MAN00004','DEFAULT',NULL,NULL,'01','00270004','0001-01-01','0101'),
(50,'00270005','W.F. MORNING FRESH SPICY HOT TOAST 150G',NULL,NULL,NULL,NULL,0.00,0.00,0.00,40.00,65.00,'n/a',0.00,NULL,NULL,'GROCERY & GOURMET FOOD','0027',1,'MAN00006','UNILEVER',NULL,NULL,'01','00270005','0001-01-01','0101');

/*Table structure for table `product_stock` */

DROP TABLE IF EXISTS `product_stock`;

CREATE TABLE `product_stock` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `manufacturer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `opening_stock` double(10,2) DEFAULT '0.00',
  `quantity_in` double(10,2) DEFAULT '0.00',
  `quantity_out` double(10,2) DEFAULT '0.00',
  `closing_stock` double(10,2) DEFAULT '0.00',
  `unit_price` double(10,2) DEFAULT '0.00',
  `mrp_price` double(10,2) DEFAULT '0.00',
  `expire_date` date DEFAULT '1000-01-01',
  `entry_date` date DEFAULT '1000-01-01',
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `barcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8;

/*Data for the table `product_stock` */

insert  into `product_stock`(`id`,`product_code`,`product_name`,`manufacturer_code`,`opening_stock`,`quantity_in`,`quantity_out`,`closing_stock`,`unit_price`,`mrp_price`,`expire_date`,`entry_date`,`user_id`,`batch_no`,`client_code`,`barcode`,`trade_code`) values 
(55,'00290010','PARACHUTE ADVANSED COOLING HAIR OIL','MAN00005',0.00,100.00,50.00,50.00,120.00,160.00,'0001-01-01','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','5642-5667-56','01','00290010','0101'),
(56,'00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM','MAN00006',0.00,500.00,201.00,299.00,150.00,230.00,'0001-01-01','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','89-54-453434','01','00290003','0101'),
(57,'00290009','SAVLON ANTIBACTERIAL WET WIPES 20PCS','MAN00006',0.00,1000.00,500.00,500.00,60.00,85.00,'0001-01-01','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','566-78967-67457','01','00290009','0101'),
(58,'00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006',0.00,1000.00,500.00,500.00,150.00,270.00,'0001-01-01','2021-09-20','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','546-453567','01','00290005','0101'),
(59,'00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006',500.00,0.00,100.00,400.00,150.00,270.00,'0001-01-01','2021-09-26','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290005','0101'),
(60,'00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM','MAN00006',299.00,0.00,30.00,269.00,150.00,230.00,'0001-01-01','2021-10-05','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290003','0101'),
(61,'00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006',500.00,0.00,20.00,480.00,150.00,270.00,'0001-01-01','2021-10-05','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290005','0101');

/*Table structure for table `product_stock_in` */

DROP TABLE IF EXISTS `product_stock_in`;

CREATE TABLE `product_stock_in` (
  `id` int NOT NULL AUTO_INCREMENT,
  `transaction_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `manufacturer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `quantity` double(10,2) DEFAULT '0.00',
  `unit_price` double(10,2) DEFAULT '0.00',
  `total_price` double(10,2) DEFAULT '0.00',
  `mrp_price` double(10,2) DEFAULT '0.00',
  `expire_date` date DEFAULT NULL,
  `entry_date` date DEFAULT NULL,
  `invoice` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `barcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `discount_percentage` double(10,2) DEFAULT '0.00',
  `discount` double(10,2) DEFAULT '0.00',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8;

/*Data for the table `product_stock_in` */

insert  into `product_stock_in`(`id`,`transaction_id`,`product_code`,`product_name`,`manufacturer_code`,`supplier_code`,`quantity`,`unit_price`,`total_price`,`mrp_price`,`expire_date`,`entry_date`,`invoice`,`user_id`,`batch_no`,`client_code`,`barcode`,`trade_code`,`discount_percentage`,`discount`) values 
(70,'9108baeb-882e-4774-8b89-7194238eb9f3','00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM','MAN00006','03012005',500.00,150.00,75000.00,230.00,'2022-09-20','2021-09-20','0101000000035','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','89-54-453434','01','00290003','0101',0.00,0.00),
(71,'9108baeb-882e-4774-8b89-7194238eb9f3','00290009','SAVLON ANTIBACTERIAL WET WIPES 20PCS','MAN00006','03012005',1000.00,60.00,60000.00,85.00,'2022-02-20','2021-09-20','0101000000035','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','566-78967-67457','01','00290009','0101',0.00,0.00),
(72,'9108baeb-882e-4774-8b89-7194238eb9f3','00290010','PARACHUTE ADVANSED COOLING HAIR OIL','MAN00005','03012005',100.00,120.00,12000.00,160.00,'2021-09-30','2021-09-20','0101000000035','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','5642-5667-56','01','00290010','0101',0.00,0.00),
(73,'9108baeb-882e-4774-8b89-7194238eb9f3','00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006','03012005',1000.00,150.00,150000.00,270.00,'2022-01-16','2021-09-20','0101000000035','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','546-453567','01','00290005','0101',0.00,0.00);

/*Table structure for table `product_stock_out` */

DROP TABLE IF EXISTS `product_stock_out`;

CREATE TABLE `product_stock_out` (
  `id` int NOT NULL AUTO_INCREMENT,
  `transaction_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `manufacturer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `customer_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `quantity` double(10,2) DEFAULT '0.00',
  `unit_price` double(10,2) DEFAULT '0.00',
  `discount_percentage` double(10,2) DEFAULT NULL,
  `discount` double(10,2) DEFAULT NULL,
  `total_price_deducted` double(10,2) DEFAULT '0.00',
  `total_price` double(10,2) DEFAULT '0.00',
  `mrp_price` double(10,2) DEFAULT '0.00',
  `expire_date` date DEFAULT NULL,
  `entry_date` date DEFAULT NULL,
  `invoice` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `batch_no` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `barcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8;

/*Data for the table `product_stock_out` */

insert  into `product_stock_out`(`id`,`transaction_id`,`product_code`,`product_name`,`manufacturer_code`,`customer_code`,`quantity`,`unit_price`,`discount_percentage`,`discount`,`total_price_deducted`,`total_price`,`mrp_price`,`expire_date`,`entry_date`,`invoice`,`user_id`,`batch_no`,`client_code`,`barcode`,`trade_code`) values 
(49,'1dcaa3e5-1e71-454d-8e0a-d70342c9bb08','00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006','CUS00002',500.00,150.00,0.00,0.00,135000.00,135000.00,270.00,'0001-01-01','2021-09-20','0101000000036','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290005','0101'),
(50,'7d74e7dd-fe6a-43de-a298-58bb631119c6','00290010','PARACHUTE ADVANSED COOLING HAIR OIL','MAN00005','CUS00003',50.00,120.00,0.00,0.00,8000.00,8000.00,160.00,'0001-01-01','2021-09-20','0101000000037','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290010','0101'),
(51,'7d74e7dd-fe6a-43de-a298-58bb631119c6','00290009','SAVLON ANTIBACTERIAL WET WIPES 20PCS','MAN00006','CUS00003',500.00,60.00,0.00,0.00,42500.00,42500.00,85.00,'0001-01-01','2021-09-20','0101000000037','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290009','0101'),
(52,'7d74e7dd-fe6a-43de-a298-58bb631119c6','00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM','MAN00006','CUS00003',201.00,150.00,0.00,0.00,46230.00,46230.00,230.00,'0001-01-01','2021-09-20','0101000000037','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290003','0101'),
(53,'e1c4688d-f151-4986-8871-3bc9d97193c7','00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006','CUS0000X',100.00,150.00,0.00,0.00,27000.00,27000.00,270.00,'0001-01-01','2021-09-26','0101000000042','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290005','0101'),
(54,'5c5e0f58-3a3b-4a9a-8942-139452f5a5d7','00290003','FAIR & LOVELY CREAM ADVANCED MULTI VITAMIN 100GM','MAN00006','CUS0000X',30.00,150.00,0.00,0.00,6900.00,6900.00,230.00,'0001-01-01','2021-10-05','0101000000043','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290003','0101'),
(55,'5c5e0f58-3a3b-4a9a-8942-139452f5a5d7','00290005','DOVE CONDITIONER OXYGEN MOISTURE 180ML','MAN00006','CUS0000X',20.00,150.00,5.00,270.00,5130.00,5400.00,270.00,'0001-01-01','2021-10-05','0101000000043','7353822d-ce51-478c-ac0e-8ecf4e82c8ad',NULL,'01','00290005','0101');

/*Table structure for table `subcategory_info` */

DROP TABLE IF EXISTS `subcategory_info`;

CREATE TABLE `subcategory_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `description` varchar(500) DEFAULT NULL,
  `client_code` varchar(50) DEFAULT NULL,
  `category_code` varchar(50) DEFAULT NULL,
  `category_name` varchar(100) DEFAULT NULL,
  `trade_code` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=111 DEFAULT CHARSET=utf8;

/*Data for the table `subcategory_info` */

/*Table structure for table `suppliers_info` */

DROP TABLE IF EXISTS `suppliers_info`;

CREATE TABLE `suppliers_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `company` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `address` varchar(500) DEFAULT NULL,
  `division` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `district` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `thana` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `mobile` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `supplier_type` tinyint(1) DEFAULT '1',
  `remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entry_date` date DEFAULT '1000-01-01',
  `entry_by` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `suppliers_info` */

insert  into `suppliers_info`(`id`,`code`,`name`,`company`,`address`,`division`,`district`,`thana`,`mobile`,`email`,`supplier_type`,`remarks`,`entry_date`,`entry_by`,`client_code`,`trade_code`) values 
(11,'03012004','MINA ENTERPRISE','MINA ENTERPRISE','76, BAKTAR PARA ','DHAKA','MUNSHIGANJ','GAZARIA','01545423698','mina@gmail.com',1,'n/a','2021-09-19','ADMIN','01','0101'),
(12,'03012005','SAGAR TRADERS','SAGAR TRADERS','34, AMBARKHAN','SYLHET','HABIGANJ','AJMIRIGANJ','5675674543','dalil@dfre.com',1,'n/a','2021-09-20','ADMIN','01','0101');

/*Table structure for table `trade` */

DROP TABLE IF EXISTS `trade`;

CREATE TABLE `trade` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `in_charge` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `block` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `floor` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `vat_percent` double(10,2) DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

/*Data for the table `trade` */

insert  into `trade`(`id`,`name`,`code`,`description`,`in_charge`,`client_code`,`block`,`floor`,`vat_percent`,`phone`) values 
(12,'POS','0101','N/A','Sobuj Molla','01','1','1',0.00,NULL);

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(30) DEFAULT NULL,
  `last_name` varchar(30) DEFAULT NULL,
  `email` varchar(30) DEFAULT NULL,
  `user_type` varchar(20) DEFAULT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `status` tinyint(1) DEFAULT NULL,
  `user_id` varchar(100) DEFAULT NULL,
  `added_by` varchar(50) DEFAULT NULL,
  `add_date` datetime DEFAULT '0001-01-01 00:00:00',
  `client_code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8;

/*Data for the table `user` */

insert  into `user`(`id`,`first_name`,`last_name`,`email`,`user_type`,`phone`,`status`,`user_id`,`added_by`,`add_date`,`client_code`,`trade_code`) values 
(52,'POS','ADMIN','pos.admin@nice.com','SYSADMIN','01111111111',1,'d870f273-3380-47ed-a0ec-c5cabf960270',NULL,'2021-03-07 00:00:00',NULL,NULL),
(63,'ADMIN','GlOBAL','admin@pos.com','ADMIN','12345678910',1,'7353822d-ce51-478c-ac0e-8ecf4e82c8ad','a4650d10-8f48-42e2-a4e3-370bbdb74371','2021-04-12 00:00:00','01',NULL),
(66,'HASAN','MOKUL','hasan@gmail.com','ACCOUNTS','01521208467',1,'445c39db-d1d7-47f3-a8c5-d6a1807f77d5','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','2021-09-19 14:40:36','01',NULL),
(67,'JERIN','HASAN','jerin.hasan098@gmail.com','INVENTORY','01521208468',0,'685c0f3a-bdc0-4ab8-8dcb-17468e4b80d1','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','2021-09-20 13:19:52','01',NULL),
(68,'TAWFIQUR','RAHMAN','taw@fiqur.com','ADMIN','01959658965',1,'abaa4059-e2be-4747-be2a-29ac4568a438','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','2021-09-19 00:00:00','01',NULL),
(69,'TANJIL','ANWAR','tanj@mail.com','INVENTORY','01976821178',1,'cba2485a-752d-488d-8d6e-c03642e7af90','7353822d-ce51-478c-ac0e-8ecf4e82c8ad','2021-09-20 12:46:01','01',NULL);

/*Table structure for table `user_trade` */

DROP TABLE IF EXISTS `user_trade`;

CREATE TABLE `user_trade` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `trade_code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `client_code` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;

/*Data for the table `user_trade` */

insert  into `user_trade`(`id`,`user_id`,`trade_code`,`client_code`) values 
(42,'de5a8f5d-d71c-4958-ae54-86651b4b2ffc','0101','01'),
(45,'445c39db-d1d7-47f3-a8c5-d6a1807f77d5','0101','01'),
(48,'abaa4059-e2be-4747-be2a-29ac4568a438','0101','01'),
(52,'cba2485a-752d-488d-8d6e-c03642e7af90','0101','01'),
(53,'685c0f3a-bdc0-4ab8-8dcb-17468e4b80d1','0101','01');

/*Table structure for table `z_unit_info` */

DROP TABLE IF EXISTS `z_unit_info`;

CREATE TABLE `z_unit_info` (
  `id` int NOT NULL AUTO_INCREMENT,
  `unit` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `z_unit_info` */

insert  into `z_unit_info`(`id`,`unit`) values 
(1,'Pc'),
(2,'Kg'),
(3,'Pound'),
(4,'Gram'),
(5,'Milligram'),
(6,'Dozen'),
(7,'Litre'),
(8,'Gallon'),
(9,'Ounce');

/* Procedure structure for procedure `already_expired` */

/*!50003 DROP PROCEDURE IF EXISTS  `already_expired` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `already_expired`(IN clientID VARCHAR(255),tradeID VARCHAR(255), endDate DATE)
BEGIN
	
SELECT product_code, product_name, quantity, expire_date ,batch_no FROM `expire_log` WHERE 
client_code = clientID AND
trade_code = tradeID
AND expire_date != "0001-01-01"
AND expire_date <= endDate; 
END */$$
DELIMITER ;

/* Procedure structure for procedure `available_balance` */

/*!50003 DROP PROCEDURE IF EXISTS  `available_balance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `available_balance`(IN clientID VARCHAR(255),tradeID VARCHAR(255), ac_head VARCHAR(255))
BEGIN
 select ac_head_id, ac_head_name,sum(dr_total) - Sum(cr_total) as available_balance from `product_event_info`
 where  client_code = clientID
  and trade_code = tradeID
and ac_head_id = ac_head
group by ac_head_id;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `cash_flow` */

/*!50003 DROP PROCEDURE IF EXISTS  `cash_flow` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `cash_flow`(IN
ClientCode VARCHAR(255),TradeCode VARCHAR(255), StartDate DATE, EndDate DATE)
BEGIN
SELECT p.ac_head_name AS `account`,
p.entry_date,
p.invoice,
p.customer_name,
p.supplier_name,
p.label,
p.dr_total,
p.cr_total
FROM `product_event_info` AS p LEFT JOIN `accounts_head` AS a ON a.ac_head_id = p.ac_head_id 
WHERE 
p.entry_date >= StartDate
AND p.entry_date <= EndDate
AND p.client_code = ClientCode
AND p.trade_code = TradeCode
AND a.client_code = ClientCode
AND a.ac_group_id = "01"
ORDER BY p.ac_head_name,p.entry_date;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Customer_Due` */

/*!50003 DROP PROCEDURE IF EXISTS  `Customer_Due` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `Customer_Due`(IN CustomerCode VARCHAR(255), ClientCode VARCHAR(255),TradeCode VARCHAR(255))
BEGIN
SELECT SUM(dr_total)-SUM(cr_total) AS due
FROM `product_event_info` 
WHERE customer_code = CustomerCode 
AND client_code = ClientCode 
AND trade_code = TradeCode
AND ac_head_id = "11010000";
END */$$
DELIMITER ;

/* Procedure structure for procedure `details_purchase` */

/*!50003 DROP PROCEDURE IF EXISTS  `details_purchase` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `details_purchase`( IN clientID VARCHAR(255), tradeID VARCHAR(255),startDate DATE, endDate DATE,SupplierCode VARCHAR(255))
BEGIN
	SELECT  p.entry_date ,
	p.product_name as product,
	m.name as manufacturer, 
	p.quantity as quantity,
	p.unit_price as unit_price,
	p.total_price as total
	fROM `product_stock_in` as p
	LEFT JOIN `manufacturers_info` AS m ON p.manufacturer_code = m.code
	WHERE 
        p.client_code = clientID 
        AND  p.trade_code = tradeID 
        AND p.entry_date >= startDate 
        AND  p.entry_date <= endDate
        and p.supplier_code= SupplierCode;
	END */$$
DELIMITER ;

/* Procedure structure for procedure `details_sales` */

/*!50003 DROP PROCEDURE IF EXISTS  `details_sales` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `details_sales`( IN clientID VARCHAR(255), tradeID VARCHAR(255),startDate DATE, endDate DATE)
BEGIN
	
	
	
	SELECT  p.entry_date,
	
	p.product_name AS product,
	c.name as customer_name,
	m.name AS manufacturer, 
	p.quantity AS quantity,
	p.mrp_price AS mrp,
	discount as discount,
	p.total_price AS total
	
	
	FROM `product_stock_out` AS p 
	LEFT JOIN `manufacturers_info` AS m ON p.manufacturer_code = m.code
	LEFT JOIN `customers_info` AS c ON p.customer_code = c.code
	WHERE
        p.client_code = clientID 
        AND  p.trade_code = tradeID 
        AND p.entry_date >= startDate 
        AND  p.entry_date <= endDate;
      
        
	END */$$
DELIMITER ;

/* Procedure structure for procedure `expired_notice` */

/*!50003 DROP PROCEDURE IF EXISTS  `expired_notice` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `expired_notice`(IN clientID VARCHAR(255),tradeID VARCHAR(255), endDate DATE)
BEGIN
	
select product_code, product_name, quantity, expire_date, batch_no from `expire_log` where 
client_code = clientID and
trade_code = tradeID and
expire_date <= DATE_ADD(endDate, INTERVAL 15 DAY) 
and expire_date != "0001-01-01"
and expire_date > endDate; 
END */$$
DELIMITER ;

/* Procedure structure for procedure `latest_stock_entry` */

/*!50003 DROP PROCEDURE IF EXISTS  `latest_stock_entry` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `latest_stock_entry`(in ProductCode varchar(255), ClientCode varchar(255),EntryDate date)
BEGIN
SELECT id,
product_code,
product_name,
manufacturer_code,
opening_stock,
quantity_in,
quantity_out,
closing_stock,
unit_price,
mrp_price,
expire_date,
MAX(entry_date) AS entry_date,
user_id,
batch_no,
client_code,
barcode
FROM `product_stock` WHERE entry_date <= EntryDate AND product_code = ProductCode AND client_code = ClientCode;
END */$$
DELIMITER ;

/* Procedure structure for procedure `ledger_report` */

/*!50003 DROP PROCEDURE IF EXISTS  `ledger_report` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `ledger_report`(IN
ClientCode varchar(255),TradeCode VARCHAR(255), StartDate DATE, EndDate DATE)
BEGIN
select ac_head_name as `account`,
entry_date,
invoice,
customer_name,
supplier_name,
label,
dr_total,
cr_total
from `product_event_info` 
where 
entry_date >= StartDate
AND entry_date <= EndDate
and client_code = ClientCode
And trade_code = TradeCode
ORDER BY ac_head_name,entry_date;
END */$$
DELIMITER ;

/* Procedure structure for procedure `ledger_total` */

/*!50003 DROP PROCEDURE IF EXISTS  `ledger_total` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `ledger_total`(IN
ClientCode VARCHAR(255),TradeCode VARCHAR(255), StartDate DATE, EndDate DATE)
BEGIN
SELECT 
sum(cr_total) as Debit,
SUM(dr_total) As Credit
FROM `product_event_info` 
WHERE 
entry_date >= StartDate
AND entry_date <= EndDate
AND client_code = ClientCode
AND trade_code = TradeCode
ORDER BY entry_date ;
END */$$
DELIMITER ;

/* Procedure structure for procedure `opening_balance` */

/*!50003 DROP PROCEDURE IF EXISTS  `opening_balance` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `opening_balance`(IN
ClientCode VARCHAR(255),TradeCode VARCHAR(255), StartDate DATE, EndDate DATE)
BEGIN
SELECT SUM(dr_total)as total FROM product_event_info WHERE
entry_date < StartDate
AND client_code= ClientCode AND trade_code = TradeCode
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `stock_report` */

/*!50003 DROP PROCEDURE IF EXISTS  `stock_report` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `stock_report`( IN clientID VARCHAR(255), tradeID VARCHAR(255),startDate DATE, endDate DATE)
BEGIN
	
	
	
	SELECT  p.entry_date,
	p.product_name AS product,
	m.name AS manufacturer, 
	p.opening_stock AS opening_stock,
	p.quantity_in AS `stock_in`,
	p.quantity_out as `stock_out`,
	p.closing_stock as `closing_stock`,
	p.unit_price as `unit_price`,
	p.mrp_price as `mrp_price`
	
	
	FROM `product_stock` AS p LEFT JOIN `manufacturers_info` AS m ON p.manufacturer_code = m.code
	WHERE 
        p.client_code = clientID 
        AND  p.trade_code = tradeID 
        AND p.entry_date >= startDate 
        AND  p.entry_date <= endDate;
      
        
	END */$$
DELIMITER ;

/* Procedure structure for procedure `Supplier_Due` */

/*!50003 DROP PROCEDURE IF EXISTS  `Supplier_Due` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`root`@`localhost` PROCEDURE `Supplier_Due`(IN SupplierCode VARCHAR(255), ClientCode VARCHAR(255),TradeCode VARCHAR(255))
BEGIN
SELECT Sum(cr_total)-Sum(dr_total) as due
FROM `product_event_info` 
WHERE supplier_code = SupplierCode 
AND client_code = ClientCode 
AND trade_code = TradeCode
and ac_head_id = "03010000";
END */$$
DELIMITER ;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
