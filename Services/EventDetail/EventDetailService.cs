using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApi.Data;
using webApi.Dtos.Event;
using webApi.Dtos.EventDetail;
using webApi.Models;

namespace webApi.Services.EventDetail
{
    public class EventDetailService : IEventDetailService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventDetailService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
          public async Task<ServiceResponse<GetEventDto>> AddDetailEvents(AddEventDetailDto newDetailEvent)
        { 
                 
         ServiceResponse<GetEventDto> response = new ServiceResponse<GetEventDto>();
         try
         {
             webApi.Models.Event events = await _context.Events
                   .Include(c => c.Tournament)
                   .Include(c => c.EventDetail).ThenInclude(cs => cs.EventDetailStatus)
                    .FirstOrDefaultAsync(c => c.eventID == newDetailEvent.eventID &&
                    c.Tournament.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

              if(events == null)
                {
                    response.Success = false;
                    response.Message = "Event not found.";
                    return response;
                }
                 EventDetailStatus eventStatus = await _context.EventDetailStatuses
                    .FirstOrDefaultAsync(s => s.EventDetailStatusId == newDetailEvent.EventDetailStatusId);
              if(eventStatus == null)
                {
                    response.Success = false;
                    response.Message = "Event Stutus not found";
                    return response;
                }
                webApi.Models.EventDetail eventDetail = new  webApi.Models.EventDetail
                {
                    Event = events,
                    EventDetailStatus = eventStatus,
                    EventDetailName = newDetailEvent.eventDetailName,
                    EventDetailNumber = newDetailEvent.eventDetailNumber,
                    EventDetailOdd = newDetailEvent.eventDetailOdd,
                    FinishingPosition  = newDetailEvent.finishingPosition,
                    FirstTimer  = newDetailEvent.firstTimer

                };

                await _context.EventDetails.AddAsync(eventDetail);
                await _context.SaveChangesAsync();

                  response.Data = _mapper.Map<GetEventDto>(events);

         }catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}