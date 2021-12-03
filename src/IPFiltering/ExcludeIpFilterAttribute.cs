namespace IPFiltering;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ExcludeIpFilterAttribute : Attribute, IExcludeIpFilter { }
#pragma warning restore
