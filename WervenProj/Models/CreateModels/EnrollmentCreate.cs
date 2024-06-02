namespace WervenProj.Models.CreateModels
{
    public class EnrollmentCreate
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ConstractionSiteId { get; set; }

        public static bool Validate(EnrollmentCreate obj) {
            return obj == null ? false : true;
        }

    }
}
