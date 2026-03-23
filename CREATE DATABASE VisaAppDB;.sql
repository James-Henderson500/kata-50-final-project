CREATE DATABASE VisaAppDB;
GO

USE VisaAppDB;
GO

CREATE TABLE Country (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CountryCode CHAR(2) NOT NULL,
    CountryName VARCHAR(100) NOT NULL
);

INSERT INTO Country (CountryCode, CountryName)
VALUES
('GB', 'Great Britain'),
('US', 'United States'),
('CA', 'Canada'),
('AU', 'Australia'),
('IN', 'India'),
('CN', 'China'),
('FR', 'France'),
('DE', 'Germany'),
('JP', 'Japan');
GO

CREATE TABLE ApplicationStatus (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StatusName VARCHAR(50) NOT NULL
);

INSERT INTO ApplicationStatus (StatusName)
VALUES
('New'),
('Reviewed'),
('Approved'),
('Rejected');
GO

CREATE TABLE VisaType (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL 
);

INSERT INTO VisaType (TypeName)
VALUES
('Tourist'),
('Work'),
('Student');
GO

CREATE TABLE VisaApplication (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    PassportNumber VARCHAR(50) NOT NULL,
    Nationality INT NOT NULL,
    ApplicationDate DATE NOT NULL DEFAULT (GETDATE()),
    StatusId INT NOT NULL,
    VisaTypeId INT NOT NULL,
    FOREIGN KEY (Nationality) REFERENCES Country(Id),
    FOREIGN KEY (StatusId) REFERENCES ApplicationStatus(Id),
    FOREIGN KEY (VisaTypeId) REFERENCES VisaType(Id)
);