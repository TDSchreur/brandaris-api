using MediatR;

namespace Features.GetPerson;

public record GetPersonQuery(int Id) : IRequest<GetPersonResponse>;
