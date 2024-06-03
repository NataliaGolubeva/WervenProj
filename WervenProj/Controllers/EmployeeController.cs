using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WervenProj.IRepositories;
using WervenProj.Models.CreateModels;
using WervenProj.Models;
using WervenProj.Models.DTO;

namespace WervenProj.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(ILogger<EmployeeController> log, IEmployeeRepository employeeRepo, IEnrollmentRepository enrollmentRepo)
        {
            _log = log;
            _employeeRepo = employeeRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public ILogger<EmployeeController> _log { get; }
        public IEmployeeRepository _employeeRepo { get; }
        public IEnrollmentRepository _enrollmentRepo { get; }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> GetEmployees()
        {
            try
            {
                var result = await _employeeRepo.GetEmployees();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepo.GetEmployee(id);
                return result == null ? NotFound("Employee is not found") : Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
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
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
        [HttpDelete]
        [Route("deleteEmployee/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteEmployee(int id)
        {
            try
            {
                // check if employee is not assigned for an an active site
                var isActive = await _enrollmentRepo.IfEmployeeIsEnrolledForActiveSite(id);
                if (isActive) { 
                    return BadRequest("Action is denied. Employee is enrolled for an active site. Please unenroll employee first."); 
                }
                var result = await _employeeRepo.DeleteEmployeee(id);

                return result ? Ok("deleted") : NotFound("Employee with provided Id is not found");
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
