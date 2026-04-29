USE EasyGamesDB;
GO

-- 1. Get all clients (with optional search + sort)
CREATE OR ALTER PROCEDURE sp_GetClients
    @SearchTerm NVARCHAR(50) = NULL,
    @SortOrder  NVARCHAR(20) = 'name_asc'
AS
BEGIN
    SELECT ClientID, Name, Surname, ClientBalance
    FROM Client
    WHERE (@SearchTerm IS NULL
           OR Name    LIKE '%' + @SearchTerm + '%'
           OR Surname LIKE '%' + @SearchTerm + '%')
    ORDER BY
        CASE WHEN @SortOrder = 'name_asc'     THEN Name    END ASC,
        CASE WHEN @SortOrder = 'name_desc'    THEN Name    END DESC,
        CASE WHEN @SortOrder = 'balance_asc'  THEN ClientBalance END ASC,
        CASE WHEN @SortOrder = 'balance_desc' THEN ClientBalance END DESC,
        ClientID ASC;
END
GO

-- 2. Get one client by ID
CREATE OR ALTER PROCEDURE sp_GetClientByID
    @ClientID INT
AS
BEGIN
    SELECT ClientID, Name, Surname, ClientBalance
    FROM Client
    WHERE ClientID = @ClientID;
END
GO

-- 3. Get all transactions for a client
CREATE OR ALTER PROCEDURE sp_GetTransactionsByClient
    @ClientID INT
AS
BEGIN
    SELECT
        t.TransactionID,
        t.Amount,
        t.TransactionTypeID,
        t.ClientID,
        t.Comment,
        tt.TransactionTypeName,
        c.Name + ' ' + c.Surname AS ClientName
    FROM [Transaction] t
    INNER JOIN TransactionType tt ON t.TransactionTypeID = tt.TransactionTypeID
    INNER JOIN Client           c  ON t.ClientID          = c.ClientID
    WHERE t.ClientID = @ClientID
    ORDER BY t.TransactionID DESC;
END
GO

-- 4. Add a new transaction AND update client balance
CREATE OR ALTER PROCEDURE sp_AddTransaction
    @Amount           DECIMAL(18,2),
    @TransactionTypeID SMALLINT,
    @ClientID         INT,
    @Comment          NVARCHAR(100) = NULL
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insert the transaction
        INSERT INTO [Transaction] (Amount, TransactionTypeID, ClientID, Comment)
        VALUES (@Amount, @TransactionTypeID, @ClientID, @Comment);

        -- Update client balance: Credit adds, Debit subtracts
        UPDATE Client
        SET ClientBalance = ClientBalance +
            CASE WHEN @TransactionTypeID = 2 THEN ABS(@Amount)   -- Credit
                 ELSE -ABS(@Amount)                               -- Debit
            END
        WHERE ClientID = @ClientID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 5. Edit a transaction comment
CREATE OR ALTER PROCEDURE sp_UpdateComment
    @TransactionID BIGINT,
    @Comment       NVARCHAR(100)
AS
BEGIN
    UPDATE [Transaction]
    SET Comment = @Comment
    WHERE TransactionID = @TransactionID;
END
GO

-- 6. Get all transaction types (Debit / Credit)
CREATE OR ALTER PROCEDURE sp_GetTransactionTypes
AS
BEGIN
    SELECT TransactionTypeID, TransactionTypeName
    FROM TransactionType;
END
GO