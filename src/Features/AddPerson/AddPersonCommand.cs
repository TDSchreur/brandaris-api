using MediatR;

namespace Features.AddPerson;

public record AddPersonCommand(string FirstName, string LastName) : IRequest<AddPersonResponse>;
