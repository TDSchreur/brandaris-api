using Brandaris.Features.Models;

namespace Brandaris.Features.AddPerson;

public record AddPersonResponse(PersonModel Value, bool Success = true) : ResponseBase<PersonModel>(Value, Success);
