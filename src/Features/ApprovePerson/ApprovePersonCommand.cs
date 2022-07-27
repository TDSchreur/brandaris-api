using MediatR;

namespace Brandaris.Features.ApprovePerson;

public record ApprovePersonCommand(int Id) : IRequest<ApprovePersonsResponse>;