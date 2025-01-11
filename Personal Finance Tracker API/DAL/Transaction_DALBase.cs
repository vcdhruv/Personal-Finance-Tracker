using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Personal_Finance_Tracker_API.Models;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_API.DAL
{
    public class Transaction_DALBase : DAL_Helpers
    {
        #region Get All Transactions Of Specific User
        public List<TransactionModel> GetAllTransactions(int UserID,string? Type = null,string? Category = null,DateTime? StartDate = null,DateTime? EndDate = null )
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_GetAll");
                db.AddInParameter(cmd,"@UserID",DbType.Int64,UserID);
                db.AddInParameter(cmd, "@Type", DbType.String, Type);
                db.AddInParameter(cmd, "@Category", DbType.String, Category);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, EndDate);

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
                        transaction.Date = rd["FormattedDate"].ToString();
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

        #region Get Transaction Of Specific User By Transaction ID
        public TransactionModel GetTransactionByID(int TransactionID,int UserID)
        {
            TransactionModel transaction = new TransactionModel();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_GetByID");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                db.AddInParameter(cmd, "@TransactionID", DbType.Int64, TransactionID);

                IDataReader rd = db.ExecuteReader(cmd);
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        transaction.TransactionID = (int)rd["TransactionID"];
                        transaction.UserID = (int)rd["UserID"];
                        transaction.Amount = (decimal)rd["Amount"];
                        transaction.Type = rd["Type"].ToString();
                        transaction.Category = rd["Category"].ToString();
                        transaction.Date = rd["Date"].ToString();
                        transaction.Description = rd["Description"].ToString();
                        
                    }
                }
                return transaction;
            }
            catch
            {
                return transaction;
            }
        }
        #endregion

        #region Add New Transaction Of Specific User
        public bool AddTransaction(TransactionModel transaction)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_POST");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, transaction.UserID);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, transaction.Amount);
                db.AddInParameter(cmd, "@Type", DbType.String, transaction.Type);
                db.AddInParameter(cmd, "@Date", DbType.DateTime, transaction.Date);
                db.AddInParameter(cmd, "@Category", DbType.String, transaction.Category);
                db.AddInParameter(cmd, "@Description", DbType.String, transaction.Description);
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update Transaction Of Specific User
        public bool UpdateTransaction(TransactionModel transaction)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_PUT");
                db.AddInParameter(cmd, "@TransactionID", DbType.Int64, transaction.TransactionID);
                db.AddInParameter(cmd, "@UserID", DbType.Int64, transaction.UserID);
                db.AddInParameter(cmd,"@Amount",DbType.Decimal,transaction.Amount);
                db.AddInParameter(cmd,"@Type",DbType.String, transaction.Type);
                db.AddInParameter(cmd,"@Category",DbType.String,transaction.Category);
                db.AddInParameter(cmd,"@Date",DbType.DateTime,DateTime.Parse(transaction.Date));
                db.AddInParameter(cmd, "@Description", DbType.String, transaction.Description);
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete Transaction Of Specific User
        public bool DeleteTransaction(int UserID, int TransactionID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Transactions_DELETE");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                db.AddInParameter(cmd, "@TransactionID", DbType.Int64, TransactionID);
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
