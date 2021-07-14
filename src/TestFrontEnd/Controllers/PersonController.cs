using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestFrontEnd.Models;
using TestFrontEnd.ServiceAgents;

namespace TestFrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IBrandarisApiServiceAgent _brandarisApiServiceAgent;

        public PersonController(IBrandarisApiServiceAgent brandarisApiServiceAgent) => _brandarisApiServiceAgent = brandarisApiServiceAgent;

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPersonResponse>> GetAsync(int id)
        {
            GetPersonResponse person = await _brandarisApiServiceAgent.GetPersonAsync(id);

            return person;
        }

        [HttpGet("claims")]
        public ActionResult<IEnumerable<KeyValuePair<string, string>>> GetClaims()
        {
            IEnumerable<KeyValuePair<string, string>> claims = HttpContext.User.Claims
                                                                          .OrderBy(x => x.Type)
                                                                          .Select(kv => new KeyValuePair<string, string>(kv.Type, kv.Value));

            return Ok(claims);
        }

        [HttpGet("claims_remote")]
        public async Task<ActionResult<IEnumerable<KeyValuePair<string, string>>>> GetClaimsRemoteAsync()
        {
            IEnumerable<KeyValuePair<string, string>> claims = await _brandarisApiServiceAgent.GetRemoteClaimsAsync();

            return Ok(claims);
        }
    }
}
