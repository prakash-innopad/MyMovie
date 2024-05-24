using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class AddFieldsToMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisLikes",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrailerLink",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisLikes",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TrailerLink",
                table: "Movies");
        }
    }
}
