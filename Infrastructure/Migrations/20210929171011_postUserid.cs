using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class postUserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_AspNetUsers_applicationUserId",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_post_applicationUserId",
                table: "post");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "post");

            migrationBuilder.AlterColumn<string>(
                name: "authorID",
                table: "post",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_authorID",
                table: "post",
                column: "authorID");

            migrationBuilder.AddForeignKey(
                name: "FK_post_AspNetUsers_authorID",
                table: "post",
                column: "authorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_AspNetUsers_authorID",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_post_authorID",
                table: "post");

            migrationBuilder.AlterColumn<string>(
                name: "authorID",
                table: "post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "post",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_applicationUserId",
                table: "post",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_post_AspNetUsers_applicationUserId",
                table: "post",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
