using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("4-CreateKPIMetrics_Table")]
    public class CreateKPIMetrics : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KPIMetrics",
                schema: "dbo",
                columns: table => new
                {
                    MetricId = table.Column<long>(nullable: false)
                                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerName = table.Column<string>(nullable: false, type: "varchar(50)"),
                    SiteName = table.Column<string>(nullable: false, type: "varchar(250)"),
                    PageUrl = table.Column<string>(nullable: false, type: "varchar(512)"),
                    TotalRequestCount = table.Column<int>(nullable: false),
                    SuccessRequestCount = table.Column<int>(nullable: false),
                    FailedRequestCount = table.Column<int>(nullable: false),
                    AverageResponseTime = table.Column<double>(nullable: false),
                    SuccessAverageResponseTime = table.Column<double>(nullable: false),
                    FailedAverageResponseTime = table.Column<double>(nullable: false),
                    SuccessPercentage = table.Column<double>(nullable: false),
                    FailedPercentage = table.Column<double>(nullable: false),
                    LogDate = table.Column<DateTime>(nullable: false, type: "datetime2"),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()"),
                    IndexId = table.Column<long>(nullable: false),
                    ComputeRuleId = table.Column<long>(nullable: false)
                },
                constraints: tbl =>
                {
                    tbl.PrimaryKey("PK_MetricId", x => x.MetricId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Index_IndexId",
                table: "KPIMetrics",
                column: "IndexId",
                principalTable: "SearchIndices",
                principalColumn: "IndexId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_ComputeRule_ComputeRuleId",
                table: "KPIMetrics",
                column: "ComputeRuleId",
                principalTable: "ComputeRules",
                principalColumn: "ComputeRuleId");
        }
    }
}
