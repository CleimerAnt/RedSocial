using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infraestructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class requeridoqasduads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_UserId",
                table: "Replies");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_comentId",
                table: "Replies",
                column: "comentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_comentId",
                table: "Replies",
                column: "comentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_comentId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_comentId",
                table: "Replies");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_UserId",
                table: "Replies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies",
                column: "UserId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
