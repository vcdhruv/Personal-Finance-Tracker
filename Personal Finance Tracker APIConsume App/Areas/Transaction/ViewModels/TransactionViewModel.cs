using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.Transaction.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Amount Is Required")]
        [Range(1,int.MaxValue,ErrorMessage = "Only Positive Amount Allowed.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Type Selection Is Required")]
        public Type Type { get; set; }
        [Required(ErrorMessage = "Category Is Required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Date Is Required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }
    }

    public enum Type
    {
        Income,
        Expense
    }
}
