using System.Reflection;

namespace MbdcLocalBudgetsApplication;

public struct ApplicationLayerAssemblyMarker
{
    public static Assembly Assembly => typeof(ApplicationLayerAssemblyMarker).Assembly;
}