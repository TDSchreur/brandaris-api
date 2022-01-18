using System.Collections.Generic;
using Features.Models;

namespace Features.GetPerson;

public record GetPersonsResponse(ICollection<PersonModel> Value, bool Succes = true) : ResponseBase<ICollection<PersonModel>>(Value, Succes);
