using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class UpdateLeagueCommand : ICommand
{

    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string Country { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Country);
    }

    public async Task<CommandResult> ExecuteAsync(string leagueId, ILeagueRepository? leagueRepository)
    {
        if (leagueRepository == null) throw new InvalidOperationException("LeagueRepository is not initialized.");
        var league = new Leagues { LeagueId = leagueId, Name = Name, Country = Country };

        try
        {
            var existingLeague = await leagueRepository.GetLeagueByIdAsync(leagueId);
            if (existingLeague == null) return CommandResult.NotFound();

            await leagueRepository.UpdateLeagueAsync(league);
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
        if (parameter is not (string leagueId and not null))
            throw new ArgumentException("O parâmetro deve ser uma string representando o ID do time.");

        var leagueRepository = parameter as ILeagueRepository;
        await ExecuteAsync(leagueId, leagueRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}