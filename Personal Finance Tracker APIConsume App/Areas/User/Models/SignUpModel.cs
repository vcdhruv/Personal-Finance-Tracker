using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class SignUpModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        [DisplayName("User Name")]
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
