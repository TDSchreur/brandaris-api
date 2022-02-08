using Brandaris.Features.Models;

namespace Brandaris.Features.ApprovePerson;

public record ApprovePersonsResponse(PersonModel Value, bool Succes = true) : ResponseBase<PersonModel>(Value, Succes);
