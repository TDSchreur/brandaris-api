namespace Features.Models
{
    public class PersonModel : IResponseModel
    {
        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }
    }
}
