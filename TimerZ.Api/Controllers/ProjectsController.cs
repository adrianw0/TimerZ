using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.Extensions;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectsService;


        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectsService.GetProjects());
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {

             var userId = HttpContext.User.GetUserId();
            try
            {
                await _projectsService.AddProject(project, userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);

            }
            return Created("", project);
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectsService.DeleteProject(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
            return Ok();
        }


        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }
    }
}
