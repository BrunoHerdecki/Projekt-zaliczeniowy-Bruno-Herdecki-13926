using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Migrations
{
    /// <inheritdoc />
    public partial class change_return_date_to_expected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "HistoryOfReservation",
                newName: "ExpectedReturnDate");

            migrationBuilder.AddColumn<bool>(
                name: "HasReturned",
                table: "HistoryOfReservation",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasReturned",
                table: "HistoryOfReservation");

            migrationBuilder.RenameColumn(
                name: "ExpectedReturnDate",
                table: "HistoryOfReservation",
                newName: "ReturnDate");
        }
    }
}
