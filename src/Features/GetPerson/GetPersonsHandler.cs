using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.GetPerson;

public class GetPersonsHandler : IRequestHandler<GetPersonsQuery, GetPersonsResponse>
{
    private readonly IQuery<Person> _query;

    public GetPersonsHandler(IQuery<Person> query) => _query = query;

    public async Task<GetPersonsResponse> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        List<PersonModel> persons = await _query.Where(x => !request.HasFirstName || x.FirstName == request.FirstName)
                                                .Where(x => !request.HasLastName || x.LastName == request.LastName)
                                                .Select(x => new PersonModel
                                                             {
                                                                 Id = x.Id,
                                                                 FirstName = x.FirstName,
                                                                 LastName = x.LastName
                                                             })
                                                .ToListAsync(cancellationToken);

        return new GetPersonsResponse(persons);
    }
}
