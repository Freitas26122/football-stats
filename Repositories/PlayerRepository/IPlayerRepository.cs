using FootballStatsAPI.Models;

namespace FootballStatsAPI.Repositories
{
    public interface IPlayerRepository
    {
        Task AddPlayerAsync(Players player);
        Task<Players> GetPlayerByIdAsync(string playerId);
        Task UpdatePlayerAsync(Players player);
    }
}