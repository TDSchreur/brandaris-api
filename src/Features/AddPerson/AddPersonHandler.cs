using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using Features.Models;
using MediatR;

namespace Features.AddPerson
{
    public class AddPersonHandler : IRequestHandler<AddPersonCommand, AddPersonResponse>
    {
        private readonly ICommand<Person> _command;

        public AddPersonHandler(ICommand<Person> command) => _command = command;

        public async Task<AddPersonResponse> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            Person person = new()
                            {
                                FirstName = request.FirstName, LastName = request.LastName
                            };
            _command.Add(person);

            await _command.SaveChangesAsync(cancellationToken);

            return new AddPersonResponse(new PersonModel
                                         {
                                             Id = person.Id,
                                             FirstName = person.FirstName,
                                             LastName = person.LastName
                                         });
        }
    }
}
