using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DndTracker.Migrations
{
    /// <inheritdoc />
    public partial class CountdownEncounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "countdown",
                table: "Encounters",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "countdown",
                table: "Encounters");
        }
    }
}
