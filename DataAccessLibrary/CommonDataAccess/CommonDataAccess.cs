using System.Data;
using System.Reflection;
using Dapper;

namespace DataAccessLibrary.CommonDataAccess
{
    internal class CommonDataAccess<TConnection>
        where TConnection : IDbConnection
    {
        private readonly ConstructorInfo? connectionCtor;

        internal CommonDataAccess()
        {
            Type connectionType = typeof(TConnection);
            connectionCtor = connectionType?.GetConstructor(new[] { typeof(string) });
        }

        internal List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = (IDbConnection)connectionCtor?.Invoke(new[] { connectionString }))
            {
                List<T> resultRows = connection.Query<T>(sqlStatement, parameters).ToList();
                return resultRows;
            }
        }

        internal void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = (IDbConnection)connectionCtor?.Invoke(new[] { connectionString }))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
