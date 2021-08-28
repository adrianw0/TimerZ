using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Api.Dtos;
using TimerZ.Api.Extensions;
using TimerZ.Api.Mapper;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class TimerEntriesController : ControllerBase
    {
        private readonly ITimerEntryReadRepository _timerEntryReadRepo;
        private readonly ITimerEntryWriteRepository _timerEntryWriteRepo;


        [HttpGet("Entries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimerEntryDTO))]
        public IActionResult GetEntries()
        {           
            var entries = _timerEntryReadRepo.GetAllEntries();

            //TODO: move mapping to separate class
            var entriesDto = entries.Select(TimerEntryMapper.TimerEntryToDto).OrderByDescending(e=>e.StartDate);

            return Ok(entriesDto);

        }

        [HttpPost("AddEntry")]
        public IActionResult AddEntry([FromBody] TimerEntryDTO dtoEntry)
        {
            var entry = TimerEntryMapper.DtoToTimerEntry(dtoEntry);
            entry.UserId = HttpContext.User.GetUserId();
            try
            {
                var newEntry = _timerEntryWriteRepo.AddOrUpdateTimerEntry(entry);
                
                return Created("", TimerEntryMapper.TimerEntryToDto(newEntry));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }

        }

        [HttpGet("GetRunningEntry")]
        public IActionResult GetRunningEntry()
        {
            try
            {
                return Ok(_timerEntryReadRepo.GetRunning());
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }
        }

        [HttpDelete("DeleteTimerEntry/{id}")]
        public IActionResult DeleteTimerEntry(int id)
        {
            try
            {
                _timerEntryWriteRepo.DeleteTimerEntry(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }

        public TimerEntriesController(ITimerEntryReadRepository timerEntryReadRepo, ITimerEntryWriteRepository timerEntryWriteRepo)
        {
            _timerEntryReadRepo = timerEntryReadRepo;
            _timerEntryWriteRepo = timerEntryWriteRepo;
        }
    }
}
