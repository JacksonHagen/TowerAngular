CREATE TABLE IF NOT EXISTS accounts(
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS towerevents(
    id INT NOT NULL AUTO_INCREMENT primary key,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255),
    coverImg VARCHAR(255),
    location VARCHAR(255),
    capacity INT,
    startDate DATETIME,
    isCanceled TINYINT,
    type VARCHAR(255),
    FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS tickets(
    id INT NOT NULL AUTO_INCREMENT primary key,
    eventId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY(eventId) REFERENCES towerevents(id),
    FOREIGN KEY(creatorId) REFERENCES accounts(id)
) default charset utf8;

CREATE TABLE IF NOT EXISTS comments(
    id INT NOT NULL AUTO_INCREMENT primary key,
    eventId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    body TEXT,
    isAttending TINYINT,
    FOREIGN KEY(eventId) REFERENCES towerevents(id),
    FOREIGN KEY(creatorId) REFERENCES accounts(id)
) default charset utf8;