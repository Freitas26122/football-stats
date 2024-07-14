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
            if (command == null)
                return BadRequest("Comando não pode ser nulo.");

            var league = new Leagues
            {
                LeagueId = command.LeagueId,
                Name = command.Name,
                Country = command.Country
            };

            await _leagueRepository.AddLeagueAsync(league);

            return CreatedAtAction(nameof(CreateLeague), new { id = league.LeagueId }, league);
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