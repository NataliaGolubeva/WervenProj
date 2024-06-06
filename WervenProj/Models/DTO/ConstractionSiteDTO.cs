using System.ComponentModel.DataAnnotations;

namespace WervenProj.Models.DTO
{
    public class ConstractionSiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StatusNr { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public IList<EmployeeDTO> Employees { get; set; }
    }
}
