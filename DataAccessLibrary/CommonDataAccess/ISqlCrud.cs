using DataAccessLibrary.Models;

namespace DataAccessLibrary.CommonDataAccess
{
    public interface ISqlCrud
    {
        public List<BasicAddressModel> GetAllAddresses();

        public List<EmployerModel> GetAllEmployers();
    }
}
