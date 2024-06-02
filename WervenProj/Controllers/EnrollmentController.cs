using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;

namespace WervenProj.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        public EnrollmentController(ILogger<EnrollmentController> log, IEnrollmentRepository enrollmentRepo)
        {
            _log = log;
            _enrollmentRepo = enrollmentRepo;
        }

        public ILogger _log { get; }
        public IEnrollmentRepository _enrollmentRepo { get; }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSite>> GetSites()
        {
            try
            {
                var result = await _enrollmentRepo.GetEnrollments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpPost]
        [Route("enrollEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateEnrollment(EnrollmentCreate data)
        {
            var isValid = EnrollmentCreate.Validate(data);
            if (!isValid) { 
                return   BadRequest("Object is empty");
            }
            try
            {
                var result = await _enrollmentRepo.EnrollEmployee(data);

                return result ? CreatedAtAction(nameof(CreateEnrollment), new { result = true }) : BadRequest("Provided data is not valid");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
        [HttpPut]
        [Route("unEnrollEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> StopEnrollment(EnrollmentStop data)
        {
            var isValid = EnrollmentStop.Validate(data);
            if (!isValid)
            {
                return BadRequest("Object is empty");
            }
            try
            {
                var result = await _enrollmentRepo.UnEnrollEmployee(data);

                return result == false ? NotFound("Employee or active enrollment is not found") : Ok("Employee is unenrolled");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
