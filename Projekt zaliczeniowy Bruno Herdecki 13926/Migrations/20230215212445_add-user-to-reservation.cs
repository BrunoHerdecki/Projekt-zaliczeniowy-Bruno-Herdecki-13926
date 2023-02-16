using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Migrations
{
    /// <inheritdoc />
    public partial class addusertoreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HistoryOfReservation",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOfReservation_UserId",
                table: "HistoryOfReservation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryOfReservation_Users_UserId",
                table: "HistoryOfReservation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryOfReservation_Users_UserId",
                table: "HistoryOfReservation");

            migrationBuilder.DropIndex(
                name: "IX_HistoryOfReservation_UserId",
                table: "HistoryOfReservation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryOfReservation");
        }
    }
}
