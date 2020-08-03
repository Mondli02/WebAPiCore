using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using System.Linq;
using webApi.Services.Tournament;
using System.Threading.Tasks;
using webApi.Dtos.Tournament;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace webApi.Controllers
{
[Authorize]
[ApiController]
[Route("[controller]")]
public class TournamentController: ControllerBase
{
  private readonly ITournamentService _tournamentService;
public TournamentController (ITournamentService tournamentService)
{
        this._tournamentService = tournamentService;
}
[Route("GetAll")]
public async Task<IActionResult> Get() // Get All Tournaments
{
        return Ok( await _tournamentService.GetAllTournamants());
}

[HttpGet("{id}")]
public async Task<IActionResult> GetSingle(int id) // Get Tournament by ID
{
        return Ok( await _tournamentService.GetTournamentById(id));
} 

[HttpPost]
public async Task<IActionResult> AddTournament(AddTournamentDto newTournament) // Add Tournament
{
        return Ok(await  _tournamentService.AddTournamants(newTournament));
}

[HttpPut]
public async Task<IActionResult> UpdateTournament(UpdateTournamentDto updateTournament) //Update Tournament
{
        ServiceResponse<GetTournamentDto> response = await _tournamentService.UpdateTournament(updateTournament);
        if(response.Data == null){
                return NotFound(response);
}
return Ok(response);
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id) // Delete Tournament
{
        ServiceResponse<List<GetTournamentDto>> response = await _tournamentService.DeleteTournament(id);
        if(response.Data == null){
                return NotFound(response);
}
        return Ok(response);
} 
    }
}
            
