using Microsoft.EntityFrameworkCore.Migrations;

namespace webApi.Migrations
{
    public partial class UserTournamentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_userId",
                table: "Tournaments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_userId",
                table: "Tournaments",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_userId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_userId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Tournaments");
        }
    }
}
