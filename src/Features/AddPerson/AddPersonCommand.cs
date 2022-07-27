using System;
using MediatR;

namespace Brandaris.Features.AddPerson;

public record AddPersonCommand
(string FirstName,
 string LastName,
 bool Approved = false,
 string CreatedBy = null,
 Guid? CreatedById = null,
 DateTimeOffset? CreatedDate = null) : IRequest<AddPersonResponse>;