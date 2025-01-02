namespace Personal_Finance_Tracker_API.Models
{
    public class BudgetModel
    {
        public int BudgetID { get; set; }
        public int UserID { get; set; }
        public string Category {  get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
    }
}
