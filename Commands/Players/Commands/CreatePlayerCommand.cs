using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class CreatePlayerCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string TeamId { get; set; }
    public string? Position { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TeamId);
    }

    public async Task<CommandResult> ExecuteAsync(IPlayerRepository? playerRepository)
    {
        if (playerRepository == null) throw new InvalidOperationException("PlayerRepository is not initialized.");
        var player = new Players
        {
            PlayerId = Guid.NewGuid().ToString().Substring(0, 8),
            TeamId = TeamId,
            Name = Name,
            Position = Position
        };

        try
        {
            await playerRepository.AddPlayerAsync(player);
            RaiseCanExecuteChanged();
            return CommandResult.Success();
        }
        catch (RepositoryException ex)
        {
            return CommandResult.Failure($"Erro ao criar jogador: {ex.Message}");
        }
        catch (Exception)
        {
            return CommandResult.Failure("Erro inesperado ao criar jogador.");
        }
    }

    public async void Execute(object? parameter)
    {
        var playerRepository = parameter as IPlayerRepository;
        await ExecuteAsync(playerRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
