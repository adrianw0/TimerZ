using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimerZ.Domain.Models;
using TimerZ.Repository.Interfaces;

namespace TimerZ.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsReadRepository _labelsReadRepo;
        private readonly ILabelsWriteRepository _labelsWriteRepo;

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Label))]
        [HttpGet("Labels")]
        public IActionResult GetLabels()
        {
            var labels = _labelsReadRepo.GetAllLabels();
            return Ok(labels);
        }

        [HttpPost("AddLabel")]
        public IActionResult AddLabel([FromBody] Label label)
        {
            try
            {
                _labelsWriteRepo.AddNewLabel(label);
            }
            catch (Exception)
            {
                return BadRequest();

            }
            return Created("", label);
        }
        [HttpDelete("DeleteLabel/{id}")]
        public IActionResult DeleteLabel(int id)
        {
            try
            {
                _labelsWriteRepo.DeleteLabel(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }


        public LabelsController(ILabelsReadRepository labelsReadRepo,  ILabelsWriteRepository labelsWriteRepo)
        {
            _labelsReadRepo = labelsReadRepo;
            _labelsWriteRepo = labelsWriteRepo;
        }
    }
}
