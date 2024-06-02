using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Data;

namespace WervenProj.Models
{
    public class ConstractionSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ConstractionStatus Status { get; set; }
        public int StatusId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public virtual IList<ConstractionSiteEnrollments> Enrollments { get; set; }

    }
}
