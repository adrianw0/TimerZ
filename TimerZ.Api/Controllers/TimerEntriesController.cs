using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.DTOs;
using TimerZ.Api.Extensions;
using TimerZ.Domain.Models;
using TimerZ.TimerTracking.Services.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class TimerEntriesController : Controller
    {

        private readonly ITimeTrackingService _timeTrackingService;
        private readonly IMapper _mapper;


        [HttpGet("Entries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimerEntryDto))]
        public async Task<IActionResult> GetEntries()
        {
            var entries = await _timeTrackingService.GetEntries();
            return Ok(_mapper.Map<List<TimerEntryDto>>(entries));
        }

        [HttpPost("AddEntry")]
        public async Task<IActionResult> AddEntry([FromBody] TimerEntryDto dtoEntry)
        {
            var userId = HttpContext.User.GetUserId();
            var entry = _mapper.Map<TimerEntry>(dtoEntry);
            try
            {
                
                var newEntry = await _timeTrackingService.AddEntry(entry, userId);

                return Created(string.Empty, _mapper.Map<TimerEntryDto>(newEntry));
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
                var entry = await _timeTrackingService.GetRunningEntry();
                return Ok(_mapper.Map<TimerEntryDto>(entry));
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

        public TimerEntriesController( ITimeTrackingService timeTrackingService, IMapper mapper)
        {
            _timeTrackingService = timeTrackingService;
            _mapper = mapper;
        }
    }
}
