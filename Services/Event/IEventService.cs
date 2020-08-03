using System.Collections.Generic;
using webApi.Models;
using System.Threading.Tasks;
using webApi.Dtos.Event;
using webApi.Dtos.Tournament;

namespace webApi.Services.Event
{
    public interface IEventService
    {
         Task<ServiceResponse<GetTournamentDto>> AddEvents(AddEventDto newEvent);

          Task<ServiceResponse<List<GetEventDto>>> GetAllEvents();

          Task<ServiceResponse<GetEventDto>> GetEventById(int id);
          Task<ServiceResponse<GetEventDto>> UpdateEvent(UpdateEventDto updatedEvent);

          Task<ServiceResponse<List<GetEventDto>>> DeleteEvent(int id);
    }
}