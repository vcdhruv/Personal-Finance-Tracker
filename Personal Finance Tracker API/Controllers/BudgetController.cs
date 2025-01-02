using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Tracker_API.BAL;
using Personal_Finance_Tracker_API.Models;


namespace Personal_Finance_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        #region Get All Budgets Of Specific User
        [Authorize]
        [HttpGet("{UserID}")]
        public IActionResult GetAllBudgets(int UserID)
        {
            Budget_BALBase budget = new Budget_BALBase();
            List<BudgetModel> budgets = budget.GetAllBudgets(UserID);
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if (budgets.Count > 0 && budgets != null)
            {
                response.Add("Status", true);
                response.Add("Message", "All Budgets Are Fetched Successfullyy...");
                response.Add("Budgets", budgets);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                response.Add("Budgets", null);
                return Ok(response);
            }
        }
        #endregion

        #region Add New Budget Of Specific User
        [Authorize]
        [HttpPost]
        public IActionResult AddNewBudget([FromForm] BudgetModel budget)
        {
            Budget_BALBase budget_BAL = new Budget_BALBase();
            bool isSuccess = budget_BAL.AddNewBudget(budget);
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if (isSuccess)
            {
                response.Add("Status", true);
                response.Add("Message", "New Budget Added Successfully..");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                return Ok(response);
            }
        }
        #endregion

        #region Update Budget Of Specific User
        [Authorize]
        [HttpPut]
        public IActionResult UpdateBudget([FromForm] BudgetModel budget, int UserID, int BudgetID)
        {
            Budget_BALBase budget_BALBase = new Budget_BALBase();
            bool isSuccess = budget_BALBase.UpdateBudget(budget, UserID, BudgetID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSuccess)
            {
                response.Add("Status", true);
                response.Add("Message", "Budget Updated Successfully..");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                return Ok(response);
            }
        }
        #endregion

        #region Delete Budget Of Specific User
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteBudget(int UserID, int BudgetID)
        {
            Budget_BALBase budget_ = new Budget_BALBase();
            bool isSuccess = budget_.DeleteBudget(UserID, BudgetID);
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if (isSuccess)
            {
                response.Add("Status", true);
                response.Add("Message", "Budget Deleted Successfully..");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                return Ok(response);
            }
        }
        #endregion
    }
}
