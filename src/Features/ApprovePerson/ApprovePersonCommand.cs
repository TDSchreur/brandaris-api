using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.AddPerson;
using Brandaris.Features.Models;
using Brandaris.Features.UpdatePerson;
using MediatR;

namespace Brandaris.Features.ApprovePerson;

public record ApprovePersonCommand(int Id) : IRequest<ApprovePersonsResponse>;

public class ApprovePersonHandler : IRequestHandler<ApprovePersonCommand, ApprovePersonsResponse>
{
    private readonly ICommand<Person> _command;
    private readonly IMediator _mediator;
    private readonly IQuery<PersonPreCheck> _query;

    public ApprovePersonHandler(IQuery<PersonPreCheck> query,
                                ICommand<Person> command,
                                IMediator mediator)
    {
        _query = query;
        _command = command;
        _mediator = mediator;
    }

    public async Task<ApprovePersonsResponse> Handle(ApprovePersonCommand request, CancellationToken cancellationToken)
    {
        PersonPreCheck personPreCheck = await _query.FirstOrDefaultAsync(x => x.Id == request.Id);
        PersonModel personModel;

        if (personPreCheck.ParentId.HasValue)
        {
            UpdatePersonResponse updatePersonResponse = await _mediator.Send(new UpdatePersonCommand(personPreCheck.ParentId.Value, personPreCheck.FirstName, personPreCheck.LastName, true), cancellationToken);
            personModel = updatePersonResponse.Value;
        }
        else
        {
            AddPersonResponse addPersonResponse = await _mediator.Send(new AddPersonCommand(personPreCheck.FirstName, personPreCheck.LastName, true), cancellationToken);
            personModel = addPersonResponse.Value;
        }

        _command.Remove(new PersonPreCheck
        {
            Id = personPreCheck.Id
        });

        await _command.SaveChangesAsync(cancellationToken);

        ApprovePersonsResponse retval = new(personModel);
        return retval;
    }
}

public record ApprovePersonsResponse(PersonModel Value, bool Succes = true) : ResponseBase<PersonModel>(Value, Succes);
