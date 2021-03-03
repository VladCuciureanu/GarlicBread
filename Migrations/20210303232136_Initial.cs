using System;
using GarlicBread.Persistence.Document;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GarlicBread.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "GuildConfigurations",
                table => new
                {
                    GuildId = table.Column<decimal>("numeric(20,0)", nullable: false),
                    Data = table.Column<GuildConfig>("jsonb", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_GuildConfigurations", x => x.GuildId); });

            migrationBuilder.CreateTable(
                "Reminders",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuildId = table.Column<decimal>("numeric(20,0)", nullable: false),
                    ChannelId = table.Column<decimal>("numeric(20,0)", nullable: false),
                    MessageId = table.Column<decimal>("numeric(20,0)", nullable: false),
                    Text = table.Column<string>("character varying(100)", maxLength: 100, nullable: true),
                    CreatorId = table.Column<decimal>("numeric(20,0)", nullable: false),
                    DueAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Reminders", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GuildConfigurations");

            migrationBuilder.DropTable(
                "Reminders");
        }
    }
}