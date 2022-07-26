using System;
using MediatR;

namespace Brandaris.Features.UpdatePerson;

public record UpdatePersonCommand(int Id, string FirstName, string LastName, bool Approved = false, string UpdatedBy = null, Guid? UpdatedById = null, DateTimeOffset? UpdatedDate = null) : IRequest<UpdatePersonResponse>;