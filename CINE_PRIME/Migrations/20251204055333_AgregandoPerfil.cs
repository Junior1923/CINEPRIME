using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CINE_PRIME.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenPerfil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenPerfil",
                table: "AspNetUsers");
        }
    }
}
