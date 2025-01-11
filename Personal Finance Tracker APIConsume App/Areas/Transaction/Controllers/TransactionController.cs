using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using NuGet.Common;
using Personal_Finance_Tracker_APIConsume_App.Areas.Transaction.Models;
using Personal_Finance_Tracker_APIConsume_App.Areas.Transaction.ViewModels;
using Personal_Finance_Tracker_APIConsume_App.BAL;
using System.Net.Http.Headers;
using System.Transactions;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.Transaction.Controllers
{
    [Area("Transaction")]
    public class TransactionController : Controller
    {
        #region Configuration Settings
        Uri baseAddress = new Uri("http://localhost:54297/api");
        private readonly HttpClient _client;
        public TransactionController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        #endregion

        #region View Transaction
        public IActionResult Index(string? Type = null,string? Category = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {

            #region For Adding Authorization As Header To Method
            string token = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            // Base API endpoint
            string baseUrl = $"{_client.BaseAddress}/Transaction/{CV.UserID()}";

            // Query parameters
            List<string> queryParams = new List<string>();

            if (!string.IsNullOrEmpty(Type))
                queryParams.Add($"Type={Type}");

            if (!string.IsNullOrEmpty(Category))
                queryParams.Add($"Category={Category}");

            if (StartDate != null)
                queryParams.Add($"StartDate={StartDate.Value:yyyy-MM-dd}"); // Format the date as needed

            if (EndDate != null)
                queryParams.Add($"EndDate={EndDate.Value:yyyy-MM-dd}");

            // Combine query parameters
            string queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";

            // Final URL
            string url = baseUrl + queryString;

            // Make the GET request
            HttpResponseMessage response = _client.GetAsync(url).Result;


            //HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Transaction/{CV.UserID()}/{Type}/{Category}/{StartDate}/{EndDate}").Result;
            List<TransactionModel> transactions = new List<TransactionModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic jsonObject = JsonConvert.DeserializeObject(data);

                var transactionDataOfObject = jsonObject.Transactions;
                var extractedDataJson = JsonConvert.SerializeObject(transactionDataOfObject,Formatting.Indented);
                transactions = JsonConvert.DeserializeObject<List<TransactionModel>>(extractedDataJson);
                // Check if the request is an AJAX request
                if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(transactions); // Return JSON for AJAX requests
                }
                return View(transactions);
            }
            return Json(new List<TransactionModel>());
        }
        #endregion

        #region Add New Transaction

        #region Form Screen
        [HttpGet]
        public IActionResult AddNewTransaction()
        {
            return View();
        }
        #endregion

        #region Add New Transaction Method
        [HttpPost]
        public async Task<IActionResult> AddNewTransaction(TransactionViewModel new_Transaction)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();

            content.Add(new StringContent(@CV.UserID().ToString()), "UserID");
            content.Add(new StringContent(new_Transaction.Type.ToString()), "Type");
            content.Add(new StringContent(new_Transaction.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Amount");
            content.Add(new StringContent(new_Transaction.Description), "Description");
            content.Add(new StringContent(new_Transaction.Category.ToString()), "Category");
            content.Add(new StringContent(new_Transaction.Date.ToString()), "Date");

            #region For Adding Authorization As Header To Method
            string token = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/Transaction",content);
            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #endregion

        #region Edit Transaction

        #region Filling Model Of Get By ID
        [HttpGet]
        public IActionResult UpdateTransaction(int edit_id) 
        {

            #region For Adding Authorization As Header To Method
            string token = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Transaction/{@CV.UserID()}/transaction/{edit_id}").Result;
            TransactionViewModel transaction = new TransactionViewModel();
            if (response.IsSuccessStatusCode) 
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic jsonObject = JsonConvert.DeserializeObject(data);

                var transactionDataOfObject = jsonObject.Transaction;
                var extractedDataJson = JsonConvert.SerializeObject(transactionDataOfObject, Formatting.Indented);
                transaction = JsonConvert.DeserializeObject<TransactionViewModel>(extractedDataJson);
            }
            return View(transaction);
        }
        #endregion

        #region Update Transaction Method
        [HttpPost]
        public async Task<IActionResult> UpdateTransaction(TransactionViewModel update_transaction)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(update_transaction.TransactionID.ToString()), "TransactionID");
            content.Add(new StringContent(@CV.UserID().ToString()), "UserID");
            content.Add(new StringContent(update_transaction.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)),"Amount");
            content.Add(new StringContent(update_transaction.Category), "Category");
            content.Add(new StringContent(update_transaction.Type.ToString()), "Type");
            content.Add(new StringContent(update_transaction.Date.ToString()), "Date");
            content.Add(new StringContent(update_transaction.Description), "Description");

            #region For Adding Authorization As Header To Method
            string token = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/Transaction", content);
            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #endregion

        #region Delete Transaction
        [HttpGet]
        public async Task<IActionResult> DeleteTransaction(int delete_id)
        {

            #region For Adding Authorization As Header To Method
            string token = HttpContext.Session.GetString("Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}/Transaction/{delete_id}/transaction/{@CV.UserID()}");
            if (response.IsSuccessStatusCode) 
            {
                TempData["Success_Msg"] = "Transaction Deleted Successfully...";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error_Msg"] = "Please Try Again...";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}
