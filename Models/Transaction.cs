namespace EasyGamesApp.Models
{
    public class Transaction
    {
        public long TransactionID { get; set; }
        public decimal Amount { get; set; }
        public short TransactionTypeID { get; set; }
        public int ClientID { get; set; }
        public string? Comment { get; set; }
        // Extra fields for display (joined from other tables)
        public string TransactionTypeName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
    }
}