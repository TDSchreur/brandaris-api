using System.Collections.Generic;

namespace Features.GetPerson
{
    public class GetPersonResponse : ResponseBase<IEnumerable<PersonModel>>
    {
        public GetPersonResponse(IEnumerable<PersonModel> value, bool succes = true) : base(value, succes) { }
    }
}
