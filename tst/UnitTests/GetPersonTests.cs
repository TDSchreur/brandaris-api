using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.GetPerson;
using Brandaris.Features.GetPersons;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable.Moq;
using Xunit;

namespace UnitTests;

public class GetPersonTests
{
    public GetPersonTests()
    {
        List<Person> testdataPerson = new()
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

        List<PersonPreCheck> testdataPreCheck = new()
        {
            new PersonPreCheck
            {
                Id = 1,
                FirstName = "Dennis",
                LastName = "Schreur"
            },
            new PersonPreCheck
            {
                Id = 2,
                FirstName = "Tess",
                LastName = "Schreur"
            },
            new PersonPreCheck
            {
                Id = 3,
                FirstName = "Daan",
                LastName = "Schreur"
            },
            new PersonPreCheck
            {
                Id = 4,
                FirstName = "Peter",
                LastName = "Pan"
            }
        };

        PersonQuery = new Query<Person>(testdataPerson.AsQueryable().BuildMock());
        PersonPreCheckQuery = new Query<PersonPreCheck>(testdataPreCheck.AsQueryable().BuildMock());
    }

    private Query<PersonPreCheck> PersonPreCheckQuery { get; }

    private Query<Person> PersonQuery { get; }

    [Fact]
    public async Task GetPerson_ShouldReturnNull()
    {
        // arrange
        GetPersonHandler sut = new(PersonQuery, NullLogger<GetPersonHandler>.Instance);

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
        GetPersonHandler sut = new(PersonQuery, NullLogger<GetPersonHandler>.Instance);

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
        GetPersonsHandler sut = new(PersonQuery, PersonPreCheckQuery);

        // act
        GetPersonsQuery request = new()
        {
            FirstName = firstname,
            LastName = lastName,
            Approved = false
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