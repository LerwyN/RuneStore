using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUNE.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseDateToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Notes"
                ALTER COLUMN "ReleaseDate" TYPE date
                USING ("ReleaseDate" AT TIME ZONE 'UTC')::date;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Notes"
                ALTER COLUMN "ReleaseDate" TYPE timestamp with time zone
                USING ("ReleaseDate"::timestamp AT TIME ZONE 'UTC');
                """);
        }
    }
}
