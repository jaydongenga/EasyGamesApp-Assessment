USE EasyGamesDB;
GO

-- Insert Transaction Types
IF NOT EXISTS (SELECT 1 FROM TransactionType)
BEGIN
    INSERT INTO TransactionType (TransactionTypeName)
    VALUES ('Debit'), ('Credit');
END
GO

-- Insert Clients
IF NOT EXISTS (SELECT 1 FROM Client)
BEGIN
    INSERT INTO Client (Name, Surname, ClientBalance)
    VALUES
    ('Peter', 'Parker', 1000.00),
    ('Tony', 'Stark', 800000.00),
    ('Bruce', 'Banner', 254111.00);
END
GO