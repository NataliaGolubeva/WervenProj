namespace WervenProj.Models.DTO
{
    public class EnrollmentsDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; } = string.Empty;
        public int ConstractionSiteId { get; set; }
        public string? ConstractionSiteName { get; set; } = string.Empty;
        public string IsActive { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
