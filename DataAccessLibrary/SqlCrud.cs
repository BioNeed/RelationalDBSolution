using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly SqlDataAccess db = new SqlDataAccess();
        private readonly string connectionString;

        public SqlCrud(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateFullAddress(FullAddressModel newAddress)
        {
            string statement = @"insert into dbo.Addresses (Country, City, Street) 
                                                    values (@Country, @City, @Street);";

            db.SaveData(statement, newAddress.BasicInfo, connectionString);

            statement = @"select Id from dbo.Addresses 
                        where Country = @Country and City = @City and Street = @Street";

            int newAddressId = db.LoadData<int, BasicAddressModel>(
                statement, newAddress.BasicInfo, connectionString).First();

            foreach (PersonModel person in newAddress.People)
            {
                if (person.Id == 0)
                {
                    CreatePerson(person);
                    person.Id = FindPersonId(person);
                }

                CreateAddressAndPersonLink(newAddressId, person.Id);
            }

            foreach (EmployerModel employer in newAddress.Employers)
            {
                if (employer.Id == 0)
                {
                    CreateEmployer(employer);
                    employer.Id = FindEmployerId(employer);
                }

                CreateAddressAndEmployerLink(newAddressId, employer.Id);
            }
        }

        public void CreatePerson(PersonModel person)
        {
            string statement = @"insert into dbo.People (FirstName, LastName)
                                                values (@FirstName, @LastName);";

            db.SaveData(statement, person, connectionString);
        }

        public void CreateEmployer(EmployerModel employerModel)
        {
            string statement = @"insert into dbo.Employers (CompanyName, IsHiring)
                                                values (@CompanyName, @IsHiring);";

            db.SaveData(statement, employerModel, connectionString);
        }

        public List<BasicAddressModel> GetAllAddresses()
        {
            string statement = "select * from dbo.Addresses";

            return db.LoadData<BasicAddressModel, dynamic>(statement, new { }, connectionString);
        }

        public List<EmployerModel> GetAllEmployers()
        {
            string statement = "select * from dbo.Employers";

            return db.LoadData<EmployerModel, dynamic>(statement, new { }, connectionString);
        }

        public BasicAddressModel GetBasicAddressById(int addressId)
        {
            string statement = "select Id, Country, City, Street from dbo.Addresses where Id = @Id";

            BasicAddressModel basicAddress = db.LoadData<BasicAddressModel, dynamic>(
                statement, new { Id = addressId }, connectionString).FirstOrDefault();

            return basicAddress;
        }

        public FullAddressModel? GetFullAddressById(int addressId)
        {
            FullAddressModel fullAddress = new FullAddressModel();

            fullAddress.BasicInfo = GetBasicAddressById(addressId);

            if (fullAddress.BasicInfo == null)
            {
                // do something to tell the user that the record was not found
                return null;
            }

            string statement = @"select p.*
                                from dbo.People p
                                inner join dbo.AddressPerson ap on p.Id = ap.PersonId
                                where ap.AddressId = @Id";

            fullAddress.People = db.LoadData<PersonModel, dynamic>(
                statement, new { Id = addressId }, connectionString);

            statement = @"select e.*
                        from dbo.Employers e
                        inner join dbo.AddressEmployer ae on e.Id = ae.EmployerId
                        where ae.AddressId = @Id";

            fullAddress.Employers = db.LoadData<EmployerModel, dynamic>(
                statement, new { Id = addressId }, connectionString);

            return fullAddress;
        }

        public void UpdateAddress(BasicAddressModel address)
        {
            string statement = "update dbo.Addresses set Country = @Country, City = @City, Street = @Street where Id = @Id";

            db.SaveData(statement, address, connectionString);
        }

        public void RemoveEmployerFromAddress(int addressId, int employerId)
        {
            string statement = "select * from dbo.AddressEmployer where EmployerId = @EmployerId";
            int links = db.LoadData<AddressEmployer, dynamic>(statement, new { EmployerId = employerId }, connectionString).Count;

            statement = "delete from dbo.AddressEmployer where AddressId = @AddressId and EmployerId = @EmployerId";
            db.SaveData(statement, new { AddressId = addressId, EmployerId = employerId }, connectionString);

            if (links == 1)
            {
                statement = "delete from dbo.Employers where Id = @EmployerId";
                db.SaveData(statement, new { EmployerId = employerId }, connectionString);
            }
        }

        private int FindPersonId(PersonModel person)
        {
            string statement = @"select Id from dbo.People 
                                where FirstName = @FirstName and LastName = @LastName";

            return db.LoadData<int, PersonModel>(statement, person, connectionString).First();
        }

        private int FindEmployerId(EmployerModel employer)
        {
            string statement = @"select Id from dbo.Employers 
                                where CompanyName = @CompanyName and IsHiring = @IsHiring";

            return db.LoadData<int, EmployerModel>(statement, employer, connectionString).First();
        }

        private void CreateAddressAndPersonLink(int addressId, int personId)
        {
            string statement = @"insert into dbo.AddressPerson (AddressId, PersonId) 
                                                        values (@AddressId, @PersonId);";

            db.SaveData(statement, new { AddressId = addressId, PersonId = personId }, connectionString);
        }

        private void CreateAddressAndEmployerLink(int addressId, int employerId)
        {
            string statement = @"insert into dbo.AddressEmployer (AddressId, EmployerId) 
                                                        values (@AddressId, @EmployerId);";

            db.SaveData(statement, new { AddressId = addressId, EmployerId = employerId }, connectionString);
        }
    }
}