namespace DataAccessLibrary.Models
{
    public class FullAddressModel
    {
        public BasicAddressModel BasicInfo { get; set; }

        public List<PersonModel> People { get; set; } = new List<PersonModel>();

        public List<EmployerModel> Employers { get; set; } = new List<EmployerModel>();
    }
}
