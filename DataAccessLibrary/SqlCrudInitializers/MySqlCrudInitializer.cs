using MySql.Data.MySqlClient;
using DataAccessLibrary.CommonDataAccess;

namespace DataAccessLibrary.SqlCrudInitializers
{
    public class MySqlCrudInitializer : ISqlCrudInitializer
    {
        public ISqlCrud InitializeSqlCrud(string connectionString)
        {
            CommonDataAccess<MySqlConnection> dataAccess = new CommonDataAccess<MySqlConnection>();

            return new CommonSqlCrud<MySqlConnection>(dataAccess, connectionString);
        }
    }
}
