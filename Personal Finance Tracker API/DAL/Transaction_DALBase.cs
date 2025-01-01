using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Personal_Finance_Tracker_API.Models;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_API.DAL
{
    public class Transaction_DALBase : DAL_Helpers
    {
        #region Get All Transactions
        public List<TransactionModel> GetAllTransactions(int UserID)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_GetAll");
                db.AddInParameter(cmd,"@UserID",DbType.Int64,UserID);
                IDataReader rd = db.ExecuteReader(cmd);
                if (rd != null) 
                {
                    while (rd.Read()) 
                    {
                        TransactionModel transaction = new TransactionModel();
                        transaction.TransactionID = (int)rd["TransactionID"];
                        transaction.UserID = (int)rd["UserID"];
                        transaction.Amount = (decimal)rd["Amount"];
                        transaction.Type = rd["Type"].ToString();
                        transaction.Category = rd["Category"].ToString();
                        transaction.Date = (DateTime)rd["Date"];
                        transaction.Description = rd["Description"].ToString();
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
            catch
            {
                return transactions;
            }
        }
        #endregion
    }
}
