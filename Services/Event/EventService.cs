using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApi.Data;
using webApi.Dtos.Event;
using webApi.Dtos.Tournament;
using webApi.Models;

namespace webApi.Services.Event
{
    public class EventService : IEventService
    {
          private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

          int  GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); // Authenticated user.
     
        public async Task<ServiceResponse<GetTournamentDto>> AddEvents(AddEventDto newEvent)
        { 
                  
            ServiceResponse<GetTournamentDto> response = new ServiceResponse<GetTournamentDto>();
         try
            {
              
            webApi.Models.Tournament tournament = await _context.Tournaments
            .FirstOrDefaultAsync(t => t.TournamentId == newEvent.TournamentId &&
            t.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

             if(tournament == null)
                {
                    response.Success = false;
                    response.Message = "Tournament not found.";
                    return response;
                }

               webApi.Models.Event events = new webApi.Models.Event
                {
                    eventName     = newEvent.EventName,
                    eventNumber     = newEvent.Eventnumber,
                    eventDateTime  = newEvent.EventDate,
                    Tournament = tournament
                   
                };
                 await _context.Events.AddAsync(events);
                 await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetTournamentDto>(tournament);

            }
             catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetEventDto>>> GetAllEvents()
            {
                ServiceResponse<List<GetEventDto>> serviceResponse = new ServiceResponse<List<GetEventDto>>();
                List<webApi.Models.Event> dbevents = await _context.Events.Where(c => c.Tournament.user.Id == GetUserId()).ToListAsync();
                serviceResponse.Data = (dbevents.Select(c => _mapper.Map<GetEventDto>(c))).ToList();
                return serviceResponse;
            }
       public async Task<ServiceResponse<GetEventDto>> GetEventById(int id)
        {
            ServiceResponse<GetEventDto> serviceResponse = new ServiceResponse<GetEventDto>();
                 webApi.Models.Event events = await _context.Events
                   .Include(c => c.Tournament)
                   .Include(c => c.EventDetail).ThenInclude(cs => cs.EventDetailStatus)
                    .FirstOrDefaultAsync(c => c.eventID == id &&
                    c.Tournament.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));


            serviceResponse.Data = _mapper.Map<GetEventDto>(events);
            return serviceResponse;
        }
       public async Task<ServiceResponse<GetEventDto>> UpdateEvent(UpdateEventDto updatedEvent)
        {
            ServiceResponse<GetEventDto> serviceResponse = new ServiceResponse<GetEventDto>();
            try
            {
            webApi.Models.Event events = await _context.Events.Include(c => c.Tournament.user).FirstOrDefaultAsync(c => c.eventID == updatedEvent.eventID);
                if (events.Tournament.user.Id == GetUserId())
                {
                    events.eventName = updatedEvent.eventName;
                    events.Tournament = updatedEvent.Tournament;
                    events.eventNumber = updatedEvent.eventNumber;
                    events.eventDateTime = updatedEvent.eventDateTime;
                    events.eventEndDateTime = updatedEvent.eventEndDateTime;
                 

                    _context.Events.Update(events);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetEventDto>(events);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Event not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
           public async Task<ServiceResponse<List<GetEventDto>>> DeleteEvent(int id)
        {
            ServiceResponse<List<GetEventDto>> serviceResponse = new ServiceResponse<List<GetEventDto>>();
            try
            {
                  webApi.Models.Event events = await _context.Events
                    .FirstOrDefaultAsync(c => c.eventID == id && c.Tournament.user.Id == GetUserId());
                if (events != null)
                {
                    _context.Events.Remove(events);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Events.Where(c => c.Tournament.user.Id == GetUserId())
                        .Select(c => _mapper.Map<GetEventDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Event not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        
    }
}