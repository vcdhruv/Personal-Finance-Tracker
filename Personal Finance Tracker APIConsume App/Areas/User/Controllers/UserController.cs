using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using Personal_Finance_Tracker_APIConsume_App.Areas.User.Models;
using Personal_Finance_Tracker_APIConsume_App.BAL;

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

        #region Method To Generate Random Code For Email Verification
        public string GenerateRandomCodeForEmail() 
        {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string random = "";
            Random rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                int generateRandom = rnd.Next(0, s.Length);
                random += s[generateRandom];
            }
            return random;
        }
        #endregion

        #region Send Email POST Method
        public async Task<IActionResult> SendEmail(EmailModel email)
        {
            // Generate the random verification code
            string verificationCode = GenerateRandomCodeForEmail();

            // Prepare the email body with HTML content
            string emailBody = $@"
            <html>
            <body>
                <h1>Email Verification</h1>
                <p>Dear User,</p>
                <p>Please use the following code to verify your email address:</p>
                <h2 style='color: blue;'>{verificationCode}</h2>
                <p>Thank you!</p>
            </body>
            </html>";

            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("badwhisky777@gmail.com"),"Receptor" );
            content.Add(new StringContent("This Is For Email Verification"), "Subject");
            //content.Add(new StringContent(emailBody), "Body");

            // Add HTML body with explicit Content-Type
            var htmlBodyContent = new StringContent(emailBody);
            htmlBodyContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            content.Add(htmlBodyContent, "Body");

            HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/User/Email",content);
            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            else
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
