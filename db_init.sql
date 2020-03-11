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
CREATE TABLE IF NOT EXISTS `ticketnow`.`User` (
  `id_user` VARCHAR(10) NOT NULL,
  `email` VARCHAR(320) NOT NULL,
  `password_hash` VARCHAR(64) NOT NULL,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_user`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `umclinic`.`Doctor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ticketnow`.`Ticket`;
CREATE TABLE IF NOT EXISTS `ticketnow`.`Ticket` (
  `id_ticket` INT NOT NULL,
  `price` INT NOT NULL,
  `type` ENUM('simple','complete') NOT NULL,
  `purchased_datetime` DATETIME NOT NULL,
  `used` BOOLEAN NOT NULL,
  PRIMARY KEY (`id_ticket`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `umclinic`.`Modality`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ticketnow`.`History` (
  `used_datetime` DATETIME NOT NULL,
  `id_ticket` INT NOT NULL,
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
