using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WervenProj.IRepositories;
using WervenProj.Models.CreateModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WervenProj.Controllers
{
    [ApiController]
    [Route("api/seedDb")]
    public class SeedDBController : ControllerBase
    {
        public SeedDBController(ILogger<SeedDBController> log, IEmployeeRepository employeeRepo, IConstractionSiteRepository constractionSiteRepo)
        {
            _log = log;
            _employeeRepo = employeeRepo;
            _constractionSiteRepo = constractionSiteRepo;
        }

        public ILogger<SeedDBController> _log { get; }
        public IEmployeeRepository _employeeRepo { get; }
        public IConstractionSiteRepository _constractionSiteRepo { get; }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Seed()
        {
        
            try
            {
                List<EmployeeCreate> employees = new List<EmployeeCreate>
                {
                    new EmployeeCreate {Name= "Julien Doyle", RoleId= 1},
                    new EmployeeCreate {Name= "Taylor Melton", RoleId= 2},
                    new EmployeeCreate {Name= "Abbey Page", RoleId= 3},
                    new EmployeeCreate {Name= "Charity Barrett", RoleId= 4},
                    new EmployeeCreate {Name= "Marissa Jennings", RoleId= 1},
                    new EmployeeCreate {Name= "Tyson Austin", RoleId= 2},
                    new EmployeeCreate {Name= "Bradley Silva", RoleId= 3},
                    new EmployeeCreate {Name= "Ronan Mejia", RoleId= 4},
                    new EmployeeCreate {Name= "Larry Lawson", RoleId= 1},
                    new EmployeeCreate {Name= "Dream Benjamin", RoleId= 2},
                    new EmployeeCreate {Name= "Mavis Warner", RoleId= 3},
                    new EmployeeCreate {Name= "Leo Wells", RoleId= 4},
                    new EmployeeCreate {Name= "Jacqueline Schmidt", RoleId= 2},


                };
                foreach (EmployeeCreate employee in employees) {
                    await _employeeRepo.CreateEmployee(employee);
                }
                List<ConstractionSiteCreate> sites = new List<ConstractionSiteCreate>
                {
                    new ConstractionSiteCreate {
                        Name= "Business Center", 
                        Description= "New buisness center with offices",
                        StatusId= 1
                    },
                     new ConstractionSiteCreate {
                        Name= "School",
                        Description= "New school in the city",
                        StatusId= 2
                    },
                      new ConstractionSiteCreate {
                        Name= "Residential area",
                        Description= "New residential area with park and childrens playground ",
                        StatusId= 3
                    },
                       new ConstractionSiteCreate {
                        Name= "School 2",
                        Description= "New school in the city",
                        StatusId= 1
                    },
                      new ConstractionSiteCreate {
                        Name= "Residential area 2",
                        Description= "New residential area with park and childrens playground ",
                        StatusId= 4
                    }
                };
                foreach (ConstractionSiteCreate site in sites)
                {
                    await _constractionSiteRepo.CreateSite(site);
                }
                return CreatedAtAction(nameof(Seed), new { result = "success" });
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
