using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraduationProjectBMS.Migrations
{
    /// <inheritdoc />
    public partial class articleIdColumnInReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ArticleId",
                table: "Replies",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Articles_ArticleId",
                table: "Replies",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Articles_ArticleId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ArticleId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Replies");

        }
    }
}
