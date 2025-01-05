namespace Personal_Finance_Tracker_APIConsume_App.DAL
{
    public class DAL_Helpers
    {
        public static string ConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnStr");
    }
}
