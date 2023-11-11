using System.Reflection;
namespace Features.Helpers;

public static class FeaturesAssemblyReference
{
    public static readonly Assembly Assembly = typeof(FeaturesAssemblyReference).Assembly;
}