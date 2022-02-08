using Brandaris.Features.Models;

namespace Brandaris.Features.GetPerson;

public record GetPersonResponse(PersonModel Value, bool Succes = true) : ResponseBase<PersonModel>(Value, Succes);
