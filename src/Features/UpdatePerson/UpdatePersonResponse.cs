using Brandaris.Features.Models;

namespace Brandaris.Features.UpdatePerson;

public record UpdatePersonResponse(PersonModel Value, bool Success = true) : ResponseBase<PersonModel>(Value, Success);