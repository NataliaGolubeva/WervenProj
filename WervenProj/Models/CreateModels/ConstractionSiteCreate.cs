using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace WervenProj.Models.CreateModels
{
    public class ConstractionSiteCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }

        public static bool Validate(ConstractionSiteCreate obj)
        {
            if (obj == null ||
                obj.Name.Length < 3
                || obj.Name.Length > 50
                || obj.Description.Length > 255
                || obj.StatusId < 1
                || obj.StatusId > 4) {
                return false;
            }
            return true;
        }

    }
}
