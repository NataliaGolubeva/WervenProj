﻿using Microsoft.EntityFrameworkCore;
using WervenProj.Data;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public ApplicationDbContext _db { get; }

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            try
            {
                var query = from employee in _db.Employees
                           join role in _db.EmployeeRoles
                           on employee.RoleId equals role.Id
                           select new EmployeeDTO()
                           {
                               Id = employee.Id,
                               Name = employee.Name,
                               Role = role.RoleName,
                               RoleNr = role.RoleNr

                           };
                var list = await query.ToListAsync();
                return list;
            }
            catch (Exception ex) { throw; }
        }
        public async Task<EmployeeDTO?> GetEmployee(int employeeId)
        {
            try
            {
                var query = from employee in _db.Employees
                            join role in _db.EmployeeRoles
                            on employee.RoleId equals role.Id
                            where employee.Id == employeeId 
                            select new EmployeeDTO()
                            {
                                Id = employee.Id,
                                Name = employee.Name,
                                Role = role.RoleName,
                                RoleNr = role.RoleNr

                            };
                var result = await query.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex) { throw; }
        }

        public  Task<bool> UpdateEmployee(EmployeeCreate data)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateEmployee(EmployeeCreate data)
        {
            try
            {
                var employee = new Employee
                {
                    Name = data.Name,
                    RoleId = data.RoleId
                };

                _db.Add(employee);
                await _db.SaveChangesAsync();
                return employee.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEmployeee(int employeeId)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(s => s.Id == employeeId);
                if (employee == null)
                {
                    return false;
                }
                else
                {
                    _db.Employees.Remove(employee);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { throw; }
        }

        public async Task<IEnumerable<EmployeeRole>> GetRoles()
        {
            try
            {
                var roles = await _db.EmployeeRoles.ToListAsync();
                return roles;

            }
            catch (Exception ex) { throw; }
        }
    }
}
