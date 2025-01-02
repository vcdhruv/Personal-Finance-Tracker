using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Personal_Finance_Tracker_API.Models;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_API.DAL
{
    public class Budget_DALBase : DAL_Helpers
    {
        #region Get All Budgets Of Specific User
        public List<BudgetModel> GetAllBudgets(int UserID)
        {
            List<BudgetModel> budgets = new List<BudgetModel>();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Budgets_GetAll");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                IDataReader rd = db.ExecuteReader(cmd);
                if (rd != null)
                {
                    while (rd.Read())
                    {
                        BudgetModel budget = new BudgetModel();
                        budget.UserID = (int)rd["UserID"];
                        budget.BudgetID = (int)rd["BudgetID"];
                        budget.Category = rd["Category"].ToString();
                        budget.Amount = (decimal)rd["Amount"];
                        budget.Month = rd["Month"].ToString();
                        budgets.Add(budget);
                    }
                }
                return budgets;
            }
            catch
            {
                return budgets;
            }
        }
        #endregion

        #region Add New Budget Of Specific User
        public bool AddNewBudget(BudgetModel budget)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Budget_POST");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, budget.UserID);
                db.AddInParameter(cmd, "@Category", DbType.String, budget.Category);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, budget.Amount);
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update Budget Of Specific User
        public bool UpdateBudget(BudgetModel budget, int UserID, int BudgetID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Budget_PUT");
                db.AddInParameter(cmd, "@BudgetID", DbType.Int64, BudgetID);
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                db.AddInParameter(cmd, "@Category", DbType.String, budget.Category);
                db.AddInParameter(cmd, "@Amount", DbType.Decimal, budget.Amount);
                db.AddInParameter(cmd, "@Month", DbType.DateTime, DateTime.Parse(budget.Month));
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region Delete Budget Of Specific User
        public bool DeleteBudget(int UserID, int BudgetID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Budget_DELETE");
                db.AddInParameter(cmd, "@UserID", DbType.Int64, UserID);
                db.AddInParameter(cmd, "@BudgetID", DbType.Int64, BudgetID);
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

