using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class addShowDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenNumber",
                table: "MovieCinemas");

            migrationBuilder.DropColumn(
                name: "ShowTime",
                table: "MovieCinemas");

            migrationBuilder.CreateTable(
                name: "ShowDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieCinemaId = table.Column<int>(type: "int", nullable: false),
                    ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScreenNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowDetail_MovieCinemas_MovieCinemaId",
                        column: x => x.MovieCinemaId,
                        principalTable: "MovieCinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowDetail_MovieCinemaId",
                table: "ShowDetail",
                column: "MovieCinemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowDetail");

            migrationBuilder.AddColumn<int>(
                name: "ScreenNumber",
                table: "MovieCinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowTime",
                table: "MovieCinemas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
