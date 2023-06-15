using System.Data.SqlClient;
using DataAccessLibrary.CommonDataAccess;

namespace DataAccessLibrary.SqlCrudInitializers
{
    public class SqlServerCrudInitializer : ISqlCrudInitializer
    {
        public ISqlCrud InitializeSqlCrud(string connectionString)
        {
            CommonDataAccess<SqlConnection> dataAccess = new CommonDataAccess<SqlConnection>();

            return new CommonSqlCrud<SqlConnection>(dataAccess, connectionString);
        }
    }
}
