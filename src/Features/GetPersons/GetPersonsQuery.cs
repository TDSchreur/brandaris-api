using MediatR;

namespace Brandaris.Features.GetPersons;

public record GetPersonsQuery : IRequest<GetPersonsResponse>
{
    public bool Approved { get; init; }

    public string FirstName { get; init; }

    internal bool HasFirstName => !string.IsNullOrWhiteSpace(FirstName);

    internal bool HasLastName => !string.IsNullOrWhiteSpace(LastName);

    public string LastName { get; init; }
}