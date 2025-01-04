using Microsoft.AspNetCore.Html;

namespace Personal_Finance_Tracker_APIConsume_App.Areas.User.Models
{
    public class EmailModel
    {
        public string Receptor { get; set; }
        public string Message { get; set; }
        public HtmlContentBuilder Body { get; set; }
    }
}
