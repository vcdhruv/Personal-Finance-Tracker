﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Address Is Required"), EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required"), PasswordPropertyText]
        [DisplayName("Password")]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
