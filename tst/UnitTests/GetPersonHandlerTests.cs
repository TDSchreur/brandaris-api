using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using DataAccess;
using Features.GetPerson;
using Xunit;

namespace UnitTests
{
    public class GetPersonHandlerTests : IClassFixture<PersonDatabaseFixture>
    {
        public GetPersonHandlerTests(PersonDatabaseFixture fixture) => Context = fixture.Context;

        public DataContext Context { get; }

        [Theory]
        [InlineData("", "Schreur", 3)]
        [InlineData("Dennis", "Schreur", 1)]
        [InlineData("", "Pan", 1)]
        [InlineData("", "", 4)]
        public async Task GetPersons(string firstname, string lastName, int expectedResults)
        {
            // arrange
            Query<Person> query = new(Context);

            GetPersonsHandler sut = new(query);

            // act
            GetPersonsQuery request = new()
                                 {
                                     FirstName = firstname, LastName = lastName
                                 };
            GetPersonsResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                Assert.All(result.Value, x => Assert.Equal(firstname, x.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                Assert.All(result.Value, x => Assert.Equal(lastName, x.LastName));
            }

            Assert.Equal(expectedResults, result.Value.Count);
        }
    }
}
