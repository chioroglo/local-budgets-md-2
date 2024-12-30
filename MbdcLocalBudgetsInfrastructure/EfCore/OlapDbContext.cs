using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MbdcLocalBudgetsInfrastructure.EfCore;

public class OlapDbContext : DbContext , IUnitOfWork
{
    public OlapDbContext() {}
    public OlapDbContext(DbContextOptions<OlapDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("dwh");

        var assembly = InfrastructureAssemblyMarker.Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    public DbSet<District> Districts { get; init; }
    public DbSet<Year> Years { get; init; }
    public DbSet<Locality> Localities { get; init; }
    public DbSet<OlapBudgetReport> OlapBudgetReports { get; init; }
}