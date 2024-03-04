using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infraestructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class requeridoqasdu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies",
                column: "UserId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies",
                column: "UserId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
