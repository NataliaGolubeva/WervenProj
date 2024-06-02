using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WervenProj.IRepositories;
using WervenProj.Models.CreateModels;
using WervenProj.Models;

namespace WervenProj.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(ILogger<EmployeeController> log, IEmployeeRepository employeeRepo)
        {
            _log = log;
            _employeeRepo = employeeRepo;
        }

        public ILogger<EmployeeController> _log { get; }
        public IEmployeeRepository _employeeRepo { get; }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSite>> GetEmployees()
        {
            try
            {
                var result = await _employeeRepo.GetEmployees();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSite>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepo.GetEmployee(id);
                return result == null ? NotFound("Employee is not found") : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpPost]
        [Route("createEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateEmployee(EmployeeCreate data)
        {
            var isValid = EmployeeCreate.Validate(data);
            // validation
            if (!isValid)
            {
                return BadRequest("Provided object is not valid");
            }
            try
            {
                var result = await _employeeRepo.CreateEmployee(data);

                return CreatedAtAction(nameof(CreateEmployee), new { id = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
        [HttpDelete]
        [Route("deleteEmployee/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteSite(int id)
        {
            try
            {
                var result = await _employeeRepo.DeleteEmployeee(id);

                return result ? Ok() : NotFound("Employee with provided Id is not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
