namespace EasyGamesApp.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public decimal ClientBalance { get; set; }
        // Helper: full name for dropdown display
        public string FullName => $"{Name} {Surname}";
    }
}