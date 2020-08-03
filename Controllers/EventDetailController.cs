using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos.EventDetail;
using webApi.Models;
using webApi.Services.EventDetail;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventDetailController: ControllerBase
    {
        
      private readonly IEventDetailService _eventDetailService;
        public EventDetailController(IEventDetailService eventDetailService)
        {
            _eventDetailService = eventDetailService;
        }

          [HttpPost]
        public async Task<IActionResult> AddDetailEvent(AddEventDetailDto newDetailEvent)
        {
                return Ok(await _eventDetailService.AddDetailEvents(newDetailEvent));
        }
    }
}