using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Tracker_API.BAL;
using Personal_Finance_Tracker_API.Models;
using Personal_Finance_Tracker_API.Services;

namespace Personal_Finance_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IEmailService emailService;

        public UserController(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            this.emailService = emailService;
        }

        #region Sign Up
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public IActionResult SignUp([FromForm] UserModel user)
        {
            User_BALBase user_BALBase = new User_BALBase();
            bool newUser = user_BALBase.SignUp(user);
            Dictionary<string, dynamic> response = new Dictionary<String, dynamic>();
            if (newUser)
            {
                response.Add("Status", true);
                response.Add("Message", "New User Added Successfully..");
                response.Add("token", TokenGeneration.GenerateToken(_configuration));
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                response.Add("token", Unauthorized());
                return Ok(response);
            }
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginModel login)
        {
            User_BALBase User_bal = new User_BALBase();
            LoginModel isUserAlreadyPresent = User_bal.Login(login);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (login != null)
            {
                response.Add("Status", true);
                response.Add("Message", "User Is Logged In Successfully..");
                response.Add("UserName", isUserAlreadyPresent.UserName);
                response.Add("Email", isUserAlreadyPresent.Email);
                response.Add("UserID", isUserAlreadyPresent.UserID);
                response.Add("token", TokenGeneration.GenerateToken(_configuration));
                return Ok(response);
            }
            else
            {
                response.Add("Status", false);
                response.Add("Message", "Some Error Has Occured..");
                response.Add("UserName", "");
                response.Add("Email", "");
                response.Add("UserID", "");
                response.Add("token", Unauthorized());
                return Ok(response);
            }
        }
        #endregion

        #region Send Email
        [AllowAnonymous]
        [HttpPost("Email")]
        public async Task<IActionResult> SendEamil([FromForm] EmailModel email)
        {
            await emailService.SendEmail(email);
            return Ok();
        }
        #endregion
    }
}
