using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.Updates
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("B2-CreateKPIMetricTimers_Table")]
    public class CreateKPIMetricTimers : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KPIMetricTimers",
                schema: "dbo",
                columns: table => new
                {
                    MetricTimerId = table.Column<long>(nullable: false)
                                         .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexId = table.Column<long>(nullable: false, type: "bigint"),
                    RowModifyDateLog = table.Column<DateTime>(nullable: true, type: "datetime2", defaultValueSql: "getdate()"),
                    LastInsertDate = table.Column<DateTime>(nullable: false, type: "datetime2")
                },
                constraints: tbl =>
                {
                    tbl.PrimaryKey("PK_MetricTimerId", x => x.MetricTimerId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_KPI_MTs_Index_IndexId",
                table: "KPIMetricTimers",
                column: "IndexId",
                principalTable: "SearchIndices",
                principalColumn: "IndexId");

            migrationBuilder.Sql(
                sql: "INSERT INTO dbo.KPIMetricTimers (LastInsertDate,IndexId) SELECT MAX(LogDate) AS LastInsertDate, IndexId FROM dbo.KPIMetrics GROUP BY IndexId", 
                suppressTransaction: false);
        }
    }
}