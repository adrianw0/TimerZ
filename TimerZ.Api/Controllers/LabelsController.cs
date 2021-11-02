using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.Extensions;
using TimerZ.Domain.Models;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class LabelsController : Controller
    {
        private readonly ILabelsService _labelsService;


        [HttpGet("Labels")]
        public async Task<IActionResult> GetLabels()
        {
            return Ok( await _labelsService.GetLabels());
        }

        [HttpPost("AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] Label label)
        {
            var userId = HttpContext.User.GetUserId();
            try
            {
                await _labelsService.AddLabel(label, userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message?? e.Message);
            }
            return Created("", label);
        }
        [HttpDelete("DeleteLabel/{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            try
            {
                await _labelsService.DeleteLabel(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }


        public LabelsController(ILabelsService labelsService)
        {
            _labelsService = labelsService;
        }
    }
}
