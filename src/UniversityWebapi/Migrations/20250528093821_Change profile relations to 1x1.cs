using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityWebapi.Migrations
{
    /// <inheritdoc />
    public partial class Changeprofilerelationsto1x1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudentProfiles_StudentProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TeacherProfiles_TeacherProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeacherProfileId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TeacherProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_UserId",
                table: "TeacherProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_AspNetUsers_UserId",
                table: "StudentProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherProfiles_AspNetUsers_UserId",
                table: "TeacherProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_AspNetUsers_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherProfiles_AspNetUsers_UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_TeacherProfiles_UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentProfileId",
                table: "AspNetUsers",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeacherProfileId",
                table: "AspNetUsers",
                column: "TeacherProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudentProfiles_StudentProfileId",
                table: "AspNetUsers",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TeacherProfiles_TeacherProfileId",
                table: "AspNetUsers",
                column: "TeacherProfileId",
                principalTable: "TeacherProfiles",
                principalColumn: "Id");
        }
    }
}
