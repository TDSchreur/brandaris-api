namespace Features.GetPerson
{
    public class AddPersonResponse : ResponseBase<PersonModel>
    {
        public AddPersonResponse(PersonModel value, bool success = true) : base(value, success)
        {
        }
    }
}
