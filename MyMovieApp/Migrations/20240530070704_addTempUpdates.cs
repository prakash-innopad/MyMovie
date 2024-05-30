using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class addTempUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "CertificateId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "CertificateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "CertificateId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "CertificateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
