using System.Windows.Input;
using FootballStatsAPI.Models;
using FootballStatsAPI.Repositories;

public class UpdateTeamCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public string Name { get; set; }
    public string LeagueId { get; set; }
    public string? City { get; set; }

    public bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(LeagueId);
    }

    public async Task<CommandResult> ExecuteAsync(string teamId, ITeamRepository? teamRepository)
    {
        if (teamRepository == null)
            throw new InvalidOperationException("Repositório do time não está inicializado.");

        var team = new Teams { TeamId = teamId, Name = Name, LeagueId = LeagueId, City = City };

        try
        {
            var existingTeam = await teamRepository.GetTeamByIdAsync(teamId);
            if (existingTeam == null)
                return CommandResult.NotFound();

            await teamRepository.UpdateTeamAsync(team);
            RaiseCanExecuteChanged();
            return CommandResult.Success();
        }
        catch (Exception ex)
        {
            return CommandResult.Failure($"Erro ao atualizar time: {ex.Message}");
        }
    }

    public async void Execute(object? parameter)
    {
        if (parameter is not (string teamId and not null))
            throw new ArgumentException("O parâmetro deve ser uma string representando o ID do time.");

        var teamRepository = parameter as ITeamRepository;
        await ExecuteAsync(teamId, teamRepository);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
