using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.GetPerson;
using MockQueryable.Moq;
using Xunit;

namespace UnitTests;

public class GetPersonTests
{
    public GetPersonTests()
    {
        List<Person> testdata = new()
        {
            new Person
            {
                Id = 1,
                FirstName = "Dennis",
                LastName = "Schreur"
            },
            new Person
            {
                Id = 2,
                FirstName = "Tess",
                LastName = "Schreur"
            },
            new Person
            {
                Id = 3,
                FirstName = "Daan",
                LastName = "Schreur"
            },
            new Person
            {
                Id = 4,
                FirstName = "Peter",
                LastName = "Pan"
            }
        };

        Query = new Query<Person>(testdata.AsQueryable().BuildMock().Object);
    }

    public Query<Person> Query { get; }

    [Fact]
    public async Task GetPerson_ShouldReturnNull()
    {
        // arrange
        GetPersonHandler sut = new(Query);

        // act
        GetPersonQuery request = new(int.MaxValue);
        GetPersonResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Null(result.Value);
    }

    [Theory]
    [InlineData(1, "Dennis", "Schreur")]
    [InlineData(4, "Peter", "Pan")]
    public async Task GetPersonAsync(int id, string firstName, string lastName)
    {
        // arrange
        GetPersonHandler sut = new(Query);

        // act
        GetPersonQuery request = new(id);
        GetPersonResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(firstName, result.Value.FirstName);
        Assert.Equal(lastName, result.Value.LastName);
    }

    [Theory]
    [InlineData("", "Schreur", 3)]
    [InlineData("Dennis", "Schreur", 1)]
    [InlineData("", "Pan", 1)]
    [InlineData("", "", 4)]
    public async Task GetPersons(string firstname, string lastName, int expectedResults)
    {
        // arrange
        GetPersonsHandler sut = new(Query);

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
