using System.Threading.Tasks;
using TestFrontEnd.Models;

namespace TestFrontEnd.ServiceAgents
{
    public interface IBrandarisApiServiceAgent
    {
        Task<GetPersonResponse> GetPersonAsync(int id);
    }
}
