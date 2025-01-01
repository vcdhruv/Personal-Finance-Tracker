namespace Personal_Finance_Tracker_API.DAL
{
    public class DAL_Helpers
    {
        public static string connStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnStr")!;
    }
}
