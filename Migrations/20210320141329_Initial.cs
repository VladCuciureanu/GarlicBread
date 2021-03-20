using GarlicBread.Persistence.Document;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarlicBread.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomizableRoles",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    UserId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizableRoles", x => new { x.GuildId, x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "GuildConfigurations",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Data = table.Column<GuildConfig>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildConfigurations", x => x.GuildId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomizableRoles");

            migrationBuilder.DropTable(
                name: "GuildConfigurations");
        }
    }
}
