using Features.Models;

namespace Features.UpdatePerson;

public class UpdatePersonResponse : ResponseBase<PersonModel>
{
    public UpdatePersonResponse(PersonModel value, bool success = true) : base(value, success) { }
}
