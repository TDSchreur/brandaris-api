using MediatR;

namespace Features.GetPerson;

public record GetPersonsQuery : IRequest<GetPersonsResponse>
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    internal bool HasFirstName => !string.IsNullOrWhiteSpace(FirstName);

    internal bool HasLastName => !string.IsNullOrWhiteSpace(LastName);
}
