using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.Dtos;
using TimerZ.Api.Extensions;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class TimerEntriesController : Controller
    {

        private readonly ITimeTrackingService _timeTrackingService;


        [HttpGet("Entries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimerEntryDTO))]
        public async Task<IActionResult> GetEntries()
        {
            return Ok(await _timeTrackingService.GetEntries()); //TODO: PAGING!!!
        }

        [HttpPost("AddEntry")]
        public async Task<IActionResult> AddEntry([FromBody] TimerEntryDTO dtoEntry)
        {
            var userId = HttpContext.User.GetUserId();
            try
            {
                var newEntry = await _timeTrackingService.AddEntry(dtoEntry, userId);

                return Created(string.Empty, newEntry);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }

        }

        [HttpGet("GetRunningEntry")]
        public async Task<IActionResult> GetRunningEntry()
        {
            try
            {
                return Ok(await _timeTrackingService.GetRunningEntry());
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }
        }

        [HttpDelete("DeleteTimerEntry/{id}")]
        public async Task<IActionResult> DeleteTimerEntry(int id)
        {
            try
            {
                await _timeTrackingService.DeleteTimerEntry(id);
            }
            catch (Exception e)
            {
                BadRequest(e.InnerException?.Message);
            }
            return Ok();
        }

        public TimerEntriesController( ITimeTrackingService timeTrackingService)
        {
 
            _timeTrackingService = timeTrackingService;
        }
    }
}
