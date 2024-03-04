using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infraestructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class requeridoqu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ParentreplyId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ParentreplyId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ParentreplyId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies",
                column: "UserId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "ParentreplyId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ParentreplyId",
                table: "Replies",
                column: "ParentreplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_UserId",
                table: "Replies",
                column: "UserId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ParentreplyId",
                table: "Replies",
                column: "ParentreplyId",
                principalTable: "Replies",
                principalColumn: "Id");
        }
    }
}
