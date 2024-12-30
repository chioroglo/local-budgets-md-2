using System.Reflection;

namespace MbdcLocalBudgetsInfrastructure;

public struct InfrastructureAssemblyMarker
{
    public static Assembly Assembly => typeof(InfrastructureAssemblyMarker).Assembly;
}