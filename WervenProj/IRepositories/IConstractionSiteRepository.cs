using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IConstractionSiteRepository
    {
        Task<IEnumerable<ConstractionSiteDTO>> GetConstractionSites();
        Task<ConstractionSiteDTO?> GetConstractionSite(int siteId);
        Task<IEnumerable<ConstractionStatus>> GetStatusList();
        Task<int> CreateConstractionSite(ConstractionSiteCreate site);
        Task<bool> UpdateConstractionSite(ConstractionSiteCreate site);
        Task<bool> UpdateConstractionSiteStatus(ConstractionSiteUpdateStatus data);
        Task<bool> DeleteConstractionSite(int siteId);

    }
}
