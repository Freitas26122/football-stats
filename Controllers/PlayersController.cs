using Microsoft.AspNetCore.Mvc;
using FootballStatsAPI.Repositories;
using FootballStatsAPI.Models;

namespace FootballStatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            var result = await command.ExecuteAsync(_playerRepository);

            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return CreatedAtAction(nameof(CreatePlayer), command);
        }

        [HttpPut("{playerId}")]
        public async Task<IActionResult> UpdatePlayer(string playerId, [FromBody] UpdatePlayerCommand command)
        {
            if (command == null) return BadRequest("Comando não pode ser nulo.");
            var result = await command.ExecuteAsync(playerId, _playerRepository);

            if (!result.IsSuccess) return result.IsNotFound ? NotFound() : BadRequest(result.ErrorMessage);
            return CreatedAtAction(nameof(UpdatePlayer), command);
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult<Players>> GetPlayerById(string playerId)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (player == null) return NotFound();

            return player;
        }
    }
}