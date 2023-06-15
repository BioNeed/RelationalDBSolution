using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataAccessLibrary
{
    internal class SqlDataAccess
    {
        internal List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> resultRows = connection.Query<T>(sqlStatement, parameters).ToList();
                return resultRows;
            }
        }

        internal void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
