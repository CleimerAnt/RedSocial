using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infraestructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class adddriend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendFirstName",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FriendImgUrl",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FriendLastName",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FriendUserName",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendFirstName",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendImgUrl",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendLastName",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendUserName",
                table: "Friends");
        }
    }
}
