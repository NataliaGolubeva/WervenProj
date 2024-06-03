﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ConstractionSite>> GetAllEnrollments()
        {
            try
            {
                var result = await _enrollmentRepo.GetEnrollments();
                return Ok(result);
            }
            catch (Exception ex)
            {

                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpGet]
        [Route("getActive/employee{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSite>> GetActiveEnrollmentsListForEmployee(int id)
        {
            // possible to check first if employee exists
            try
            {
                var result = await _enrollmentRepo.GetActiveEnrollmentsForEmployee(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpGet]
        [Route("getActive/site{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSite>> GetActiveEnrollmentsListForSite(int id)
        {
            // possible to check first if site  exists
            try
            {
                var result = await _enrollmentRepo.GetActiveEnrollmentsForSite(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpPost]
        [Route("enrollEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateEnrollment(EnrollmentData data)
        {
            var isValid = EnrollmentData.Validate(data);

            if (!isValid) { 
                return   BadRequest("Object is empty");
            }
            try
            {
                // check first if enrollment exist
                var selected = await _enrollmentRepo.GetSelectedEnrollment(data);
                if (selected != null) {
                    // check if enrollment is active, so employee is already enrolled for this site. To avoid duplicates
                    if (selected.IsActive == "true")
                    {
                        return BadRequest("Employee is already enrolled for this site");
                    }
                }
               
                // if not exist or not active, enroll
                var result = await _enrollmentRepo.EnrollEmployee(data);
                return result ? CreatedAtAction(nameof(CreateEnrollment), new { result = true }) : BadRequest("Provided data is not valid");
                
                
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
        [HttpPut]
        [Route("unenrollEmployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> StopEnrollment(EnrollmentData data)
        {
            try
            {
                var result = await _enrollmentRepo.UnEnrollEmployee(data);

                return result == false ? NotFound("Employee or active enrollment is not found") : Ok("Employee is unenrolled");

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
