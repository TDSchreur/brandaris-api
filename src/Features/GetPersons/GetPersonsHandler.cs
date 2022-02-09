using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.GetPersons;

public class GetPersonsHandler : IRequestHandler<GetPersonsQuery, GetPersonsResponse>
{
    private readonly IQuery<PersonPreCheck> _personPreCheckQuery;
    private readonly IQuery<Person> _personQuery;

    public GetPersonsHandler(IQuery<Person> personQuery,
                             IQuery<PersonPreCheck> personPreCheckQuery)
    {
        _personQuery = personQuery;
        _personPreCheckQuery = personPreCheckQuery;
    }

    public async Task<GetPersonsResponse> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        if (request.Approved)
        {
            List<PersonModel> persons = await _personQuery.Where(x => !request.HasFirstName || x.FirstName == request.FirstName)
                                                          .Where(x => !request.HasLastName || x.LastName == request.LastName)
                                                          .Select(x => new PersonModel(x.Id, x.FirstName, x.LastName, null))
                                                          .ToListAsync(cancellationToken);

            return new GetPersonsResponse(persons);
        }
        else
        {
            List<PersonModel> persons = await _personPreCheckQuery.Where(x => !request.HasFirstName || x.FirstName == request.FirstName)
                                                                  .Where(x => !request.HasLastName || x.LastName == request.LastName)
                                                                  .Select(x => new PersonModel(x.Id, x.FirstName, x.LastName, x.ParentId))
                                                                  .ToListAsync(cancellationToken);

            return new GetPersonsResponse(persons);
        }
    }
}
