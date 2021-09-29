using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class editmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_post_CategoryId",
                table: "post",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_post_categories_CategoryId",
                table: "post",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_categories_CategoryId",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_post_CategoryId",
                table: "post");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "post");

            migrationBuilder.CreateTable(
                name: "post_Categories",
                columns: table => new
                {
                    postId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_Categories", x => new { x.postId, x.categoryId });
                    table.ForeignKey(
                        name: "FK_post_Categories_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_Categories_post_postId",
                        column: x => x.postId,
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_Categories_categoryId",
                table: "post_Categories",
                column: "categoryId");
        }
    }
}
