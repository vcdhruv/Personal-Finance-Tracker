using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Tracker_API.BAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        #region Get Spending Summary
        [Authorize]
        [HttpGet("SpendingSummary/{UserID}")]
        public IActionResult SpendingSummary(int UserID)
        {
            Report_BALBase report = new Report_BALBase();
            List<ReportModel> spendings = report.SpendingSummary(UserID);
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if (spendings != null && spendings.Count > 0) 
            {
                response.Add("Status", true);
                response.Add("Message", "Spending Summary fetched successfully...");
                response.Add("SpendingSummary", spendings);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                response.Add("SpendingSummary", null);
                return Ok(response);
            }
        }
        #endregion

        #region Get Savings Progress
        [Authorize]
        [HttpGet("SavingsProgress/{UserID}")]
        public IActionResult SavingsProgress(int UserID)
        {
            Report_BALBase report = new Report_BALBase();
            List<ReportModel> savings = report.SavingsProgress(UserID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (savings != null && savings.Count > 0)
            {
                response.Add("Status", true);
                response.Add("Message", "Savings Progress fetched successfully...");
                response.Add("Savings", savings);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                response.Add("Savings", null);
                return Ok(response);
            }
        }
        #endregion
    }
}
