using Features.Models;

namespace Features.AddPerson
{
    public class AddPersonResponse : ResponseBase<PersonModel>
    {
        public AddPersonResponse(PersonModel value, bool success = true) : base(value, success) { }
    }
}
