using System.Collections;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IEnrollmentRepository
    {
        Task<bool> EnrollEmployee(EnrollmentCreate data);
        Task<bool> UnEnrollEmployee(EnrollmentStop data);
        Task<IEnumerable<EnrollmentsDTO>> GetEnrollments();
    }
}
