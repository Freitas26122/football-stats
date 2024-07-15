using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballStatsAPI.Models;

public class TeamsDbConfig : IEntityTypeConfiguration<Teams>
{
    public void Configure(EntityTypeBuilder<Teams> builder)
    {
        builder.ToTable("teams", schema: "dbo");
        builder.HasKey(t => t.TeamId);
        builder.Property(t => t.TeamId).IsRequired().HasMaxLength(36).HasColumnName("team_id");
        builder.Property(t => t.LeagueId).IsRequired().HasMaxLength(36).HasColumnName("league_id");
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(t => t.City).HasMaxLength(100).HasColumnName("city");
    }
}
