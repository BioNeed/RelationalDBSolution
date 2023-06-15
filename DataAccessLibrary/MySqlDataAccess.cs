using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

namespace DataAccessLibrary
{
    internal class MySqlDataAccess
    {
        internal List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                List<T> resultRows = connection.Query<T>(sqlStatement, parameters).ToList();
                return resultRows;
            }
        }

        internal void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
