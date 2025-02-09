﻿using Microsoft.AspNetCore.Authorization;
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
        #region Get All Transactions Of Specific User
        [Authorize]
        [HttpGet("{UserID}")]
        public IActionResult GetAllTransactions(int UserID, string? Type = null, string? Category = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            Transaction_BALBase transaction = new Transaction_BALBase();
            List<TransactionModel> transactions =  transaction.GetAllTransactions(UserID, Type, Category, StartDate, EndDate);
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
        #endregion

        #region Get Transaction Of Specific User By ID
        [Authorize]
        [HttpGet("{UserID}/transaction/{TransactionID}")]
        public IActionResult GetTransactionByID(int TransactionID,int UserID)
        {
            Transaction_BALBase transaction_ = new Transaction_BALBase();
            TransactionModel transaction = transaction_.GetTransactionByID(TransactionID, UserID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (transaction != null && transaction.TransactionID != null && transaction.UserID != null && transaction.Amount != null && transaction.Category != null && transaction.Date != null && transaction.Type != null && transaction.Description != null)
            {
                response.Add("Status", true);
                response.Add("Message", "Transaction Fetched Successfully...");
                response.Add("Transaction", transaction);
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                response.Add("Transaction", null);
                return Ok(response);
            }
        }
        #endregion

        #region Add New Transaction Of Specific User
        [Authorize]
        [HttpPost]
        public IActionResult AddNewTransaction([FromForm] TransactionModel transaction)
        {
            Transaction_BALBase newTransaction = new Transaction_BALBase();
            bool isSuccess = newTransaction.AddNewTransaction(transaction);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSuccess) 
            {
                response.Add("Status", true);
                response.Add("Message", "New Transaction Added Successfully...");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                return Ok(response);
            }
        }
        #endregion

        #region Update Transaction Of Specific User
        [Authorize]
        [HttpPut]
        public IActionResult UpdateTransaction([FromForm]TransactionModel transaction)
        {
            Transaction_BALBase transaction_ = new Transaction_BALBase();
            bool isSuccess = transaction_.UpdateTransaction(transaction);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSuccess)
            {
                response.Add("Status", true);
                response.Add("Message", "Transaction Updated Successfully...");
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured...");
                return Ok(response);
            }
        }
        #endregion

        #region Delete Transaction Of Specific User
        [Authorize]
        [HttpDelete("{TransactionID}/transaction/{UserID}")]
        public IActionResult DeleteTransaction(int UserID, int TransactionID)
        {
            Transaction_BALBase transaction_ = new Transaction_BALBase();
            bool isSuccess = transaction_.DeleteTransaction(UserID, TransactionID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isSuccess)
            {
                response.Add("Status", true);
                response.Add("Message", "Transaction Deleted Successfully..");
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
