using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Password And Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
