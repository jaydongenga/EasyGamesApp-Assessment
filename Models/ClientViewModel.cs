namespace EasyGamesApp.Models
{
    public class ClientViewModel
    {
        public List<Client> Clients { get; set; } = new();
        public Client? SelectedClient { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
        public List<TransactionType> TransactionTypes { get; set; } = new();
        public int SelectedClientID { get; set; }
        public string? SearchTerm { get; set; }
        public string SortOrder { get; set; } = string.Empty;
    }
}