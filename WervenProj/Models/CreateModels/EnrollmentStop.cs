namespace WervenProj.Models.CreateModels
{
    public class EnrollmentStop
    {
        public int EmployeeId { get; set; }
        public int ConstractionSiteEnrollmentId {  get; set; }

        public static bool Validate ( EnrollmentStop obj )
        {
            return obj == null ? false : true;
        }
    }
}
