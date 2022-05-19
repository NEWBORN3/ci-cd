using EventManagmentApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository repository;

        public EventsController(IEventRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))]
        public IActionResult GetAll() => Ok(repository.GetAll());

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        public IActionResult GetById(int id)
        {
            var exsisitngEvent = repository.GetById(id);
            if (exsisitngEvent == null)
            {
                return NotFound();
            }
            return Ok(exsisitngEvent);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult Add([FromBody] Event newEvent)
        {
            if (newEvent.Id < 1)
            {
                return BadRequest("Invalid id");
            }
            repository.Add(newEvent);
            return CreatedAtAction(nameof(GetById), new { id =newEvent.Id }, newEvent);
        }

        [HttpDelete]
        [Route("{eventsToDeleteId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int eventsToDeleteId)
        {
            try
            {
                repository.Delete(eventsToDeleteId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
