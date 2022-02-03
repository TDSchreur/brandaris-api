using System.Collections.Generic;
using Brandaris.Features.Models;

namespace Brandaris.Features.GetPerson;

public record GetPersonsResponse(ICollection<PersonModel> Value, bool Succes = true) : ResponseBase<ICollection<PersonModel>>(Value, Succes);
