using Microsoft.AspNetCore.Mvc;
using FootballStatsAPI.Repositories;
using FootballStatsAPI.Models;
using FootballStatsAPI.Queries;

namespace FootballStatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly FootballContext _context;

        public TeamsController(ITeamRepository teamRepository, FootballContext context)
        {
            _teamRepository = teamRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await command.ExecuteAsync(_teamRepository);

            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return CreatedAtAction(nameof(CreateTeam), command);
        }

        [HttpPut("{teamId}")]
        public async Task<IActionResult> UpdateTeam(string teamId, [FromBody] UpdateTeamCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await command.ExecuteAsync(teamId, _teamRepository);

            if (!result.IsSuccess)
                return result.IsNotFound ? NotFound() : BadRequest(result.ErrorMessage);

            return NoContent();
        }

        [HttpGet("GetTeamById/{teamId}")]
        public async Task<IActionResult> GetTeamById(string teamId)
        {
            var query = new GetTeamByIdQuery(_context) { TeamId = teamId };
            var teams = await query.ExecuteAsync();

            if (teams == null || teams.Count == 0)
                return NotFound("Nenhum time encontrado.");

            return Ok(teams);
        }

        [HttpGet("GetTeamsByLeagueId/{leagueId}")]
        public async Task<IActionResult> GetTeamsByLeagueId(string leagueId)
        {
            var query = new GetTeamsQuery(_context) { LeagueId = leagueId };
            var teams = await query.ExecuteAsync();

            if (teams == null || teams.Count == 0)
                return NotFound("Nenhum time encontrado.");

            return Ok(teams);
        }

    }
}