using Dapper;
using EasyGamesApp.Models;
using Microsoft.Data.SqlClient;

namespace EasyGamesApp.Data
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Helper: creates a new database connection
        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        // ── Get all clients (with optional search and sort) ──────────────────
        public IEnumerable<Client> GetClients(string? searchTerm = null, string sortOrder = "name_asc")
        {
            using var conn = GetConnection();
            return conn.Query<Client>(
                "sp_GetClients",
                new { SearchTerm = searchTerm, SortOrder = sortOrder },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        // ── Get a single client by ID ─────────────────────────────────────────
        public Client? GetClientByID(int clientID)
        {
            using var conn = GetConnection();
            return conn.QueryFirstOrDefault<Client>(
                "sp_GetClientByID",
                new { ClientID = clientID },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        // ── Get all transactions for a client ─────────────────────────────────
        public IEnumerable<Transaction> GetTransactionsByClient(int clientID)
        {
            using var conn = GetConnection();
            return conn.Query<Transaction>(
                "sp_GetTransactionsByClient",
                new { ClientID = clientID },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        // ── Add a new transaction (also updates client balance) ───────────────
        public void AddTransaction(decimal amount, short transactionTypeID, int clientID, string? comment)
        {
            using var conn = GetConnection();
            conn.Execute(
                "sp_AddTransaction",
                new
                {
                    Amount = amount,
                    TransactionTypeID = transactionTypeID,
                    ClientID = clientID,
                    Comment = comment
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        // ── Update a transaction comment ──────────────────────────────────────
        public void UpdateComment(long transactionID, string comment)
        {
            using var conn = GetConnection();
            conn.Execute(
                "sp_UpdateComment",
                new { TransactionID = transactionID, Comment = comment },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        // ── Get all transaction types (Debit / Credit) ────────────────────────
        public IEnumerable<TransactionType> GetTransactionTypes()
        {
            using var conn = GetConnection();
            return conn.Query<TransactionType>(
                "sp_GetTransactionTypes",
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
    }
}