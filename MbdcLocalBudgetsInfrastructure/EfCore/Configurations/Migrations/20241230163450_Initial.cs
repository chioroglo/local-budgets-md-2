using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dwh");

            migrationBuilder.CreateTable(
                name: "District",
                schema: "dwh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Year",
                schema: "dwh",
                columns: table => new
                {
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.Value);
                    table.CheckConstraint("CK_Year_Epoch", "[Value] >= 1970");
                });

            migrationBuilder.CreateTable(
                name: "Locality",
                schema: "dwh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ParentDistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locality_District_ParentDistrictId",
                        column: x => x.ParentDistrictId,
                        principalSchema: "dwh",
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OlapBudgetReport",
                schema: "dwh",
                columns: table => new
                {
                    YearId = table.Column<int>(type: "int", nullable: false),
                    LocalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlannedExpenses = table.Column<long>(type: "bigint", nullable: false),
                    ActualExpenses = table.Column<long>(type: "bigint", nullable: false),
                    PlannedIncome = table.Column<long>(type: "bigint", nullable: false),
                    ActualIncome = table.Column<long>(type: "bigint", nullable: false),
                    ExpensesDetailsSalariesPaid = table.Column<long>(type: "bigint", nullable: false),
                    ExpensesDetailsFixedAssets = table.Column<long>(type: "bigint", nullable: false),
                    ExpensesDetailsUtilityServices = table.Column<long>(type: "bigint", nullable: false),
                    ExpensesDetailsPublicWelfare = table.Column<long>(type: "bigint", nullable: false),
                    ExpensesDetailsCultureInvestments = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsTaxesAssetIncomeTax = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsTaxesPropertyTax = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsTaxesEntrepreneurshipTax = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsTaxesLandUseTax = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsGoodsAndServiceIncome = table.Column<long>(type: "bigint", nullable: false),
                    IncomesDetailsPropertyRentIncome = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OlapBudgetReport", x => new { x.LocalityId, x.YearId, x.DistrictId });
                    table.ForeignKey(
                        name: "FK_OlapBudgetReport_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "dwh",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OlapBudgetReport_Locality_LocalityId",
                        column: x => x.LocalityId,
                        principalSchema: "dwh",
                        principalTable: "Locality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OlapBudgetReport_Year_YearId",
                        column: x => x.YearId,
                        principalSchema: "dwh",
                        principalTable: "Year",
                        principalColumn: "Value");
                });

            migrationBuilder.InsertData(
                schema: "dwh",
                table: "Year",
                column: "Value",
                values: new object[]
                {
                    2000,
                    2001,
                    2002,
                    2003,
                    2004,
                    2005,
                    2006,
                    2007,
                    2008,
                    2009,
                    2010,
                    2011,
                    2012,
                    2013,
                    2014,
                    2015,
                    2016,
                    2017,
                    2018,
                    2019,
                    2020,
                    2021,
                    2022,
                    2023,
                    2024
                });

            migrationBuilder.CreateIndex(
                name: "IX_District_Code",
                schema: "dwh",
                table: "District",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locality_Code",
                schema: "dwh",
                table: "Locality",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locality_ParentDistrictId",
                schema: "dwh",
                table: "Locality",
                column: "ParentDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OlapBudgetReport_DistrictId",
                schema: "dwh",
                table: "OlapBudgetReport",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OlapBudgetReport_YearId",
                schema: "dwh",
                table: "OlapBudgetReport",
                column: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OlapBudgetReport",
                schema: "dwh");

            migrationBuilder.DropTable(
                name: "Locality",
                schema: "dwh");

            migrationBuilder.DropTable(
                name: "Year",
                schema: "dwh");

            migrationBuilder.DropTable(
                name: "District",
                schema: "dwh");
        }
    }
}
