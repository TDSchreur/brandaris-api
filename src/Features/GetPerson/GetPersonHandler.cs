using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.GetPerson;

public class GetPersonHandler : IRequestHandler<GetPersonQuery, GetPersonResponse>
{
    private readonly IQuery<Person> _query;

    public GetPersonHandler(IQuery<Person> query) => _query = query;

    public async Task<GetPersonResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        PersonModel person = await _query.Where(x => x.Id == request.Id)
                                         .Select(x => new PersonModel(x.Id, x.FirstName, x.LastName, null))
                                         .FirstOrDefaultAsync(cancellationToken);

        return new GetPersonResponse(person, person != null);
    }
}
