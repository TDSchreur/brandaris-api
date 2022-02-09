using System.Collections.Generic;
using System.Threading.Tasks;
using TestFrontEnd.Models;

namespace TestFrontEnd.ServiceAgents;

public interface IBrandarisApiServiceAgent
{
    Task<IEnumerable<KeyValuePair<string, string>>> GetRemoteClaimsAsync();
}
