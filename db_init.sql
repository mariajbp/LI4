
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema ticketnow
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ticketnow` DEFAULT CHARACTER SET utf8 ;
USE `ticketnow` ;

-- -----------------------------------------------------
-- Table `ticketnow`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`User`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`User` (
  `id_user` VARCHAR(10) NOT NULL,
  `email` VARCHAR(320) NOT NULL,
  `password_hash` VARCHAR(200) NOT NULL,
  `name` VARCHAR(100),
  `permissions` INT DEFAULT 0 NOT NULL,
  PRIMARY KEY (`id_user`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Mapping Table `ticketnow`.`TicketType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`TicketType`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`TicketType` (
  `type` TINYINT NOT NULL,
  `price` FLOAT NOT NULL,
  `name` VARCHAR(25) NOT NULL UNIQUE,
  PRIMARY KEY (`type`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`History`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`History`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`History` (
  `used_datetime` DATETIME NOT NULL,
  `id_ticket` BINARY(32) NOT NULL,
  `id_user` VARCHAR(10) NOT NULL,
  INDEX used_datetime_idx (`used_datetime` ASC),
  CONSTRAINT FOREIGN KEY (`id_ticket`)
  REFERENCES `ticketnow`.`Ticket` (`id_ticket`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FOREIGN KEY (`id_user`)
  REFERENCES `ticketnow`.`User` (`id_user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    PRIMARY KEY (`id_ticket`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`Transaction`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Transaction`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Transaction` (
  `id_transaction` VARCHAR(16) NOT NULL,
  `item_number` TINYINT NOT NULL,
  `id_user` VARCHAR(10) NOT NULL,
  `id_ticket` BINARY(32) NOT NULL,
  `total_price` FLOAT NOT NULL,
  `datetime` DATETIME NOT NULL,
  CONSTRAINT FOREIGN KEY (`id_ticket`)
  REFERENCES `ticketnow`.`Ticket` (`id_ticket`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FOREIGN KEY (`id_user`)
  REFERENCES `ticketnow`.`User` (`id_user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  PRIMARY KEY (`id_transaction`,`item_number`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `ticketnow`.`Ticket`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Ticket`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Ticket` (
  `id_ticket` BINARY(32) NOT NULL,
  `id_user` VARCHAR(10) NOT NULL,
  `type` TINYINT NOT NULL,
  `used` BOOLEAN DEFAULT false NOT NULL,
  CONSTRAINT FOREIGN KEY (`id_user`)
  REFERENCES `ticketnow`.`User` (`id_user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FOREIGN KEY (`type`)
  REFERENCES `ticketnow`.`TicketType` (`type`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    PRIMARY KEY (`id_ticket`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Mapping Table `ticketnow`.`Location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Location`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Location` (
  `id_location` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(32) NOT NULL UNIQUE,
  PRIMARY KEY (`id_location`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Mapping Table `ticketnow`.`MealType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`MealType`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`MealType` (
  `id_meal_type` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(32) NOT NULL UNIQUE,
  PRIMARY KEY (`id_meal_type`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `ticketnow`.`Meal`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Meal`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Meal` (
  `date` DATE NOT NULL,
  `id_location` INT NOT NULL,
  `soup` VARCHAR(32) NOT NULL,
  `main_dish` VARCHAR(32) NOT NULL,
  `id_meal_type` INT NOT NULL,
  `description` VARCHAR(64) NOT NULL,
  CONSTRAINT FOREIGN KEY (`id_location`)
  REFERENCES `ticketnow`.`Location` (`id_location`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT FOREIGN KEY (`id_meal_type`)
  REFERENCES `ticketnow`.`MealType` (`id_meal_type`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  PRIMARY KEY (`date`,`id_location`))
ENGINE = InnoDB;

