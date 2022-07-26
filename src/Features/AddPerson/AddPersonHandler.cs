using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.AddPerson;

public class AddPersonHandler : IRequestHandler<AddPersonCommand, AddPersonResponse>
{
    private readonly ICommand<Person> _personCommand;
    private readonly ICommand<PersonPreCheck> _personPreCheckCommand;

    public AddPersonHandler(ICommand<Person> personCommand,
                            ICommand<PersonPreCheck> personPreCheckCommand)
    {
        _personCommand = personCommand;
        _personPreCheckCommand = personPreCheckCommand;
    }

    public async Task<AddPersonResponse> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        int id;

        if (request.Approved)
        {
            Person person = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedBy = request.CreatedBy,
                CreatedById = request.CreatedById.GetValueOrDefault(),
                CreatedDate = request.CreatedDate.GetValueOrDefault()
            };
            _personCommand.Add(person);
            await _personCommand.SaveChangesAsync(cancellationToken);
            id = person.Id;
        }
        else
        {
            PersonPreCheck person = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _personPreCheckCommand.Add(person);
            await _personPreCheckCommand.SaveChangesAsync(cancellationToken);
            id = person.Id;
        }

        return new AddPersonResponse(new PersonModel(id, request.FirstName, request.LastName));
    }
}
