﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal_Finance_Tracker_APIConsume_App.Areas.User.Models;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        #region Configuration For Uniform Resource Indicator
        Uri baseAddress = new Uri("http://localhost:54297/api");
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }

        #region Login Screen
        [Route("/")]
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login POST Method
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginModel login)
        {
            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(login.UserName), "UserName");
                formData.Add(new StringContent(login.Password), "Password");
                HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/User/Login", formData);
                if (response.IsSuccessStatusCode) 
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var tokenResponse = JsonConvert.DeserializeObject<LoginModel>(responseBody);
                    HttpContext.Session.SetString("Token", tokenResponse.Token);
                    HttpContext.Session.SetString("UserName", tokenResponse.UserName);
                    HttpContext.Session.SetString("Email", tokenResponse.Email);
                    HttpContext.Session.SetString("UserID", tokenResponse.UserID.ToString());
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }
        #endregion

        #region Logout 
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion
    }
}
