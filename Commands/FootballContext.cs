using Microsoft.EntityFrameworkCore;
using FootballStatsAPI.Models;

public class FootballContext : DbContext
{
    public DbSet<Leagues> Leagues { get; set; }
    public DbSet<Teams> Teams { get; set; }
    public DbSet<Players> Players { get; set; }
    public List<LogChangedEntry> LogChangedEntries { get; set; }

    public FootballContext(DbContextOptions<FootballContext> options) : base(options)
    {
        LogChangedEntries = new List<LogChangedEntry>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new LeagueDbConfig());
        builder.ApplyConfiguration(new TeamsDbConfig());
        builder.ApplyConfiguration(new PlayersDbConfig());
    }
}
