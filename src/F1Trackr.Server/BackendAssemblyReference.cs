using System.Reflection;

namespace F1Trackr.Server;

public sealed class BackendAssemblyReference
{
    public static readonly Assembly Assembly = typeof(BackendAssemblyReference).Assembly;
}
