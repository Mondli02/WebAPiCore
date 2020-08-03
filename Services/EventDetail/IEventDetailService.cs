using System.Threading.Tasks;
using webApi.Dtos.Event;
using webApi.Dtos.EventDetail;
using webApi.Models;

namespace webApi.Services.EventDetail
{
    public interface IEventDetailService
    {
          Task<ServiceResponse<GetEventDto>> AddDetailEvents(AddEventDetailDto newDetailEvent);
    }  
}