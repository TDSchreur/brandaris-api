using System;

namespace IPFiltering;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ExcludeIpFilterAttribute : Attribute, IExcludeIpFilter { }