namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
