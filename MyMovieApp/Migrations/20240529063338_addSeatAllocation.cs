using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class addSeatAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowDetail_MovieCinemas_MovieCinemaId",
                table: "ShowDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowDetail",
                table: "ShowDetail");

            migrationBuilder.RenameTable(
                name: "ShowDetail",
                newName: "ShowDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ShowDetail_MovieCinemaId",
                table: "ShowDetails",
                newName: "IX_ShowDetails_MovieCinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowDetails",
                table: "ShowDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SeatAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowDetailId = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatAllocations_ShowDetails_ShowDetailId",
                        column: x => x.ShowDetailId,
                        principalTable: "ShowDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatAllocations_ShowDetailId",
                table: "SeatAllocations",
                column: "ShowDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowDetails_MovieCinemas_MovieCinemaId",
                table: "ShowDetails",
                column: "MovieCinemaId",
                principalTable: "MovieCinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowDetails_MovieCinemas_MovieCinemaId",
                table: "ShowDetails");

            migrationBuilder.DropTable(
                name: "SeatAllocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowDetails",
                table: "ShowDetails");

            migrationBuilder.RenameTable(
                name: "ShowDetails",
                newName: "ShowDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ShowDetails_MovieCinemaId",
                table: "ShowDetail",
                newName: "IX_ShowDetail_MovieCinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowDetail",
                table: "ShowDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowDetail_MovieCinemas_MovieCinemaId",
                table: "ShowDetail",
                column: "MovieCinemaId",
                principalTable: "MovieCinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
