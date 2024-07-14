using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class CreateLeagueCommand : ICommand
{
    private readonly ILeagueRepository _leagueRepository;
    public event EventHandler CanExecuteChanged;

    public string LeagueId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public CreateLeagueCommand() { }

    public CreateLeagueCommand(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository ?? throw new ArgumentNullException(nameof(leagueRepository));
    }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Country);
    }

    public async void Execute(object? parameter)
    {
        await ExecuteAsync(parameter);
    }

    public async Task ExecuteAsync(object? parameter)
    {
        if (!CanExecute(parameter)) return;

        var league = new Leagues
        {
            LeagueId = Guid.NewGuid().ToString(),
            Name = Name,
            Country = Country
        };

        try
        {
            await _leagueRepository.AddLeagueAsync(league);
            LeagueId = league.LeagueId;
            RaiseCanExecuteChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar liga: {ex.Message}");
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
