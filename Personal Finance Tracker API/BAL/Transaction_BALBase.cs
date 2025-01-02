using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class Transaction_BALBase
    {
        #region Get All Transactions Of Specific User
        public List<TransactionModel> GetAllTransactions(int UserID)
        {
            Transaction_DALBase transactions = new Transaction_DALBase();
            return transactions.GetAllTransactions(UserID);
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
        public bool UpdateTransaction(TransactionModel transaction,int UserID, int TransactionID)
        {
            Transaction_DALBase transaction_ = new Transaction_DALBase();
            return transaction_.UpdateTransaction(transaction,UserID,TransactionID);
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
