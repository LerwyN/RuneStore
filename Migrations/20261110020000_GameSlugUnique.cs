using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUNE.Migrations
{
    /// <inheritdoc />
    public partial class GameSlugUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    duplicate_game RECORD;
                    candidate_slug TEXT;
                    suffix INTEGER;
                BEGIN
                    FOR duplicate_game IN
                        SELECT game."Id", game."Slug"
                        FROM "Games" AS game
                        WHERE game."Slug" IS NOT NULL
                          AND EXISTS (
                              SELECT 1
                              FROM "Games" AS earlier_game
                              WHERE earlier_game."Slug" = game."Slug"
                                AND earlier_game."Id" < game."Id"
                          )
                        ORDER BY game."Id"
                    LOOP
                        candidate_slug := duplicate_game."Slug" || '-' || duplicate_game."Id"::text;
                        suffix := 2;

                        WHILE EXISTS (
                            SELECT 1
                            FROM "Games"
                            WHERE "Slug" = candidate_slug
                              AND "Id" <> duplicate_game."Id"
                        ) LOOP
                            candidate_slug := duplicate_game."Slug" || '-' || duplicate_game."Id"::text || '-' || suffix::text;
                            suffix := suffix + 1;
                        END LOOP;

                        UPDATE "Games"
                        SET "Slug" = candidate_slug
                        WHERE "Id" = duplicate_game."Id";
                    END LOOP;
                END;
                $$;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Slug",
                table: "Games",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_Slug",
                table: "Games");
        }
    }
}
