using Dapper;
using Microsoft.EntityFrameworkCore;

namespace FootballStatsAPI.Queries
{
    public class GetTeamsQuery
    {
        private readonly FootballContext _context;
        public string LeagueId { get; set; }

        public GetTeamsQuery(FootballContext context)
        {
            _context = context;
        }

        public async Task<List<TeamViewModel>> ExecuteAsync()
        {
            const string sql = @"
            SELECT
                [t].[team_id] as [TeamId],
                [t].[Name],
                [t].[league_id] as [LeagueId]
            FROM [dbo].[Teams] [t] WITH(NOLOCK)
            WHERE [t].[league_id] = @LeagueId 
            ";

            using (var connection = _context.Database.GetDbConnection())
            {
                var teams = await connection.QueryAsync<TeamViewModel>(sql, new { LeagueId });
                return teams.AsList();
            }
        }
    }
}