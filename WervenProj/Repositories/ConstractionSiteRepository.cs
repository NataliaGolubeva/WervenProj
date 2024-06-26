﻿using Microsoft.EntityFrameworkCore;
using WervenProj.Data;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.Repositories
{
    public class ConstractionSiteRepository : IConstractionSiteRepository
    {
        public ApplicationDbContext _db { get; }

        public ConstractionSiteRepository( ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ConstractionSiteDTO>> GetConstractionSites()
        {
            try
            {
                var sites = await _db.ConstractionSites.ToListAsync();
                var list = new List<ConstractionSiteDTO>();
                foreach (var site in sites)
                {
                    var status = await _db.ConstractionStatuses.FirstOrDefaultAsync(s => s.Id == site.StatusId);
                    var emloyeeList = (from enrollment in _db.ConstractionSiteEnrollments
                                       join employee in _db.Employees
                                       on enrollment.EmployeeId equals employee.Id
                                       join role in _db.EmployeeRoles 
                                       on employee.RoleId equals role.Id
                                       where enrollment.ConstractionSiteId == site.Id && enrollment.IsActive == "true"   
                                       select new EmployeeDTO
                                       {
                                           Id = employee.Id,
                                           Name = employee.Name,
                                           Role = role.RoleName,
                                           RoleNr = role.RoleNr
                                       }).ToList();

                    var siteDTO = new ConstractionSiteDTO()
                    {
                        Id = site.Id,
                        Name = site.Name,
                        Description = site.Description,
                        StatusId = site.StatusId,
                        Status = status != null? status.StatusName : "",
                        StatusNr = status != null? status.StatusNr : -1,
                        StartDate = site.StartDate,
                        EndDate = site.EndDate,
                        Employees = emloyeeList
                    };
                    list.Add(siteDTO);
                }
                return list;
            }
            catch (Exception ex) { throw; }
        }
        public async Task<ConstractionSiteDTO?> GetConstractionSite(int siteId)
        {
            try
            {
                var site = await _db.ConstractionSites.FirstOrDefaultAsync(s => s.Id == siteId);
                if (site == null)
                {
                    return null;
                }
                var status = await _db.ConstractionStatuses.FirstOrDefaultAsync(s => s.Id == site.StatusId);
                var emloyeeList = (from enrollment in _db.ConstractionSiteEnrollments
                                   join employee in _db.Employees
                                   on enrollment.EmployeeId equals employee.Id
                                   join role in _db.EmployeeRoles
                                   on employee.RoleId equals role.Id
                                   where enrollment.ConstractionSiteId == site.Id && enrollment.IsActive == "true"
                                   select new EmployeeDTO
                                   {
                                       Id = employee.Id,
                                       Name = employee.Name,
                                       Role = role.RoleName
                                   }).ToList();
                var dto = new ConstractionSiteDTO
                {
                    Id = site.Id,
                    Name = site.Name,
                    Description = site.Description,
                    StatusId = site.StatusId,
                    Status = status != null? status.StatusName : "",
                    StartDate = site.StartDate,
                    EndDate = site.EndDate,
                    Employees = emloyeeList

                };
                return dto;
            }
            catch (Exception ex) { throw; }

        }
        public async Task<int> CreateConstractionSite(ConstractionSiteCreate site)
        {
            try
            {
                var newSite = new ConstractionSite
                {
                    Name = site.Name,
                    Description = site.Description,
                    StatusId = site.StatusId,
                    StartDate = DateOnly.FromDateTime(DateTime.Today),
                };
                 
                _db.Add(newSite);
                await _db.SaveChangesAsync();
                return newSite.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Task<bool> UpdateConstractionSite(ConstractionSiteCreate site)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteConstractionSite(int siteId)
        {
            try
            {
                var site = await _db.ConstractionSites.FirstOrDefaultAsync(s => s.Id == siteId);
                if (site == null)
                {
                    return false;
                }
                else
                {
                    _db.ConstractionSites.Remove(site);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { throw; }
        }
        public async Task<bool> UpdateConstractionSiteStatus(ConstractionSiteUpdateStatus data)
        {
            try
            {
                var site = await _db.ConstractionSites.FirstOrDefaultAsync(s => s.Id == data.SiteId);
                if (site != null)
                {
                    site.StatusId = data.StatusId;
                    await _db.SaveChangesAsync();
                    return true;

                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ConstractionStatus>> GetStatusList()
        {
            try { 
                var statuses = await _db.ConstractionStatuses.ToListAsync();
                return statuses;
                    }
            catch (Exception ex) { throw; }
        }
    }
}
