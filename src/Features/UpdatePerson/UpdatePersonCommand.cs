using MediatR;

namespace Features.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<UpdatePersonResponse>
    {
        public int Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
