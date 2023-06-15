using DataAccessLibrary.Constants;
using Microsoft.Extensions.Configuration;

namespace IndependentUI
{
    public static class DbConnector
    {
        public static string GetConnectionStringName(int chosenSqlType)
        {
            return chosenSqlType switch
            {
                1 => ConnectionStringsNames.SqlServerConnectionStringName,
                2 => ConnectionStringsNames.SqliteConnectionStringName,
                3 => ConnectionStringsNames.MySqlConnectionStringName,
                _ => ConnectionStringsNames.MySqlConnectionStringName,
            };
        }

        public static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = string.Empty;

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
