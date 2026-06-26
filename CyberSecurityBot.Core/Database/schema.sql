-- CyberSecurityBot — Part 3 database schema (MySQL / MariaDB)
-- Run automatically at startup by DatabaseInitializer, but kept here for reference.

CREATE DATABASE IF NOT EXISTS `cybersecuritybot`
    CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `cybersecuritybot`;

CREATE TABLE IF NOT EXISTS tasks (
    id            INT AUTO_INCREMENT PRIMARY KEY,
    title         VARCHAR(255) NOT NULL,
    description   TEXT NULL,
    reminder      VARCHAR(255) NULL,
    reminder_date DATETIME NULL,
    is_complete   TINYINT(1) NOT NULL DEFAULT 0,
    created_at    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
