using MbdcLocalBudgetsDomain.Entities.Olap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations;

public class OlapBudgetReportEntityConfiguration : IEntityTypeConfiguration<OlapBudgetReport>
{
    public void Configure(EntityTypeBuilder<OlapBudgetReport> builder)
    {
        builder.ToTable(nameof(OlapBudgetReport));

        builder.HasKey(obr => new { obr.LocalityId, obr.YearId, obr.DistrictId });

        builder.HasOne(e => e.Locality)
            .WithMany(e => e.BudgetReports)
            .HasForeignKey(e => e.LocalityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Year)
            .WithMany(e => e.BudgetReports)
            .HasForeignKey(e => e.YearId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}