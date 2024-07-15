using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class UpdatePlayerCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string TeamId { get; set; }
    public string? Position { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TeamId);
    }

    public async Task<CommandResult> ExecuteAsync(string playerId, IPlayerRepository? playerRepository)
    {
        if (playerRepository == null)
            throw new InvalidOperationException("Repositório do jogador não está inicializado.");

        var player = new Players { PlayerId = playerId, Name = Name, TeamId = TeamId, Position = Position };

        try
        {
            var existingPlayer = await playerRepository.GetPlayerByIdAsync(playerId);
            if (existingPlayer == null)
                return CommandResult.NotFound();

            await playerRepository.UpdatePlayerAsync(player);
            RaiseCanExecuteChanged();
            return CommandResult.Success();
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Erro ao atualizar jogador: {ex.Message}");
        }
    }

    public async void Execute(object? parameter)
    {
        if (parameter is not (string playerId and not null))
            throw new ArgumentException("O parâmetro deve ser uma string representando o ID do jogador.");

        var playerRepository = parameter as IPlayerRepository;
        await ExecuteAsync(playerId, playerRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
