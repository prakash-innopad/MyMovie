using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class AddImageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Movies",
                newName: "ImageLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Movies",
                newName: "Genre");
        }
    }
}
