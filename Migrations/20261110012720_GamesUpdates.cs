using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RUNE.Migrations
{
    /// <inheritdoc />
    public partial class GamesUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GamesId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTag",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTag", x => new { x.GameId, x.TagId });
                    table.ForeignKey(
                        name: "FK_GameTag_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTag_TagId",
                table: "GameTag",
                column: "TagId");

            migrationBuilder.Sql("""
                INSERT INTO "Genres" ("Name")
                SELECT DISTINCT BTRIM("Genre")
                FROM "Games"
                WHERE NULLIF(BTRIM("Genre"), '') IS NOT NULL;

                INSERT INTO "GameGenre" ("GamesId", "GenreId")
                SELECT game."Id", genre."Id"
                FROM "Games" AS game
                INNER JOIN "Genres" AS genre ON genre."Name" = BTRIM(game."Genre")
                WHERE NULLIF(BTRIM(game."Genre"), '') IS NOT NULL;
                """);

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE "Games" AS game
                SET "Genre" = COALESCE((
                    SELECT STRING_AGG(genre."Name", ', ' ORDER BY genre."Name")
                    FROM "GameGenre" AS gameGenre
                    INNER JOIN "Genres" AS genre ON genre."Id" = gameGenre."GenreId"
                    WHERE gameGenre."GamesId" = game."Id"
                ), '');
                """);

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GameTag");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Tags");

        }
    }
}
