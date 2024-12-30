using MbdcLocalBudgetsDomain.Entities.Olap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations;

public class DistrictEntityConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(nameof(District));

        builder.HasIndex(e => e.Code).IsUnique();

        builder.HasMany(e => e.BudgetReports)
            .WithOne(e => e.District);

        builder.Property(e => e.Code)
            .HasMaxLength(EntityConfigurationLimits.NvarcharMaxLength);

        builder.Property(e => e.Name)
            .HasMaxLength(EntityConfigurationLimits.NvarcharMaxLength);
    }
}