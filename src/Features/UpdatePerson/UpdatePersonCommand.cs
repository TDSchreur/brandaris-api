using System;
using MediatR;

namespace Brandaris.Features.UpdatePerson;

public record UpdatePersonCommand : IRequest<UpdatePersonResponse>
{
    public string FirstName { get; init; }

    public int Id { get; init; }

    public string LastName { get; init; }

    public DateTimeOffset Date { get; init; }
}
