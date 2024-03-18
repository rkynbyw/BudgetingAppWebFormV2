using Microsoft.Extensions.Configuration;

namespace BudgetingApp.DAL
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"] == null)
            {
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return MyConfig.GetConnectionString("BudgetingAppConnectionString");
            }
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["BudgetingAppConnectionString"].ConnectionString;
            return connString;
            //return @"Data Source=ACTUAL;Initial Catalog=LatihanDb;Integrated Security=True;TrustServerCertificate=True";
            //return ConfigurationManager.AppSettings["MyDbConnectionString"];
        }
    }
}
