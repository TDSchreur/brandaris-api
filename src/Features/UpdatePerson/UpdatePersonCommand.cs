using MediatR;

namespace Features.UpdatePerson;

public class UpdatePersonCommand : IRequest<UpdatePersonResponse>
{
    public string FirstName { get; init; }

    public int Id { get; init; }

    public string LastName { get; init; }
}
