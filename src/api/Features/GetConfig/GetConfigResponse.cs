using System.Collections.Generic;

namespace Brandaris.Api.Features.GetConfig
{
    public class GetConfigResponse
    {
        public IEnumerable<KeyValuePair<string, string>> Values { get; init; }
    }
}
