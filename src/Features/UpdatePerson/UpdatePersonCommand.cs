using MediatR;

namespace Brandaris.Features.UpdatePerson;

public record UpdatePersonCommand(int Id, string FirstName, string LastName, bool Approved = false) : IRequest<UpdatePersonResponse>;
