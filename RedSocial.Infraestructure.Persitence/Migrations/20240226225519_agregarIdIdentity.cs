using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocial.Infraestructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class agregarIdIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdIndentity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdIndentity",
                table: "Users");
        }
    }
}
