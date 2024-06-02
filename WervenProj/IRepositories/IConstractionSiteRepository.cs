using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IConstractionSiteRepository
    {
        Task<IEnumerable<ConstractionSiteDTO>> GetSites();
        Task<ConstractionSiteDTO> GetConstractionSite(int siteId);
        Task<int> CreateSite(ConstractionSiteCreate site);
        Task<bool> UpdateSite(ConstractionSiteCreate site);
        Task<bool> UpdateSiteStatus(ConstractionSiteUpdateStatus data);
        Task<bool> DeleteSite(int siteId);

    }
}
