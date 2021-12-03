using MediatR;

namespace Features.GetPerson;

public class GetPersonQuery : IRequest<GetPersonResponse>
{
    public int Id { get; init; }
}
