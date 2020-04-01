-- MySQL Workbench Forward Engineering

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
  `permissions` INT NOT NULL DEFAULT = 0,
  PRIMARY KEY (`id_user`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`TicketType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`TicketType`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`TicketType` (
  `type` TINYINT NOT NULL,
  `price` FLOAT NOT NULL,
  `name` VARCHAR(25) NOT NULL UNIQUE,
  PRIMARY KEY (`type`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`Ticket`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Ticket`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Ticket` (
  `id_user` VARCHAR(10) NOT NULL,
  `id_ticket` VARCHAR(16) NOT NULL,
  `type` TINYINT NOT NULL,
  `used` BOOLEAN NOT NULL DEFAULT = false,
  PRIMARY KEY (`id_ticket`),
  CONSTRAINT `id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `ticketnow`.`User` (`id_user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    CONSTRAINT `type`
  FOREIGN KEY (`type`)
  REFERENCES `ticketnow`.`TicketType` (`type`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`History`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`History`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`History` (
  `used_datetime` DATETIME NOT NULL,
  `id_ticket` VARCHAR(16) NOT NULL,
  `id_user` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`used_datetime`),
  INDEX used_datetime_idx (`used_datetime` ASC),
  CONSTRAINT `id_ticket`
  FOREIGN KEY (`id_ticket`)
  REFERENCES `ticketnow`.`Ticket` (`id_ticket`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_user`
  FOREIGN KEY (`id_user`)
  REFERENCES `ticketnow`.`User` (`id_user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ticketnow`.`Transaction`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Transaction`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Transaction` (
  `id_transaction` VARCHAR(16) NOT NULL,
  `count` tinyint NOT NULL,
  `id_user` VARCHAR(10) NOT NULL,
  `id_ticket` VARCHAR(16) NOT NULL,
  `total_price` FLOAT NOT NULL,
  `used_datetime` DATETIME NOT NULL,
  PRIMARY KEY (`id_transaction`,`id_user`))
ENGINE = InnoDB;
