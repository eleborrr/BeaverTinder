using System.Reflection;

namespace BeaverTinder.Application.Helpers;

public static class AplicationAssemblyReference
{
    public static readonly Assembly Assembly = typeof(AplicationAssemblyReference).Assembly;
}