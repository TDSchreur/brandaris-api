using MediatR;

namespace Features.AddPerson;

public class AddPersonCommand : IRequest<AddPersonResponse>
{
    public string FirstName { get; init; }

    public string LastName { get; init; }
}
