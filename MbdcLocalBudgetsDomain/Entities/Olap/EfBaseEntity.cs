namespace MbdcLocalBudgetsDomain.Entities.Olap;

public abstract class EfBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}