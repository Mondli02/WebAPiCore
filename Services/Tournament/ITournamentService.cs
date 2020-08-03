using System.Collections.Generic;
using webApi.Models;
using System.Threading.Tasks;
using webApi.Dtos.Tournament;

namespace webApi.Services.Tournament
{
    public interface ITournamentService
    {
         Task<ServiceResponse<List<GetTournamentDto>>> GetAllTournamants();
         Task<ServiceResponse<GetTournamentDto>> GetTournamentById(int id);
         Task<ServiceResponse<List<GetTournamentDto>>> AddTournamants( AddTournamentDto newTournament);

         Task<ServiceResponse<GetTournamentDto>> UpdateTournament(UpdateTournamentDto updateTournament);

         Task<ServiceResponse<List<GetTournamentDto>>> DeleteTournament(int id);
    }
}