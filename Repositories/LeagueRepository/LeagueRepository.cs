using FootballStatsAPI.Models;

namespace FootballStatsAPI.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly FootballContext _context;

        public LeagueRepository(FootballContext context)
        {
            _context = context;
        }

        public async Task AddLeagueAsync(Leagues league)
        {
            await _context.Leagues.AddAsync(league);
            await _context.SaveChangesAsync();
        }

        public async Task<Leagues> GetLeagueByIdAsync(string leagueId)
        {
            return await _context.Leagues.FindAsync(leagueId);
        }
    }
}
