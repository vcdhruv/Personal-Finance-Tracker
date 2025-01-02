namespace Personal_Finance_Tracker_API.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
