using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CINE_PRIME.Migrations
{
    /// <inheritdoc />
    public partial class Nuevas_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ListasPendientes_UserId_TmdbMovieId",
                table: "ListasPendientes");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UserId_TmdbMovieId",
                table: "Favoritos");

            migrationBuilder.RenameColumn(
                name: "TmdbMovieId",
                table: "ListasPendientes",
                newName: "MediaId");

            migrationBuilder.RenameColumn(
                name: "TmdbMovieId",
                table: "Favoritos",
                newName: "MediaId");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "ListasPendientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Favoritos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPendientes_UserId_MediaId_MediaType",
                table: "ListasPendientes",
                columns: new[] { "UserId", "MediaId", "MediaType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UserId_MediaId_MediaType",
                table: "Favoritos",
                columns: new[] { "UserId", "MediaId", "MediaType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ListasPendientes_UserId_MediaId_MediaType",
                table: "ListasPendientes");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UserId_MediaId_MediaType",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "ListasPendientes");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Favoritos");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "ListasPendientes",
                newName: "TmdbMovieId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Favoritos",
                newName: "TmdbMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPendientes_UserId_TmdbMovieId",
                table: "ListasPendientes",
                columns: new[] { "UserId", "TmdbMovieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UserId_TmdbMovieId",
                table: "Favoritos",
                columns: new[] { "UserId", "TmdbMovieId" },
                unique: true);
        }
    }
}
