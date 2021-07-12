namespace Features.GetPerson
{
    public class GetPersonResponse : ResponseBase<PersonModel>
    {
        public GetPersonResponse(PersonModel value, bool succes = true) : base(value, succes) { }
    }
}
