using Microsoft.AspNetCore.Html;

namespace Personal_Finance_Tracker_API.Models
{
    public class EmailModel
    {
        public string Receptor { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
