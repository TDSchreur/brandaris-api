using MediatR;

namespace Features.GetPerson
{
    public class AddPersonCommand : IRequest<AddPersonResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
