using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballStatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaguesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "leagues",
                schema: "dbo",
                columns: table => new
                {
                    leagueid = table.Column<string>(name: "league_id", type: "nvarchar(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leagues", x => x.leagueid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leagues",
                schema: "dbo");
        }
    }
}
