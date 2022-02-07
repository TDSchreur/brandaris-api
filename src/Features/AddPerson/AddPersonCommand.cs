using MediatR;

namespace Brandaris.Features.AddPerson;

public record AddPersonCommand(string FirstName, string LastName, bool Approved = false) : IRequest<AddPersonResponse>;
