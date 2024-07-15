using FootballStatsAPI.Models;

namespace FootballStatsAPI.Repositories
{
    public interface ITeamRepository
    {
        Task AddTeamAsync(Teams team);
        Task<Teams> GetTeamByIdAsync(string teamId);
        Task UpdateTeamAsync(Teams team);
    }
}