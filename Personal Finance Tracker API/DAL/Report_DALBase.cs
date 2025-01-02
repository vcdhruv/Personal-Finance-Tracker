using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Personal_Finance_Tracker_API.Models;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_API.DAL
{
    public class Report_DALBase : DAL_Helpers
    {
        #region Spending Summary Response
        public List<ReportModel> SpendingSummary(int UserID)
        {
            List<ReportModel> spendings = new List<ReportModel>();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Reports_SpendingSummary");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                IDataReader rd = db.ExecuteReader(cmd);
                if (rd != null) 
                {
                    while (rd.Read()) 
                    {
                        ReportModel spending = new ReportModel();
                        spending.UserID = UserID;
                        spending.Category = rd["Category"].ToString();
                        spending.Category_Wise_Spent = (decimal)rd["Total_Spent"];
                        spending.Percentage = (decimal)rd["Percentage"];
                        spendings.Add(spending);
                    }
                }
                return spendings;
            }
            catch
            {
                return spendings;
            }
        }
        #endregion

        #region Get Savings Progress
        public List<ReportModel> SavingsProgress(int UserID)
        {
            List<ReportModel> savings = new List<ReportModel>();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Reports_SavingsProgress");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                IDataReader rd = db.ExecuteReader(cmd);
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        ReportModel saving = new ReportModel();
                        saving.UserID = UserID;
                        saving.Total_Income = (decimal)rd["Total_Income"];
                        saving.Total_Expense = (decimal)rd["Total_Expense"];
                        saving.Savings = (decimal)rd["Savings"];
                        savings.Add(saving);
                    }
                }
                return savings;
            }
            catch
            {
                return savings;
            }
        }
        #endregion
    }
}
