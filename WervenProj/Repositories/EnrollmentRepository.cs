using Microsoft.EntityFrameworkCore;
using WervenProj.Data;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public EnrollmentRepository(ILogger<EnrollmentRepository> log, ApplicationDbContext db)
        {
            _log = log;
            _db = db;
        }

        public ILogger _log { get; }
        public ApplicationDbContext _db { get; }

        public async Task<bool> EnrollEmployee(EnrollmentCreate data)
        {
            // check for duplicates to avoid
            try
            {
                var site = await _db.ConstractionSites.FirstOrDefaultAsync(s => s.Id == data.ConstractionSiteId);
                var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == data.EmployeeId);
                if (site != null && employee != null) {
                 var newEnrollment = new ConstractionSiteEnrollments
                 { 
                     EmployeeId = data.EmployeeId,
                     ConstractionSiteId = data.ConstractionSiteId, 
                     StartDate = DateTime.Now,
                     IsActive = "true"
                 };
                    _db.Add(newEnrollment);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> UnEnrollEmployee(EnrollmentStop data)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == data.EmployeeId);
                var enrollment = await _db.ConstractionSiteEnrollments.FirstOrDefaultAsync(e => e.Id == data.ConstractionSiteEnrollmentId);
                if (enrollment != null && employee != null )
                {
                    enrollment.EndDate = DateTime.Now;
                    enrollment.IsActive = "false";
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<EnrollmentsDTO>> GetEnrollments()
        {
            try
            {
                var list = from e in _db.ConstractionSiteEnrollments
                          from emp in _db.Employees
                          from c in _db.ConstractionSites
                          where e.EmployeeId == emp.Id && e.ConstractionSiteId == c.Id
                          select new EnrollmentsDTO()
                          {
                              Id = e.Id,
                              ConstractionSiteId = e.ConstractionSiteId,
                              ConstractionSiteName = c.Name,
                              EmployeeId = e.EmployeeId,
                              EmployeeName = emp.Name,
                              StartDate = e.StartDate,
                              EndDate = e.EndDate,
                              IsActive = e.IsActive,
                          };

                return list;
            }
            catch (Exception ex) { throw; }
        }
    }
}
