using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class AddCastsToMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    CastId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.CastId);
                });

            migrationBuilder.CreateTable(
                name: "CastMovie",
                columns: table => new
                {
                    CastsCastId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMovie", x => new { x.CastsCastId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CastMovie_Cast_CastsCastId",
                        column: x => x.CastsCastId,
                        principalTable: "Cast",
                        principalColumn: "CastId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMovie_MoviesId",
                table: "CastMovie",
                column: "MoviesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMovie");

            migrationBuilder.DropTable(
                name: "Cast");
        }
    }
}
