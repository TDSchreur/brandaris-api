using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        Task<GetPersonResponse> GetPersonAsync(int id = 1);
    }

    public class BrandarisApiServiceAgent : IBrandarisApiServiceAgent
    {
        private readonly HttpClient _httpClient;

        public BrandarisApiServiceAgent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetPersonResponse> GetPersonAsync(int id = 1)
        {
#pragma warning disable CA2234
            var response = await _httpClient.GetAsync($"/api/person/{id}");
#pragma warning restore CA2234
            return await response.Content.ReadFromJsonAsync<GetPersonResponse>();
        }
    }
}
