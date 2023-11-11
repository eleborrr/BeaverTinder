using System.Reflection;
namespace Application.Helpers;

public static class ApllicationAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ApllicationAssemblyReference).Assembly;
}