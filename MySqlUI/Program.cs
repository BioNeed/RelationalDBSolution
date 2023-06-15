using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace MySqlUI
{
    internal class Program
    {
        static void Main()
        {
            string connectionString = GetConnectionString();

            MySqlCrud sql = new MySqlCrud(connectionString);

            ReadAllAddresses(sql);

            Console.WriteLine("Done processing MySql");
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

        private static void ReadAllAddresses(MySqlCrud sql)
        {
            List<BasicAddressModel> addresses = sql.GetAllAddresses();

            return;
        }

        private static void ReadAllEmployers(MySqlCrud sql)
        {
            List<EmployerModel> employers = sql.GetAllEmployers();

            return;
        }

        private static void ReadFullAddressById(MySqlCrud sql, int id)
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

        private static void CreateFullAddress(MySqlCrud sql)
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

        private static void UpdateAddress(MySqlCrud sql)
        {
            BasicAddressModel address = new BasicAddressModel
            {
                Id = 4,
                Country = "Spain",
                City = "Barselona",
                Street = "Ronaldo's",
            };

            sql.UpdateAddress(address);
        }

        private static void DeleteEmployerFromAddress(MySqlCrud sql)
        {
            sql.RemoveEmployerFromAddress(7, 6);
        }
    }
}