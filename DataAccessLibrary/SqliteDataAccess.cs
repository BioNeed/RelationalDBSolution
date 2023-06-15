using System.Data;
using System.Data.SQLite;
using Dapper;

namespace DataAccessLibrary
{
    internal class SqliteDataAccess
    {
        internal List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> resultRows = connection.Query<T>(sqlStatement, parameters).ToList();
                return resultRows;
            }
        }

        internal void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
