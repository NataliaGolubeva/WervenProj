using System.ComponentModel.DataAnnotations;

namespace WervenProj.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = String.Empty;
    }
}
