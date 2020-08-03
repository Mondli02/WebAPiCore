using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos.Event;
using webApi.Models;
using webApi.Services.Event;

namespace webApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
         private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
         [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventDto newEvent)
        {
            return Ok(await _eventService.AddEvents(newEvent));
        }
         [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _eventService.GetAllEvents());
        }
         [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _eventService.GetEventById(id));
        }
         [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto updatedEvent)
        {
            ServiceResponse<GetEventDto> response = await _eventService.UpdateEvent(updatedEvent);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetEventDto>> response = await _eventService.DeleteEvent(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}