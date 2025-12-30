-- Setup of Database, run selected instruction when necessary
DROP DATABASE IF EXISTS userDB;
CREATE DATABASE userDB;
USE userDB;

-- Create User table
CREATE TABLE IF NOT EXISTS `User` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `completionTime` DOUBLE DEFAULT NULL,
    `isPaidUser` TINYINT(1) NULL DEFAULT NULL
    );

-- Insert 5 free users
INSERT INTO `User` (`username`, `password`, `completionTime`, `isPaidUser`) VALUES
('shirley', '123', NULL, 0),
('george', '123', 45.5, 0),
('chenyu', '123', NULL, 0),
('haoting', '123', 120.75, 0),
('frescylia', '123', 90.0, 0);

-- Insert 2 paid users
INSERT INTO `User` (`username`, `password`, `completionTime`, `isPaidUser`) VALUES
('shaq', '12345', 30.25, 1),
('shaw', '12345', NULL, 1);

-- View results
SHOW TABLES;
DESCRIBE User;
SELECT * FROM User;