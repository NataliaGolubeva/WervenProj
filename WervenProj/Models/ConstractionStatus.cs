using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WervenProj.Models
{
    public class ConstractionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StatusNr { get; set; }
        [MaxLength(50)]
        public string StatusName { get; set; } = string.Empty;
        public virtual IList<ConstractionSite> ConstractionSites { get; set; }
    }
}
