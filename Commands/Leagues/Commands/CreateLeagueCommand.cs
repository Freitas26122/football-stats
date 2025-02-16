using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class CreateLeagueCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string Country { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Country);
    }

    public async Task<CommandResult> ExecuteAsync(ILeagueRepository? leagueRepository)
    {
        if (leagueRepository == null) throw new InvalidOperationException("LeagueRepository is not initialized.");

        var league = new Leagues
        {
            LeagueId = Guid.NewGuid().ToString().Substring(0, 8),
            Name = Name,
            Country = Country
        };

        try
        {
            await leagueRepository.AddLeagueAsync(league);
            RaiseCanExecuteChanged();
            return CommandResult.Success();
        }
        catch (RepositoryException ex)
        {
            return CommandResult.Failure($"Erro ao criar liga: {ex.Message}");
        }
        catch (Exception)
        {
            return CommandResult.Failure("Erro inesperado ao criar liga.");
        }
    }

    public async void Execute(object? parameter)
    {
        var leagueRepository = parameter as ILeagueRepository;
        await ExecuteAsync(leagueRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}