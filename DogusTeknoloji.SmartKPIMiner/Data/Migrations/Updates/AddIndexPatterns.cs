using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.Updates
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("B1-AddIndexPattern_SearchIndices")]
    public class AddIndexPatterns : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndexPattern",
                schema: "dbo",
                table: "SearchIndices",
                nullable: false,
                defaultValue: "*",
                defaultValueSql: "'*'");

            migrationBuilder.Sql(
                sql: "UPDATE SearchIndices SET IndexName = SUBSTRING(IndexName, 0, LEN(IndexName) - CHARINDEX('-', REVERSE(IndexName) ) + 1 )",
                suppressTransaction: false);
        }
    }
}