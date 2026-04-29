-- Create database ONLY if it does not exist
IF DB_ID('EasyGamesDB') IS NULL
BEGIN
    CREATE DATABASE EasyGamesDB;
END
GO

USE EasyGamesDB;
GO

-- Create TransactionType table if not exists
IF OBJECT_ID('TransactionType', 'U') IS NULL
BEGIN
    CREATE TABLE TransactionType (
        TransactionTypeID SMALLINT PRIMARY KEY IDENTITY(1,1),
        TransactionTypeName NVARCHAR(50) NOT NULL
    );
END

-- Create Client table if not exists
IF OBJECT_ID('Client', 'U') IS NULL
BEGIN
    CREATE TABLE Client (
        ClientID INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(50) NOT NULL,
        Surname NVARCHAR(50) NOT NULL,
        ClientBalance DECIMAL(18,2) NOT NULL DEFAULT 0
    );
END

-- Create Transaction table if not exists
IF OBJECT_ID('Transaction', 'U') IS NULL
BEGIN
    CREATE TABLE [Transaction] (
        TransactionID BIGINT PRIMARY KEY IDENTITY(1,1),
        Amount DECIMAL(18,2) NOT NULL,
        TransactionTypeID SMALLINT NOT NULL,
        ClientID INT NOT NULL,
        Comment NVARCHAR(100) NULL,
        FOREIGN KEY (TransactionTypeID) REFERENCES TransactionType(TransactionTypeID),
        FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
    );
END

-- Insert Transaction Types if empty
IF NOT EXISTS (SELECT 1 FROM TransactionType)
BEGIN
    INSERT INTO TransactionType (TransactionTypeName)
    VALUES ('Debit'), ('Credit');
END

-- Insert Clients if empty
IF NOT EXISTS (SELECT 1 FROM Client)
BEGIN
    INSERT INTO Client (Name, Surname, ClientBalance)
    VALUES
        ('Peter', 'Parker', 1000.00),
        ('Tony',  'Stark',  800000.00),
        ('Bruce', 'Banner', 254111.00);
END