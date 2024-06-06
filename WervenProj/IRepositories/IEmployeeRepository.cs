using WervenProj.Models.CreateModels;
using WervenProj.Models;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<IEnumerable<EmployeeRole>> GetRoles();
        Task<EmployeeDTO?> GetEmployee(int employeeId);
        Task<int> CreateEmployee(EmployeeCreate data);
        Task<bool> UpdateEmployee(EmployeeCreate data);
        Task<bool> DeleteEmployeee(int employeeId);
    }
}
