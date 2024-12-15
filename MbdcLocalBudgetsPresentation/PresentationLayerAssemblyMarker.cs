using System.Reflection;

namespace MbdcLocalBudgetsPresentation;

public struct PresentationLayerAssemblyMarker
{
    public static Assembly Assembly => typeof(PresentationLayerAssemblyMarker).Assembly;
}