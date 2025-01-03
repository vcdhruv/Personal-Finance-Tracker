namespace Personal_Finance_Tracker_API.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
