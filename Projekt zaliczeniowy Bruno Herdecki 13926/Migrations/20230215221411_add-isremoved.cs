using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Migrations
{
    /// <inheritdoc />
    public partial class addisremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Users");
        }
    }
}
