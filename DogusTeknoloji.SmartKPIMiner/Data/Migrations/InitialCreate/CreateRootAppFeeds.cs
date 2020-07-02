using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DogusTeknoloji.SmartKPIMiner.Data.Migrations.InitialCreate
{
    [DbContext(typeof(SmartKPIDbContext))]
    [Migration("5-CreateRootAppFeeds_Table")]
    public class CreateRootAppFeeds : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RootAppFeeds",
                schema: "dbo",
                columns: table => new
                {
                    FeedId = table.Column<long>(nullable: false)
                                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationName = table.Column<string>(nullable: false, type: "varchar(150)"),
                    CreateDate = table.Column<DateTime>(nullable: false, type: "datetime2", defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedId", x => x.FeedId);
                });
        }
    }
}