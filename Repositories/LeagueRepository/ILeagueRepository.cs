using FootballStatsAPI.Models;

namespace FootballStatsAPI.Repositories
{
    public interface ILeagueRepository
    {
        Task AddLeagueAsync(Leagues league);
        Task<Leagues> GetLeagueByIdAsync(string leagueId);
    }
}
