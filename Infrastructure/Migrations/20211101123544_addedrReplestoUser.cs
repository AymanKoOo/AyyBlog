using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addedrReplestoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Reply",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reply_userId",
                table: "Reply",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_AspNetUsers_userId",
                table: "Reply",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reply_AspNetUsers_userId",
                table: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Reply_userId",
                table: "Reply");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Reply");
        }
    }
}
