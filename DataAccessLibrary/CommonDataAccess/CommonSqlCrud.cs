using System.Data;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.CommonDataAccess
{
    public class CommonSqlCrud<TConnection> : ISqlCrud
        where TConnection : IDbConnection
    {
        private readonly CommonDataAccess<TConnection> db;
        private readonly string connectionString;

        internal CommonSqlCrud(CommonDataAccess<TConnection> dataAccess, string connectionString)
        {
            db = dataAccess;
            this.connectionString = connectionString;
        }

        public List<BasicAddressModel> GetAllAddresses()
        {
            string statement = "select * from Addresses";

            return db.LoadData<BasicAddressModel, dynamic>(statement, new { }, connectionString);
        }

        public List<EmployerModel> GetAllEmployers()
        {
            string statement = "select * from Employers";

            return db.LoadData<EmployerModel, dynamic>(statement, new { }, connectionString);
        }
    }
}
