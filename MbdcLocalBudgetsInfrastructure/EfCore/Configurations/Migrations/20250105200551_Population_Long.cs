using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations.Migrations
{
    /// <inheritdoc />
    public partial class Population_Long : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Population",
                schema: "dwh",
                table: "Locality",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Population",
                schema: "dwh",
                table: "Locality",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
