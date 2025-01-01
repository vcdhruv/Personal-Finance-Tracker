using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Tracker_API.BAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [Authorize]
        [HttpGet("{UserID}")]
        public IActionResult GetAllTransactions(int UserID)
        {
            Transaction_BALBase transaction = new Transaction_BALBase();
            List<TransactionModel> transactions =  transaction.GetAllTransactions(UserID);
            Dictionary<string,dynamic> response = new Dictionary<string,dynamic>();
            if(transactions != null && transactions.Count > 0)
            {
                response.Add("Status", true);
                response.Add("Message", "All Transactions Fetched Successfully...");
                response.Add("Transactions", transactions);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                response.Add("Transactions", null);
                return Ok(response);
            }
        }
    }
}
