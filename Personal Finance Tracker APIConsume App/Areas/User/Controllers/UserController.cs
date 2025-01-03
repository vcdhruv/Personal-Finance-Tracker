using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal_Finance_Tracker_APIConsume_App.Areas.User.Models;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:54297/api");
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(login.Username), "UserName");
                formData.Add(new StringContent(login.Password), "Password");
                HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/User/Login", formData);
                if (response.IsSuccessStatusCode) 
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var tokenResponse = JsonConvert.DeserializeObject<LoginModel>(responseBody);
                    TempData["Token"] = tokenResponse.Token;
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
    }
}
