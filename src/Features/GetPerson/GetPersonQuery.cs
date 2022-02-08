using MediatR;

namespace Brandaris.Features.GetPerson;

public record GetPersonQuery(int Id) : IRequest<GetPersonResponse>;
