using Features.Models;

namespace Features.AddPerson;

public record AddPersonResponse(PersonModel Value, bool Success = true) : ResponseBase<PersonModel>(Value, Success);
