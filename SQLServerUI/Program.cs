using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SQLServerUI
{
    internal class Program
    {
        private static void Main()
        {
            // Console.WriteLine(GetConnectionString());
            string connectionString = GetConnectionString();

            SqlCrud sql = new SqlCrud(connectionString);

            DeleteEmployerFromAddress(sql);

            Console.WriteLine("Done processing SqlServer");
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

        private static void ReadAllAddresses(SqlCrud sql)
        {
            List<BasicAddressModel> addresses = sql.GetAllAddresses();

            return;
        }

        private static void ReadAllEmployers(SqlCrud sql)
        {
            List<EmployerModel> employers = sql.GetAllEmployers();

            return;
        }

        private static void ReadFullAddressById(SqlCrud sql, int id)
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

        private static void CreateFullAddress(SqlCrud sql)
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
            newAddress.People.Add(new PersonModel { Id = 4, FirstName = "Michael", LastName = "Skuratov" });

            newAddress.Employers.Add(new EmployerModel { CompanyName = "AMD.BY", IsHiring = true });
            newAddress.Employers.Add(new EmployerModel { Id = 3, CompanyName = "Spartak", IsHiring = false });

            sql.CreateFullAddress(newAddress);
        }

        private static void UpdateAddress(SqlCrud sql)
        {
            BasicAddressModel address = new BasicAddressModel
            {
                Id = 5,
                Country = "Spain",
                City = "Barselona",
                Street = "Ronaldo's",
            };

            sql.UpdateAddress(address);
        }

        private static void DeleteEmployerFromAddress(SqlCrud sql)
        {
            sql.RemoveEmployerFromAddress(9, 3);
        }
    }
}