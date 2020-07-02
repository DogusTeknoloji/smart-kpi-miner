using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("1-CreateExcludedFileFormats_Table")]
    public class CreateExcludedFileFormats : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcludedFileFormats",
                schema: "dbo",
                columns: table => new
                {
                    FormatId = table.Column<long>(nullable: false)
                                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormatExtension = table.Column<string>(nullable: false, type: "varchar(10)"),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatId", x => x.FormatId);
                });

            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "jpg");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "img");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "js");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "jpeg");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "json");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "pdf");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "png");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "css");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "gif");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "ttf");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "woff");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "woff2");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "txt");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "csv");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "mp4");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "avi");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "xml");
            migrationBuilder.InsertData(table: "ExcludedFileFormats", column: "FormatExtension", value: "xslt");
        }
    }
}
