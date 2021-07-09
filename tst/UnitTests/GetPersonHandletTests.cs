using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Data;
using Data.Entities;
using DataAccess;
using Xunit;

namespace UnitTests
{
    public class GetPersonHandletTests : IClassFixture<PersonDatabaseFixture>
    {
        public GetPersonHandletTests(PersonDatabaseFixture fixture) => Context = fixture.Context;

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

            GetPersonHandler sut = new(query);

            // act
            GetPersons request = new()
                                 {
                                     FirstName = firstname, LastName = lastName
                                 };
            ICollection<PersonModel> result = await sut.Handle(request, CancellationToken.None);

            // assert
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                Assert.All(result, x => Assert.Equal(firstname, x.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                Assert.All(result, x => Assert.Equal(lastName, x.LastName));
            }

            Assert.Equal(expectedResults, result.Count);
        }
    }
}
