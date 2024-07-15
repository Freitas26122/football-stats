using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class CreateTeamCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string LeagueId { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(LeagueId);
    }

    public async Task<CommandResult> ExecuteAsync(ITeamRepository? teamRepository)
    {
        if (teamRepository == null) throw new InvalidOperationException("TeamRepository is not initialized.");
        var team = new Teams
        {
            TeamId = Guid.NewGuid().ToString().Substring(0, 8),
            LeagueId = LeagueId,
            Name = Name,
        };

        try
        {
            await teamRepository.AddTeamAsync(team);
            RaiseCanExecuteChanged();
            return CommandResult.Success();
        }
        catch (RepositoryException ex)
        {
            return CommandResult.Failure($"Erro ao criar time: {ex.Message}");
        }
        catch (Exception)
        {
            return CommandResult.Failure("Erro inesperado ao criar time.");
        }
    }

    public async void Execute(object? parameter)
    {
        var teamRepository = parameter as ITeamRepository;
        await ExecuteAsync(teamRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
