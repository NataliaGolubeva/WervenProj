using WervenProj.Models.CreateModels;
using WervenProj.Models;
using WervenProj.Models.DTO;

namespace WervenProj.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<int> CreateEmployee(EmployeeCreate data);
        Task<bool> UpdateEmployee(EmployeeCreate data);
        Task<bool> DeleteEmployeee(int employeeId);
    }
}
