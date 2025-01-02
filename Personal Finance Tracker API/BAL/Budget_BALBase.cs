using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class Budget_BALBase
    {
        #region Get All Budgets Of Specific User
        public List<BudgetModel> GetAllBudgets(int UserID)
        {
            Budget_DALBase budget_DALBase = new Budget_DALBase();
            return budget_DALBase.GetAllBudgets(UserID);
        }
        #endregion

        #region Add New Budget Of Specific User
        public bool AddNewBudget(BudgetModel budget)
        {
            Budget_DALBase budget_ = new Budget_DALBase();
            return budget_.AddNewBudget(budget);
        }
        #endregion
    }
}
