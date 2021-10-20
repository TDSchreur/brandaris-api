using System;

namespace IPFiltering
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ExcludeIpFilterAttribute : Attribute, IExcludeIpFilter
    {
        public ExcludeIpFilterAttribute() { }
    }
#pragma warning restore

}
