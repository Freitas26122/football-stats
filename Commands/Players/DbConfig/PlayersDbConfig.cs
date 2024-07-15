using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballStatsAPI.Models;

public class PlayersDbConfig : IEntityTypeConfiguration<Players>
{
    public void Configure(EntityTypeBuilder<Players> builder)
    {
        builder.ToTable("players", schema: "dbo");
        builder.HasKey(t => t.PlayerId);
        builder.Property(t => t.PlayerId).IsRequired().HasMaxLength(36).HasColumnName("player_id");
        builder.Property(t => t.TeamId).IsRequired().HasMaxLength(36).HasColumnName("team_id");
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(t => t.Position).HasMaxLength(100).HasColumnName("position");
    }
}
