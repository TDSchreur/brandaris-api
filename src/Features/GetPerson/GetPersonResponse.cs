using Features.Models;

namespace Features.GetPerson;

public record GetPersonResponse(PersonModel Value, bool Succes = true) : ResponseBase<PersonModel>(Value, Succes);
