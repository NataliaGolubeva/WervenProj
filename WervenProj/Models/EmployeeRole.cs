using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WervenProj.Models
{
    public class EmployeeRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoleNr { get; set; }
        [MaxLength(50)]
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public virtual IList<Employee> Employees { get; set; }
    }
}
