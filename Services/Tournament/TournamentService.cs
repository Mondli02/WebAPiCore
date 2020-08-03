using System.Collections.Generic;
using webApi.Models;
using System.Linq;
using System.Threading.Tasks;
using webApi.Dtos.Tournament;
using AutoMapper;
using System;
using webApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using webApi.Services.Tournament;

namespace webApi.Services.Tournament
{

    public class TournamentService : ITournamentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TournamentService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        int  GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); // Authenticated user.

        public async Task<ServiceResponse<List<GetTournamentDto>>> AddTournamants(AddTournamentDto newTournament) // Add user to the database.
        {
            ServiceResponse<List<GetTournamentDto>> serviceResponse = new ServiceResponse<List<GetTournamentDto>>();
            webApi.Models.Tournament tournament = _mapper.Map<webApi.Models.Tournament>(newTournament); //Map Tournament to the tournament type.
              
            tournament.user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId()); // grab Id of the authenticated user

            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Tournaments.Where(c => c.user.Id == GetUserId()).Select(c => _mapper.Map<GetTournamentDto>(c))).ToList(); // get results related to authorized user.
            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetTournamentDto>>> GetAllTournamants() // Get all tournaments
        {
            ServiceResponse<List<GetTournamentDto>> serviceResponse = new ServiceResponse<List<GetTournamentDto>>();
            List<webApi.Models.Tournament> dbTournament = await _context.Tournaments.Where(c => c.user.Id== GetUserId()).ToListAsync(); // grab Id of the authenticated user

            serviceResponse.Data = (dbTournament.Select(c => _mapper.Map<GetTournamentDto>(c))).ToList(); ;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTournamentDto>> GetTournamentById(int id) // Get Toutnament by ID and authenticated user
        {
            ServiceResponse<GetTournamentDto> serviceResponse = new ServiceResponse<GetTournamentDto>();
            webApi.Models.Tournament dbTournament = await _context.Tournaments.FirstOrDefaultAsync(c => c.TournamentId == id && c.user.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetTournamentDto>(dbTournament);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTournamentDto>> UpdateTournament(UpdateTournamentDto updateTournament)
        {
            ServiceResponse<GetTournamentDto> serviceResponse = new ServiceResponse<GetTournamentDto>();
            try
            {
                webApi.Models.Tournament tournament =  await _context.Tournaments
                                                                    .Include(c => c.user)
                                                                    .FirstOrDefaultAsync(c => c.TournamentId == updateTournament.TournamentId);
                if(tournament.user.Id == GetUserId())
                {
                    tournament.TournamentName = updateTournament.TournamentName;

                    _context.Tournaments.Update(tournament);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetTournamentDto>(tournament);
                }
                else
                {
                      serviceResponse.Success = false;
                      serviceResponse.Message = "Tournament not found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetTournamentDto>>> DeleteTournament(int Id) // Delete allowed only to autheciated users
        {
            ServiceResponse<List<GetTournamentDto>> serviceResponse = new ServiceResponse<List<GetTournamentDto>>();
            try
            {

                webApi.Models.Tournament tournament = await _context.Tournaments.FirstOrDefaultAsync(c => c.TournamentId == Id && c.user.Id == GetUserId());
                if(tournament != null)
                {
                    _context.Tournaments.Remove(tournament);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = (_context.Tournaments
                                                            .Where(c => c.user.Id == GetUserId())
                                                            .Select(c => _mapper.Map<GetTournamentDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message ="Tournament not found";
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