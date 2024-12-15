using System.Reflection;

namespace MbdcLocalBudgetsApi;

public struct WebApiAssemblyMarker
{
    public static Assembly Assembly => typeof(WebApiAssemblyMarker).Assembly;
}