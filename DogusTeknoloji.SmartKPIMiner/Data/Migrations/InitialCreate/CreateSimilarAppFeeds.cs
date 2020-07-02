using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("6-CreateSimilarAppFeeds_Table")]
    public class CreateSimilarAppFeeds : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimilarAppFeeds",
                schema: "dbo",
                columns: table => new
                {
                    SimilarAppId = table.Column<long>(nullable: false)
                                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: false, type: "varchar(150)"),
                    RootAppId = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarAppId", x => x.SimilarAppId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SimilarApps_RootApp_RootAppId",
                table: "SimilarAppFeeds",
                column: "RootAppId",
                principalTable: "RootAppFeeds",
                principalColumn: "FeedId");
        }
    }
}
