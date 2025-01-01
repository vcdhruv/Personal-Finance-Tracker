﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult SignUp([FromForm] UserModel user)
        {
            User_BALBase user_BALBase = new User_BALBase();
            bool newUser = user_BALBase.SignUp(user);
            Dictionary<string,dynamic> response = new Dictionary<String,dynamic>();
            if (newUser)
            {
                response.Add("Status", true);
                response.Add("Message", "New User Added Successfully..");
                response.Add("token", TokenGeneration.GenerateToken(user,_configuration));
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
    }
}