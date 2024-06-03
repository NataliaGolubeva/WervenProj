using System.Collections;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IEnrollmentRepository
    {
        Task<bool> EnrollEmployee(EnrollmentData data);
        Task<bool> UnEnrollEmployee(EnrollmentData data);
        Task<bool> IfEmployeeIsEnrolledForActiveSite(int id);
        Task<IEnumerable<EnrollmentsDTO>> GetEnrollments();
        Task<IEnumerable<EnrollmentsDTO>> GetActiveEnrollmentsForEmployee(int employeeId);
        Task<IEnumerable<EnrollmentsDTO>> GetActiveEnrollmentsForSite(int siteId);
        Task<ConstractionSiteEnrollments?> GetSelectedEnrollment(EnrollmentData data);
        
    }
}
