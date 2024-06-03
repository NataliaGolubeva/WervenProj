﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WervenProj.IRepositories;
using WervenProj.Models;
using WervenProj.Models.CreateModels;
using WervenProj.Models.DTO;

namespace WervenProj.Controllers
{
    [ApiController]
    [Route("api/constractionSites")]
    public class ConstractionSiteController : ControllerBase
    {
        public ConstractionSiteController(ILogger<ConstractionSiteController> log, IConstractionSiteRepository constractionSiteRepo, IEnrollmentRepository enrollmentRepo)
        {
            _log = log;
            _constractionSiteRepo = constractionSiteRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public ILogger<ConstractionSiteController> _log { get; }
        public IConstractionSiteRepository _constractionSiteRepo { get; }
        public IEnrollmentRepository _enrollmentRepo { get; }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<ConstractionSite>> GetSites()
        {
            try
            {
                var result = await _constractionSiteRepo.GetConstractionSites();
                return Ok(result);
            }
            catch (Exception ex) {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }
            
        }
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConstractionSiteDTO>> GetSite(int id)
        {
            try
            {
                var result = await _constractionSiteRepo.GetConstractionSite(id);
                return result == null ? NotFound("Site is not found") : Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ");
            }

        }
        [HttpPost]
        [Route("createSite")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateSite(ConstractionSiteCreate site)
        {
            // validate status manually, the best is to fetch statuses and select available from dropdown
            var isValid = ConstractionSiteCreate.Validate(site);
            if (  !isValid )
            {
                return BadRequest("Provided data is not valid");
            }
            try
            {
                var result = await _constractionSiteRepo.CreateConstractionSite(site);

                return CreatedAtAction(nameof(CreateSite), new { id = result });
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
        [HttpPut]
        [Route("updateSiteStatus")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> UpdateStatus(ConstractionSiteUpdateStatus data)
        {
            // validate manually, the best is to fetch statuses and select available from dropdown
            var isValid = ConstractionSiteUpdateStatus.Validate(data);  
            if (!isValid)
            {
                return BadRequest("Provided data is not valid");
            }
            try
            {
                var result = await _constractionSiteRepo.UpdateConstractionSiteStatus(data);

                return result == false ? NotFound("Site is not found") : Ok("Status is updated successfully");

            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }

        [HttpDelete]
        [Route("deleteSite/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> DeleteSite(int id)
        {
            try
            {
                var result = await _constractionSiteRepo.DeleteConstractionSite(id);

                return result ? Ok("Site deleted successfully") : NotFound("Site with provided Id not found");
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }
    }
}
