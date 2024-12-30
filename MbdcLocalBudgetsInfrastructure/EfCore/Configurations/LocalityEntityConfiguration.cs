using MbdcLocalBudgetsDomain.Entities.Olap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations;

public class LocalityEntityConfiguration : IEntityTypeConfiguration<Locality>
{
    public void Configure(EntityTypeBuilder<Locality> builder)
    {
        builder.ToTable(nameof(Locality));
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ParentDistrictId)
            .IsRequired(false);

        builder.Property(e => e.Population)
            .IsRequired(false);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(EntityConfigurationLimits.NvarcharMaxLength);

        builder.HasIndex(e => e.Code).IsUnique();
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(EntityConfigurationLimits.NvarcharMaxLength);
    }
}