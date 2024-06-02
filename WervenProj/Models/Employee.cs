using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WervenProj.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; } 
        public int RoleId { get; set; }
        public virtual IList<ConstractionSiteEnrollments> Enrollments { get; set; }


    }
}
