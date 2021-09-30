using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_AspNetUsers_authorID",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "authorID",
                table: "post",
                newName: "applicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_post_authorID",
                table: "post",
                newName: "IX_post_applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_post_AspNetUsers_applicationUserId",
                table: "post",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_AspNetUsers_applicationUserId",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "post",
                newName: "authorID");

            migrationBuilder.RenameIndex(
                name: "IX_post_applicationUserId",
                table: "post",
                newName: "IX_post_authorID");

            migrationBuilder.AddForeignKey(
                name: "FK_post_AspNetUsers_authorID",
                table: "post",
                column: "authorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
