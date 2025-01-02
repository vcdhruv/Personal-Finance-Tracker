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
        [HttpGet("{UserID}")]
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
    }
}
