using Microsoft.AspNetCore.Mvc;
using FootballStatsAPI.Repositories;
using FootballStatsAPI.Models;

namespace FootballStatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeaguesController(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            var result = await command.ExecuteAsync(_leagueRepository);

            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return CreatedAtAction(nameof(CreateLeague), command);
        }

        [HttpPut("{leagueId}")]
        public async Task<IActionResult> UpdateLeague(string leagueId, [FromBody] UpdateLeagueCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            var result = await command.ExecuteAsync(leagueId, _leagueRepository);

            if (!result.IsSuccess) return result.IsNotFound ? NotFound() : BadRequest(result.ErrorMessage);
            return CreatedAtAction(nameof(CreateLeague), command);
        }

        [HttpGet("{leagueId}")]
        public async Task<ActionResult<Leagues>> GetLeagueById(string leagueId)
        {
            var league = await _leagueRepository.GetLeagueByIdAsync(leagueId);
            if (league == null) return NotFound();

            return league;
        }
    }
}