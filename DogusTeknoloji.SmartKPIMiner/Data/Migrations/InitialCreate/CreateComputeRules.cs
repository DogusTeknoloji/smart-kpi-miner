using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("3-CreateComputeRules_Table")]
    public class CreateComputeRules : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComputeRules",
                schema: "dbo",
                columns: table => new
                {
                    ComputeRuleId = table.Column<long>(nullable: false)
                                     .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HttpSuccessCodes = table.Column<string>(nullable: false, type: "varchar(300)"),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleId", x => x.ComputeRuleId);
                });

            migrationBuilder.InsertData(table: "ComputeRules", columns: new[] { "HttpSuccessCodes" }, values: new object[] { "1,2,3,401" });
        }
    }
}
