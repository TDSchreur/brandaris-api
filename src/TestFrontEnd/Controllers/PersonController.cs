using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable SA1201
#pragma warning disable SA1402

namespace TestFrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IBrandarisApiServiceAgent _brandarisApiServiceAgent;

        public PersonController(IBrandarisApiServiceAgent brandarisApiServiceAgent)
        {
            _brandarisApiServiceAgent = brandarisApiServiceAgent;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPersonResponse>> GetAsync(int id)
        {
            var person = await _brandarisApiServiceAgent.GetPersonAsync(id);

            return person;
        }

        [HttpGet("claims")]
        public ActionResult<IEnumerable<KeyValuePair<string, string>>> Get()
        {
            IEnumerable<KeyValuePair<string, string>> claims = HttpContext.User.Claims
                                                                          .OrderBy(x => x.Type)
                                                                          .Select(kv => new KeyValuePair<string, string>(kv.Type, kv.Value));

            return Ok(claims);
        }
    }

    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
#pragma warning disable CA1054
        [AllowAnonymous]
        [HttpGet("signin")]
        public IActionResult SignIn([FromQuery] string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }

            return Challenge(
                new AuthenticationProperties { RedirectUri = returnUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("signout")]
        public override SignOutResult SignOut()
        {
            return SignOut(
                new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }

    public class PersonModel
    {
        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }
    }

    public class GetPersonResponse : ResponseBase<PersonModel>
    {
        public GetPersonResponse() : base() { }
    }

    public abstract class ResponseBase<TValue>
    {
        public bool Success { get; set; }

        public TValue Value { get; set; }
    }

    public interface IBrandarisApiServiceAgent
    {
        Task<GetPersonResponse> GetPersonAsync(int id);
    }

    public class BrandarisApiServiceAgent : IBrandarisApiServiceAgent
    {
        private readonly HttpClient _httpClient;

        public BrandarisApiServiceAgent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetPersonResponse> GetPersonAsync(int id)
        {
#pragma warning disable CA2234
            var response = await _httpClient.GetAsync($"/api/person/{id}");
#pragma warning restore CA2234
            return await response.Content.ReadFromJsonAsync<GetPersonResponse>();
        }
    }
}
