using DataAccessLibrary.CommonDataAccess;
using DataAccessLibrary.SqlCrudInitializers;
using DataAccessLibrary.Models;

namespace IndependentUI
{
    internal class Program
    {
        private static void Main()
        {
            int chosenSqlType = InputSqlType();

            string connectionStringName = DbConnector.GetConnectionStringName(chosenSqlType);

            string connectionString = DbConnector.GetConnectionString(connectionStringName);

            ISqlCrudInitializer sqlCrudInitializer = chosenSqlType switch
            {
                1 => new SqlServerCrudInitializer(),
                2 => new SqliteCrudInitializer(),
                3 => new MySqlCrudInitializer(),
                _ => new MySqlCrudInitializer(),
            };

            ISqlCrud sql = sqlCrudInitializer.InitializeSqlCrud(connectionString);

            ReadAllAddresses(sql);

            Console.WriteLine("Done processing Independent");
            Console.ReadLine();
        }

        private static int InputSqlType()
        {
            bool validEnteredType;
            int chosenSqlType;

            do
            {
                Console.WriteLine("Which type of SQL would you choose?");
                Console.WriteLine("1 - SQl Server\n2 - SQLite\n3 - MySQL");

                validEnteredType = int.TryParse(Console.ReadLine(), out chosenSqlType);
            }
            while (validEnteredType == false || chosenSqlType < 1 || chosenSqlType > 3);

            return chosenSqlType;
        }

        private static void ReadAllAddresses(ISqlCrud sql)
        {
            List<BasicAddressModel> addresses = sql.GetAllAddresses();

            return;
        }

        private static void ReadAllEmployers(ISqlCrud sql)
        {
            List<EmployerModel> employers = sql.GetAllEmployers();

            return;
        }


    }
}