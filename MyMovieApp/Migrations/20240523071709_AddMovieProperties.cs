using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class AddMovieProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Runtime",
                table: "Movies",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CertificateId",
                table: "Movies",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies",
                column: "CertificateId",
                principalTable: "Certificate",
                principalColumn: "CertificateId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Certificate_CertificateId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CertificateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Movies");
        }
    }
}
