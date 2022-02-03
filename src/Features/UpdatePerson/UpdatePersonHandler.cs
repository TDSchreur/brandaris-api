using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, UpdatePersonResponse>
{
    private readonly ICommand<Person> _command;

    public UpdatePersonHandler(ICommand<Person> command) => _command = command;

    public async Task<UpdatePersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = new()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        _command.Update(
                        person,
                        x => x.FirstName,
                        x => x.LastName);

        await _command.SaveChangesAsync(cancellationToken);

        return new UpdatePersonResponse(new PersonModel
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName
        });
    }
}
