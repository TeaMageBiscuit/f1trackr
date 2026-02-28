using System.Reflection;

namespace F1Trackr.Core;

public sealed class CoreAssemblyReference
{
    public static readonly Assembly Assembly = typeof(CoreAssemblyReference).Assembly;
}
