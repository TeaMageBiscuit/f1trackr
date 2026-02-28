using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1Trackr.Core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Constructors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LogoAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructors", x => new { x.Id, x.Season });
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    GivenName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FamilyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    DriverNumber = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    ImageAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => new { x.Id, x.Season });
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Season = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaceResults",
                columns: table => new
                {
                    Season = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SprintDriverResults = table.Column<string>(type: "text", nullable: false),
                    QualifyingResults = table.Column<string>(type: "text", nullable: false),
                    DriverResults = table.Column<string>(type: "text", nullable: false),
                    ConstructorStandingsSnapshot = table.Column<string>(type: "text", nullable: false),
                    DriverStandingsSnapshot = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceResults", x => new { x.Season, x.Round });
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Season = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Circuit = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GrandPrixTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FirstPracticeTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SecondPracticeTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ThirdPracticeTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    QualifyingTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SprintQualifyingTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    SprintTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => new { x.Season, x.Round });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccessCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentScore = table.Column<int>(type: "integer", nullable: false),
                    ScoreSnapshots = table.Column<string>(type: "text", nullable: false),
                    ConstructorPredictions = table.Column<string>(type: "text", nullable: false),
                    DriverPredictions = table.Column<string>(type: "text", nullable: false),
                    DriverRacePredictions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserId",
                table: "GroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name_Season",
                table: "Groups",
                columns: new[] { "Name", "Season" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constructors");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "RaceResults");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
