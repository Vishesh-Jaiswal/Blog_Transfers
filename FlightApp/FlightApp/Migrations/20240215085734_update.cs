using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightApp.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bunisness",
                table: "Flights",
                newName: "FirstClass");

            migrationBuilder.RenameColumn(
                name: "FristClass",
                table: "Flights",
                newName: "Business");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstClass",
                table: "Flights",
                newName: "bunisness");

            migrationBuilder.RenameColumn(
                name: "Business",
                table: "Flights",
                newName: "FristClass");
        }
    }
}
