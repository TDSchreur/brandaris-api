using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, UpdatePersonResponse>
{
    private readonly ICommand<Person> _personCommand;
    private readonly ICommand<PersonPreCheck> _personPreCheckCommand;

    public UpdatePersonHandler(ICommand<Person> personCommand,
                               ICommand<PersonPreCheck> personPreCheckCommand)
    {
        _personCommand = personCommand;
        _personPreCheckCommand = personPreCheckCommand;
    }

    public async Task<UpdatePersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        int id;
        int? parentId = null;

        if (request.Approved)
        {
            Person person = new()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UpdatedBy = request.UpdatedBy,
                UpdatedById = request.UpdatedById.GetValueOrDefault(),
                UpdatedDate = request.UpdatedDate.GetValueOrDefault()
            };

            _personCommand.Attach(person);
            _personCommand.Update(person, x => x.FirstName);
            _personCommand.Update(person, x => x.LastName);
            _personCommand.Update(person, x => x.UpdatedBy);
            _personCommand.Update(person, x => x.UpdatedById);
            _personCommand.Update(person, x => x.UpdatedDate);

            await _personCommand.SaveChangesAsync(cancellationToken);
            id = person.Id;
        }
        else
        {
            PersonPreCheck person = new()
            {
                ParentId = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _personPreCheckCommand.Attach(person);
            _personPreCheckCommand.Update(person, x => x.FirstName);
            _personPreCheckCommand.Update(person, x => x.LastName);

            id = person.Id;
            parentId = request.Id;
            await _personPreCheckCommand.SaveChangesAsync(cancellationToken);
        }

        return new UpdatePersonResponse(new PersonModel(id, request.FirstName, request.LastName, parentId));
    }
}
