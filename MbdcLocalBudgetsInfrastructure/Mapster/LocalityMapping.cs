using Mapster;
using MbdcLocalBudgetsDomain.Entities.Olap;
using MbdcLocalBudgetsDomain.Models;

namespace MbdcLocalBudgetsInfrastructure.Mapster;

public class LocalityMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Locality, LocalityModel>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.District, src => src.ParentDistrict.Name ?? "")
            .Map(dest => dest.Population, src => src.Population)
            .RequireDestinationMemberSource(true);
    }
}