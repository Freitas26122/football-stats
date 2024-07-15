using Microsoft.AspNetCore.Mvc;
using FootballStatsAPI.Repositories;
using FootballStatsAPI.Models;

namespace FootballStatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
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

        [HttpGet("{teamId}")]
        public async Task<ActionResult<Teams>> GetTeamById(string teamId)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team == null) return NotFound();

            return team;
        }
    }
}