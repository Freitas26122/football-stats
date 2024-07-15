using FootballStatsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStatsAPI.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FootballContext _context;

        public PlayerRepository(FootballContext context)
        {
            _context = context;
        }

        public async Task AddPlayerAsync(Players player)
        {
            var existingTeam = await _context.Teams.FindAsync(player.TeamId);
            if (existingTeam == null)
                throw new KeyNotFoundException("Time não encontrada.");

            var existsDuplicatePlayer = await _context.Players
                .AsNoTracking()
                .AnyAsync(l => l.Name == player.Name);

            if (existsDuplicatePlayer)
                throw new RepositoryException("Já existe um jogador com esse nome.", null);

            try
            {
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Erro ao adicionar jogador.", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro inesperado ao adicionar jogador.", ex);
            }
        }

        public async Task<Players> GetPlayerByIdAsync(string playerId)
        {
            var player = await _context.Players.FindAsync(playerId);
            if (player == null) throw new KeyNotFoundException("Jogador não encontrada.");

            return player;
        }

        public async Task UpdatePlayerAsync(Players player)
        {
            var existingPlayer = await _context.Players.FindAsync(player.PlayerId);
            if (existingPlayer == null)
                throw new KeyNotFoundException("Jogador não encontrada.");

            var existingTeam = await _context.Teams.FindAsync(player.TeamId);
            if (existingTeam == null)
                throw new KeyNotFoundException("Time não encontrada.");

            var existsDuplicatePlayer = await _context.Players
                            .AsNoTracking()
                            .AnyAsync(p => p.Name == player.Name && p.PlayerId != player.PlayerId);

            if (existsDuplicatePlayer)
                throw new RepositoryException("Já existe um jogador com esse nome.", null);

            existingPlayer.Name = player.Name;
            existingPlayer.Position = player.Position;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Erro ao atualizar jogador.", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Erro inesperado ao atualizar jogador.", ex);
            }
        }
    }
}
