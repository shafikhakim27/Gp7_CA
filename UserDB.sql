-- Setup of Database, run selected instruction when necessary
DROP DATABASE IF EXISTS userDB;
CREATE DATABASE userDB;
USE userDB;

-- Create User table with enhanced constraints
CREATE TABLE IF NOT EXISTS `User` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(255) NOT NULL UNIQUE,
    `password` VARCHAR(255) NOT NULL,
    `completionTime` DOUBLE DEFAULT NULL,
    `isPaidUser` TINYINT(1) DEFAULT 0,
    `createdAt` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `updatedAt` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_completionTime (`completionTime`),
    INDEX idx_isPaidUser (`isPaidUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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

-- Optional: Stored Procedure for Safe Completion Time Update
DELIMITER //
CREATE PROCEDURE UpdateCompletionTimeSafe(
    IN p_userId INT,
    IN p_completionTime DOUBLE
)
BEGIN
    -- Only update if new time is faster or if no time exists
    UPDATE `User`
    SET `completionTime` = p_completionTime
    WHERE `id` = p_userId
      AND (`completionTime` IS NULL OR p_completionTime < `completionTime`);
END //
DELIMITER ;

-- Optional: View for Leaderboard (simplifies queries)
CREATE OR REPLACE VIEW LeaderboardView AS
SELECT 
    ROW_NUMBER() OVER (ORDER BY completionTime ASC) AS `rank`,
    `id`,
    `username`,
    `completionTime`,
    `isPaidUser`
FROM `User`
WHERE `completionTime` IS NOT NULL
ORDER BY `completionTime` ASC;