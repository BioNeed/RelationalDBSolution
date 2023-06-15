using DataAccessLibrary.CommonDataAccess;

namespace DataAccessLibrary.SqlCrudInitializers
{
    public interface ISqlCrudInitializer
    {
        public ISqlCrud InitializeSqlCrud(string connectionString);
    }
}
