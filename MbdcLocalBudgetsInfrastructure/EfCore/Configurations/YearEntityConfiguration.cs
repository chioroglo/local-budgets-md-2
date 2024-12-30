using MbdcLocalBudgetsDomain.Entities.Olap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MbdcLocalBudgetsInfrastructure.EfCore.Configurations;

public class YearEntityConfiguration : IEntityTypeConfiguration<Year>
{
    public void Configure(EntityTypeBuilder<Year> builder)
    {
        builder.ToTable(nameof(Year), builder =>
        {
            builder.HasCheckConstraint("CK_Year_Epoch", "[Value] >= 1970");
        });

        builder.HasKey(e => e.Value);
        builder.Property(e => e.Value).ValueGeneratedNever();
        builder.HasData(new List<Year>
        {
            new Year(2000),
            new Year(2001),
            new Year(2002),
            new Year(2003),
            new Year(2004),
            new Year(2005),
            new Year(2006),
            new Year(2007),
            new Year(2008),
            new Year(2009),
            new Year(2010),
            new Year(2011),
            new Year(2012),
            new Year(2013),
            new Year(2014),
            new Year(2015),
            new Year(2016),
            new Year(2017),
            new Year(2018),
            new Year(2019),
            new Year(2020),
            new Year(2021),
            new Year(2022),
            new Year(2023),
            new Year(2024),
        });
    }
}