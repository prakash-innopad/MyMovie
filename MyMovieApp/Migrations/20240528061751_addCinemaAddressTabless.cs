using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class addCinemaAddressTabless : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCinema_Cinema_CinemaId",
                table: "MovieCinema");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCinema_Movies_MovieId",
                table: "MovieCinema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCinema",
                table: "MovieCinema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cinema",
                table: "Cinema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "MovieCinema",
                newName: "MovieCinemas");

            migrationBuilder.RenameTable(
                name: "Cinema",
                newName: "Cinemas");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCinema_MovieId",
                table: "MovieCinemas",
                newName: "IX_MovieCinemas_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCinema_CinemaId",
                table: "MovieCinemas",
                newName: "IX_MovieCinemas_CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Cinema_AddressId",
                table: "Cinemas",
                newName: "IX_Cinemas_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCinemas",
                table: "MovieCinemas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas",
                column: "CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Addresses_AddressId",
                table: "Cinemas",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCinemas_Cinemas_CinemaId",
                table: "MovieCinemas",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "CinemaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCinemas_Movies_MovieId",
                table: "MovieCinemas",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Addresses_AddressId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCinemas_Cinemas_CinemaId",
                table: "MovieCinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCinemas_Movies_MovieId",
                table: "MovieCinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCinemas",
                table: "MovieCinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "MovieCinemas",
                newName: "MovieCinema");

            migrationBuilder.RenameTable(
                name: "Cinemas",
                newName: "Cinema");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCinemas_MovieId",
                table: "MovieCinema",
                newName: "IX_MovieCinema_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCinemas_CinemaId",
                table: "MovieCinema",
                newName: "IX_MovieCinema_CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Cinemas_AddressId",
                table: "Cinema",
                newName: "IX_Cinema_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCinema",
                table: "MovieCinema",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cinema",
                table: "Cinema",
                column: "CinemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_Address_AddressId",
                table: "Cinema",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCinema_Cinema_CinemaId",
                table: "MovieCinema",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "CinemaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCinema_Movies_MovieId",
                table: "MovieCinema",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
