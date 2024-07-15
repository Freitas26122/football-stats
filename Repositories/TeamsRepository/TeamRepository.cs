using FootballStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStatsAPI.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FootballContext _context;

        public TeamRepository(FootballContext context)
        {
            _context = context;
        }

        public async Task AddTeamAsync(Teams team)
        {
            var existingLeague = await _context.Leagues.FindAsync(team.LeagueId);
            if (existingLeague == null)
                throw new KeyNotFoundException("Liga não encontrada.");

            var existsDuplicateTeam = await _context.Teams
                .AsNoTracking()
                .AnyAsync(l => l.Name == team.Name);

            if (existsDuplicateTeam)
                throw new RepositoryException("Já existe um time com esse nome.", null);

            try
            {
                await _context.Teams.AddAsync(team);
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

        public async Task<Teams> GetTeamByIdAsync(string teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null) throw new KeyNotFoundException("Time não encontrado.");

            return team;
        }

        public async Task UpdateTeamAsync(Teams team)
        {
            var existingLeague = await _context.Leagues.FindAsync(team.LeagueId);
            if (existingLeague == null)
                throw new KeyNotFoundException("Liga não encontrada.");

            var existingTeam = await _context.Teams.FindAsync(team.TeamId);
            if (existingTeam == null)
                throw new KeyNotFoundException("Time não encontrado.");

            var existsDuplicateTeam = await _context.Teams
                            .AsNoTracking()
                            .AnyAsync(l => l.Name == team.Name && l.TeamId != team.TeamId);

            if (existsDuplicateTeam)
                throw new RepositoryException("Já existe um time com esse nome.", null);


            existingTeam.Name = team.Name;
            existingTeam.LeagueId = team.LeagueId;

            await _context.SaveChangesAsync();
        }
    }
}
