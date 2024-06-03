using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WervenProj.Data;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<bool> EnrollEmployee(EnrollmentData data)
        {
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
        public async Task<bool> UnEnrollEmployee(EnrollmentData data)
        {
            try
            {
                var enrollment = await _db.ConstractionSiteEnrollments.FirstOrDefaultAsync(e => e.ConstractionSiteId == data.ConstractionSiteId && e.EmployeeId == data.EmployeeId);
                if (enrollment != null)
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
                var query = from enrollment in _db.ConstractionSiteEnrollments
                          join employee in _db.Employees
                          on enrollment.EmployeeId equals employee.Id
                          join site in _db.ConstractionSites
                          on enrollment.ConstractionSiteId equals site.Id
                          select new EnrollmentsDTO()
                          {
                              Id = enrollment.Id,
                              ConstractionSiteId = enrollment.ConstractionSiteId,
                              ConstractionSiteName = site.Name,
                              EmployeeId = enrollment.EmployeeId,
                              EmployeeName = employee.Name,
                              StartDate = enrollment.StartDate,
                              EndDate = enrollment.EndDate,
                              IsActive = enrollment.IsActive,
                          };
                var list = await query.ToListAsync();
                return list;
            }
            catch (Exception ex) { throw; }
        }
        public async Task<IEnumerable<EnrollmentsDTO>> GetActiveEnrollmentsForEmployee(int employeeId)
        {
            // active means that current employee is enrolled with site
            try
            {
               var query = (from e in _db.ConstractionSiteEnrollments
                           join site in _db.ConstractionSites
                           on e.ConstractionSiteId equals site.Id
                                where e.EmployeeId == employeeId && e.IsActive == "true"
                                select new EnrollmentsDTO()
                            {
                                Id = e.Id,
                                ConstractionSiteId = site.Id,
                                ConstractionSiteName = site.Name,
                                StartDate = e.StartDate,
                                EmployeeId = employeeId,
                                IsActive = e.IsActive
                                

                            });
                var list = await query.ToListAsync();
                return list;

            }
            catch (Exception ex) { throw; }
        }
        public async Task<IEnumerable<EnrollmentsDTO>> GetActiveEnrollmentsForSite(int siteId)
        {
            try
            {
                var query = (from e in _db.ConstractionSiteEnrollments
                             join employee in _db.Employees
                             on e.EmployeeId equals employee.Id
                             where e.ConstractionSiteId == siteId && e.IsActive == "true"
                             select new EnrollmentsDTO()
                             {
                                 Id = e.Id,
                                 ConstractionSiteId = siteId,
                                 StartDate = e.StartDate,
                                 EmployeeId = employee.Id,
                                 EmployeeName = employee.Name,
                                 IsActive = e.IsActive
                             });
                var list = await query.ToListAsync();
                return list;

            }
            catch (Exception ex) { throw; }
        }
        public async Task<bool> IfEmployeeIsEnrolledForActiveSite(int employeeId)
        {
            try
            {
                var query = from e in _db.ConstractionSiteEnrollments
                                  join s in _db.ConstractionSites
                                  on e.ConstractionSiteId equals s.Id
                                  where e.EmployeeId == employeeId && e.IsActive == "true" && s.StatusId == 3
                                  select e;
                var enrollments = await query.ToListAsync();   
                if (enrollments.Any())
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex) { throw; }
        }
        public async Task<ConstractionSiteEnrollments?> GetSelectedEnrollment(EnrollmentData data)
        {
            try
            {
                var enrollment = await _db.ConstractionSiteEnrollments.FirstOrDefaultAsync(e => e.ConstractionSiteId == data.ConstractionSiteId && e.EmployeeId == data.EmployeeId);
                return enrollment;

            }
            catch (Exception ex) { throw; }
        }
    }
}
