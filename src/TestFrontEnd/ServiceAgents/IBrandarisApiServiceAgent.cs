using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestFrontEnd.ServiceAgents;

public interface IBrandarisApiServiceAgent
{
    Task<IEnumerable<KeyValuePair<string, string>>> GetRemoteClaimsAsync();
}