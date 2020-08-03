using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webApi.Migrations
{
    public partial class Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventDetailStatuses",
                columns: table => new
                {
                    EventDetailStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDetailStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetailStatuses", x => x.EventDetailStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventName = table.Column<string>(nullable: true),
                    EventNumber = table.Column<int>(nullable: false),
                    eventDateTime = table.Column<DateTime>(nullable: false),
                    eventEndDateTime = table.Column<DateTime>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    EventDetailStatusId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventDetailId = table.Column<int>(nullable: false),
                    EventDetailName = table.Column<string>(nullable: true),
                    EventDetailNumber = table.Column<int>(nullable: false),
                    EventDetailOdd = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FinishingPosition = table.Column<int>(nullable: false),
                    FirstTimer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => new { x.EventId, x.EventDetailStatusId });
                    table.ForeignKey(
                        name: "FK_EventDetails_EventDetailStatuses_EventDetailStatusId",
                        column: x => x.EventDetailStatusId,
                        principalTable: "EventDetailStatuses",
                        principalColumn: "EventDetailStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDetails_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventDetails_EventDetailStatusId",
                table: "EventDetails",
                column: "EventDetailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TournamentId",
                table: "Events",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "EventDetailStatuses");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
