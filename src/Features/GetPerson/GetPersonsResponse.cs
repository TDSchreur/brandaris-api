using System.Collections.Generic;
using Features.Models;

namespace Features.GetPerson;

public class GetPersonsResponse : ResponseBase<ICollection<PersonModel>>
{
    public GetPersonsResponse(ICollection<PersonModel> value, bool succes = true) : base(value, succes) { }
}
