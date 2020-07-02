using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("2-CreateSearchIndices_Table")]
    public class CreateSearchIndices : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchIndices",
                schema: "dbo",
                columns: table => new
                {
                    IndexId = table.Column<long>(nullable: false)
                                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexName = table.Column<string>(nullable: false, type: "varchar(50)"),
                    UrlAddress = table.Column<string>(nullable: false, type: "varchar(250)"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexId", x => x.IndexId);
                });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-dogusotomotiv-*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-suit-*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-ortakprojeler-*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-websites-*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-dmarin*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "iis-dms-*", "10.115.207.72" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "logstash-iislogs-*", "10.112.116.87" });

            migrationBuilder.InsertData(
                table: "SearchIndices",
                columns: new[] { "IndexName", "UrlAddress" },
                values: new[] { "vdfservices-prod-*", "10.112.116.87" });
        }
    }
}
