namespace Personal_Finance_Tracker_API.Models
{
    public class ReportModel
    {
        public int UserID { get; set; }
        public decimal Total_Income { get; set; }
        public decimal Total_Expense { get; set; }
        public decimal Savings { get; set; }
        public decimal Percentage { get; set; }
        public decimal Category_Wise_Spent { get; set; }
        public string Category { get; set; }

    }
}
