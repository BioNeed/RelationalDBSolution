using System.Data.SQLite;
using DataAccessLibrary.CommonDataAccess;

namespace DataAccessLibrary.SqlCrudInitializers
{
    public class SqliteCrudInitializer : ISqlCrudInitializer
    {
        public ISqlCrud InitializeSqlCrud(string connectionString)
        {
            var dataAccess = new CommonDataAccess<SQLiteConnection>();

            return new CommonSqlCrud<SQLiteConnection>(dataAccess, connectionString);
        }
    }
}
