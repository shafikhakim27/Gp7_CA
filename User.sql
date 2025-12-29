-- Setup of Database, run selected instruction when necessary
DROP DATABASE IF EXISTS User;
CREATE DATABASE User;
USE User;

-- Create User table
CREATE TABLE IF NOT EXISTS `User` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `completionTime` DOUBLE NULL,
    `isPaidUser` BOOLEAN NULL DEFAULT FALSE
    );

-- Insert 5 free users
INSERT INTO `User` (`username`, `password`, `completionTime`, `isPaidUser`) VALUES
('shirley', '123', NULL, FALSE),
('george', '123', 45.5, FALSE),
('chenyu', '123', NULL, FALSE),
('haoting', '123', 120.75, FALSE),
('frescylia', '123', 90.0, FALSE);

-- Insert 2 paid users
INSERT INTO `User` (`username`, `password`, `completionTime`, `isPaidUser`) VALUES
('shaq', '12345', 30.25, TRUE),
('shaw', '12345', NULL, TRUE);

-- View results
SHOW TABLES;
DESCRIBE User;
SELECT * FROM User;