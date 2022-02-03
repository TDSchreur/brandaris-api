using MediatR;

namespace Brandaris.Features.AddPerson;

public record AddPersonCommand(string FirstName, string LastName) : IRequest<AddPersonResponse>;
