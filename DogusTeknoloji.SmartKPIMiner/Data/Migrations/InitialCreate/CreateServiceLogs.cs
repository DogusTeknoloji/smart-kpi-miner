using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("7-CreateServiceLogs_Table")]
    public class CreateServiceLogs : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ServiceLogs",
               schema: "dbo",
               columns: table => new
               {
                   LogId = table.Column<long>(nullable: false)
                                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   LogShortName = table.Column<string>(nullable: false, type: "varchar(50)"),
                   LogDescription = table.Column<string>(nullable: false, type: "varchar(512)"),
                   CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_LogID", x => x.LogId);
               });
        }
    }
}
