using FootballStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            var existsDuplicateLeague = await _context.Leagues
                .AsNoTracking()
                .AnyAsync(l => l.Name == league.Name);

            if (existsDuplicateLeague)
                throw new RepositoryException("Já existe uma liga com esse nome.", null);

            try
            {
                await _context.Leagues.AddAsync(league);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Erro ao adicionar a liga.", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro inesperado ao adicionar a liga.", ex);
            }
        }

        public async Task<Leagues> GetLeagueByIdAsync(string leagueId)
        {
            var league = await _context.Leagues.FindAsync(leagueId);
            if (league == null) throw new KeyNotFoundException("Liga não encontrada.");

            return league;
        }

        public async Task UpdateLeagueAsync(Leagues league)
        {
            var existingLeague = await _context.Leagues.FindAsync(league.LeagueId);
            if (existingLeague == null)
                throw new KeyNotFoundException("Liga não encontrada.");

            var existsDuplicateLeague = await _context.Leagues
                            .AsNoTracking()
                            .AnyAsync(l => l.Name == league.Name && l.LeagueId != league.LeagueId);

            if (existsDuplicateLeague)
                throw new RepositoryException("Já existe uma liga com esse nome.", null);

            existingLeague.Name = league.Name;
            existingLeague.Country = league.Country;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Erro ao atualizar a liga.", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro inesperado ao atualizar a liga.", ex);
            }
        }
    }
}
