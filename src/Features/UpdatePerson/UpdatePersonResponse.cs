using Features.Models;

namespace Features.UpdatePerson;

public record UpdatePersonResponse(PersonModel Value, bool Success = true) : ResponseBase<PersonModel>(Value, Success);
