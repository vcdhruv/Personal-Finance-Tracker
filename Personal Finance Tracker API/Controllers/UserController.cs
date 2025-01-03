using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Finance_Tracker_API.BAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            bool isUserAlreadyPresent = User_bal.Login(login.Username, login.Password);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (isUserAlreadyPresent)
            {
                response.Add("Status", true);
                response.Add("Message", "User Is Logged In Successfully..");
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
    }
}
