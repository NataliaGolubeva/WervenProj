namespace WervenProj.Models.CreateModels
{
    public class EnrollmentData
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ConstractionSiteId { get; set; }

        public static bool Validate(EnrollmentData obj) {
            return obj == null ? false : true;
        }

    }
}
