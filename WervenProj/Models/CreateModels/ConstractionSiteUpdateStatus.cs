namespace WervenProj.Models.CreateModels
{
    public class ConstractionSiteUpdateStatus
    {
        public int SiteId { get; set; }
        public int StatusId { get; set; }

        public static bool Validate(ConstractionSiteUpdateStatus obj)
        {
            if (obj == null 
              
                || obj.StatusId < 1
                || obj.StatusId > 4)
            {
                return false;
            }
            return true;
        }

    }
}
