-- Active: 1734884106177@@127.0.0.1@3306@client_schedule
CREATE TABLE city (
    cityId INT(10) NOT NULL,
    city VARCHAR(50),
    countryId INT(10),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    PRIMARY KEY (cityID)
)

CREATE TABLE country (
    countryId INT(10) NOT NULL,
    country VARCHAR(50),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdatedBy VARCHAR(40),
    PRIMARY KEY (countryId)
)

CREATE TABLE address (
    addressId INT(10) NOT NULL,
    address VARCHAR(50),
    address2 VARCHAR(50),
    cityId INT(10),
    postalCode VARCHAR(10),
    phone VARCHAR(20),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdatedBy VARCHAR(40),
    PRIMARY KEY (addressId)
)

CREATE TABLE customer (
    customerId INT(10) NOT NULL,
    customerName VARCHAR(45),
    addressId INT(10),
    active TINYINT(1),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdatedBy VARCHAR(40),
    PRIMARY KEY (customerId)
)

CREATE TABLE appointment (
    appointmentId INT(10) NOT NULL,
    customerId INT(10),
    userId INT,
    title VARCHAR(255),
    description TEXT,
    location TEXT,
    contact TEXT,
    type TEXT,
    url VARCHAR(255),
    start DATETIME,
    end DATETIME,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    PRIMARY KEY (appointmentId)
)

CREATE TABLE user (
    userId INT NOT NULL,
    userName VARCHAR(50),
    password VARCHAR(50),
    active TINYINT,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    PRIMARY KEY (userId)
)

ALTER TABLE city
ADD FOREIGN KEY (countryId) REFERENCES country(countryId);

ALTER TABLE address
ADD FOREIGN KEY (cityId) REFERENCES city(cityId);

ALTER TABLE customer
ADD FOREIGN KEY (addressId) REFERENCES address(addressId);

ALTER TABLE appointment
ADD FOREIGN KEY (customerId) REFERENCES customer(customerId);
ALTER TABLE appointment
ADD FOREIGN KEY (userId) REFERENCES user(userId);