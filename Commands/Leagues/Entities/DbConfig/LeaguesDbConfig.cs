using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballStatsAPI.Models;

public class LeagueDbConfig : IEntityTypeConfiguration<Leagues>
{
    public void Configure(EntityTypeBuilder<Leagues> builder)
    {
        builder.ToTable("leagues", schema: "dbo");
        builder.HasKey(l => l.LeagueId);
        builder.Property(l => l.LeagueId).IsRequired().HasMaxLength(36).HasColumnName("league_id");
        builder.Property(l => l.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(l => l.Country).IsRequired().HasMaxLength(100).HasColumnName("country");
    }
}
