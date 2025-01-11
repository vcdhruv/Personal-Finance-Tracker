using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class Transaction_BALBase
    {
        #region Get All Transactions Of Specific User
        public List<TransactionModel> GetAllTransactions(int UserID, string? Type = null, string? Category = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            Transaction_DALBase transactions = new Transaction_DALBase();
            return transactions.GetAllTransactions(UserID, Type, Category, StartDate, EndDate);
        }
        #endregion

        #region Get Transaction Of Specific User By Transaction ID
        public TransactionModel GetTransactionByID(int TransactionID,int UserID)
        {
            Transaction_DALBase transactions = new Transaction_DALBase();
            return transactions.GetTransactionByID(TransactionID,UserID);
        }
        #endregion

        #region Add New Transaction Of Specific User
        public bool AddNewTransaction(TransactionModel transaction)
        {
            Transaction_DALBase transaction_DALBase = new Transaction_DALBase();
            return transaction_DALBase.AddTransaction(transaction);
        }
        #endregion

        #region Update Transaction Of Specific User
        public bool UpdateTransaction(TransactionModel transaction)
        {
            Transaction_DALBase transaction_ = new Transaction_DALBase();
            return transaction_.UpdateTransaction(transaction);
        }
        #endregion

        #region Delete Transaction Of Specific User
        public bool DeleteTransaction(int UserID, int TransactionID)
        {
            Transaction_DALBase transaction_ = new Transaction_DALBase();
            return transaction_.DeleteTransaction(UserID, TransactionID);
        }
        #endregion
    }
}
