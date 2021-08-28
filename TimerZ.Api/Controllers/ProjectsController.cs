using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.Extensions;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsReadRepository _projectsReadRepo;
        private readonly IProjectsWriteRepository _projectWriteRepo;


        [HttpGet("Projects")]
        public IActionResult GetProjects()
        {
            var projects = _projectsReadRepo.GetAllProjects();
            return Ok(projects);
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] Project project)
        {
            project.UserId = HttpContext.User.GetUserId();
            try
            {
                _projectWriteRepo.AddNewProject(project);
            }
            catch (Exception e)
            {
                var a = e.Message;
                return BadRequest();

            }
            return Created("", project);
        }

        [HttpDelete("DeleteProject/{id}")]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                _projectWriteRepo.DeleteProject(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }


        public ProjectsController(IProjectsReadRepository projectsReadRepo,  IProjectsWriteRepository projectWriteRepo)
        {
            _projectsReadRepo = projectsReadRepo;
            _projectWriteRepo = projectWriteRepo;
        }
    }
}
