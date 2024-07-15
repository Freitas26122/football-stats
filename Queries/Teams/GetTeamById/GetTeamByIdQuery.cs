using Dapper;
using Microsoft.EntityFrameworkCore;

namespace FootballStatsAPI.Queries
{
    public class GetTeamByIdQuery
    {
        private readonly FootballContext _context;
        public string TeamId { get; set; }

        public GetTeamByIdQuery(FootballContext context)
        {
            _context = context;
        }

        public async Task<List<TeamByIdViewModel>> ExecuteAsync()
        {
            const string sql = @"
            SELECT
                [t].[team_id] as [TeamId],
                [t].[Name],
                [t].[league_id] as [LeagueId]
            FROM [dbo].[Teams] [t] WITH(NOLOCK)
            WHERE [t].[team_id] = @TeamId 
            ";

            using (var connection = _context.Database.GetDbConnection())
            {
                var teams = await connection.QueryAsync<TeamByIdViewModel>(sql, new { TeamId });
                return teams.AsList();
            }
        }
    }
}