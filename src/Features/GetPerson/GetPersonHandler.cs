using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Brandaris.Features.GetPerson;

public class GetPersonHandler : IRequestHandler<GetPersonQuery, GetPersonResponse>
{
    private readonly ILogger<GetPersonHandler> _logger;
    private readonly IQuery<Person> _query;

    public GetPersonHandler(IQuery<Person> query, ILogger<GetPersonHandler> logger)
    {
        _query = query;
        _logger = logger;
    }

    public async Task<GetPersonResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Personmodel with ID: {id}", request.Id);

        PersonModel person = await _query.Where(x => x.Id == request.Id)
                                         .Select(x => new PersonModel(x.Id, x.FirstName, x.LastName, null))
                                         .FirstOrDefaultAsync(cancellationToken);

        return new GetPersonResponse(person, person != null);
    }
}