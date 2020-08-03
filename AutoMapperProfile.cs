using AutoMapper;
using webApi.Dtos.Event;
using webApi.Dtos.Tournament;
using webApi.Models;

namespace webApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tournament, GetTournamentDto>();
            CreateMap<AddTournamentDto,Tournament>(); 
            CreateMap<Event,GetEventDto>(); 
        }
        
    }

}