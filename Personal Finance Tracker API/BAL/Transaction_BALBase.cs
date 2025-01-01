using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class Transaction_BALBase
    {
        #region Get All Transactions
        public List<TransactionModel> GetAllTransactions(int UserID)
        {
            Transaction_DALBase transactions = new Transaction_DALBase();
            return transactions.GetAllTransactions(UserID);
        }
        #endregion
    }
}
