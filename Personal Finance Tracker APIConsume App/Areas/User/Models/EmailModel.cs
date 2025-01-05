using Microsoft.AspNetCore.Html;
using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class EmailModel
    {
        [Required]
        public string Email { get; set; }
        public string Receptor { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
