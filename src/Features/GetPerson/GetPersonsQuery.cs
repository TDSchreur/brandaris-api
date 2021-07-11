using MediatR;

namespace Features.GetPerson
{
    public class GetPersonsQuery : IRequest<GetPersonsResponse>
    {
        public string FirstName { get; init; }

        public bool HasFirstName => !string.IsNullOrWhiteSpace(FirstName);

        public bool HasLastName => !string.IsNullOrWhiteSpace(LastName);

        public string LastName { get; init; }
    }
}
