using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SQLServerUI
{
    internal class Program
    {
        private static void Main()
        {
            string connectionString = GetConnectionString();

            SqliteCrud sql = new SqliteCrud(connectionString);

            DeleteEmployerFromAddress(sql);

            Console.WriteLine("Done processing Sqlite");
            Console.ReadLine();
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = string.Empty;

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }

        private static void ReadAllAddresses(SqliteCrud sql)
        {
            List<BasicAddressModel> addresses = sql.GetAllAddresses();

            return;
        }

        private static void ReadAllEmployers(SqliteCrud sql)
        {
            List<EmployerModel> employers = sql.GetAllEmployers();

            return;
        }

        private static void ReadFullAddressById(SqliteCrud sql, int id)
        {
            FullAddressModel fullAddress = sql.GetFullAddressById(id);
            if (fullAddress == null)
            {
                Console.WriteLine($"No address found associated with id = {id}.");
                return;
            }

            Console.WriteLine($"Full address: \nId: {fullAddress.BasicInfo.Id}, " +
                $"Country: {fullAddress.BasicInfo.Country}, City: {fullAddress.BasicInfo.City}, " +
                $"Street: {fullAddress.BasicInfo.Street}");

            foreach (PersonModel person in fullAddress.People)
            {
                Console.WriteLine($"Person Id: {person.Id}, Person First Name: {person.FirstName}, " +
                    $"Person Last Name: {person.LastName}");
            }

            Console.WriteLine();

            foreach (EmployerModel employer in fullAddress.Employers)
            {
                Console.WriteLine($"Employer Id: {employer.Id}, Employer Company Name: {employer.CompanyName}, " +
                    $"Employer Hiring Status: {employer.IsHiring}");
            }
        }

        private static void CreateFullAddress(SqliteCrud sql)
        {
            FullAddressModel newAddress = new FullAddressModel
            {
                BasicInfo = new BasicAddressModel
                {
                    Country = "Russia",
                    City = "Moscow",
                    Street = "Lenina",
                },
            };

            newAddress.People.Add(new PersonModel { FirstName = "Ivan", LastName = "ivanov" });
            newAddress.People.Add(new PersonModel { Id = 3, FirstName = "Michael", LastName = "Skuratov" });

            newAddress.Employers.Add(new EmployerModel { CompanyName = "AMD.BY", IsHiring = true });
            newAddress.Employers.Add(new EmployerModel { Id = 3, CompanyName = "Spartak", IsHiring = true });

            sql.CreateFullAddress(newAddress);
        }

        private static void UpdateAddress(SqliteCrud sql)
        {
            BasicAddressModel address = new BasicAddressModel
            {
                Id = 3,
                Country = "Spain",
                City = "Barselona",
                Street = "Ronaldo's",
            };

            sql.UpdateAddress(address);
        }

        private static void DeleteEmployerFromAddress(SqliteCrud sql)
        {
            sql.RemoveEmployerFromAddress(5, 4);
        }
    }
}