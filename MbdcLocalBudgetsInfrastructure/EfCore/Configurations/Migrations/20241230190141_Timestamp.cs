using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations.Migrations
{
    /// <inheritdoc />
    public partial class Timestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                schema: "dwh",
                table: "OlapBudgetReport",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "dwh",
                table: "OlapBudgetReport");
        }
    }
}
