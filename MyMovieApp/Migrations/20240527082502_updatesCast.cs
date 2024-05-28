using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovieApp.Migrations
{
    public partial class updatesCast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMovie_Cast_CastsCastId",
                table: "CastMovie");

            migrationBuilder.RenameColumn(
                name: "CastsCastId",
                table: "CastMovie",
                newName: "CastsId");

            migrationBuilder.RenameColumn(
                name: "CastId",
                table: "Cast",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMovie_Cast_CastsId",
                table: "CastMovie",
                column: "CastsId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMovie_Cast_CastsId",
                table: "CastMovie");

            migrationBuilder.RenameColumn(
                name: "CastsId",
                table: "CastMovie",
                newName: "CastsCastId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cast",
                newName: "CastId");

            migrationBuilder.AddForeignKey(
                name: "FK_CastMovie_Cast_CastsCastId",
                table: "CastMovie",
                column: "CastsCastId",
                principalTable: "Cast",
                principalColumn: "CastId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
