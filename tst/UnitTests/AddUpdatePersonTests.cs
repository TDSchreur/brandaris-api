using System.Linq.Expressions;
using Data.Entities;
using DataAccess;
using Features.AddPerson;
using Features.UpdatePerson;
using Moq;
using Xunit;

namespace UnitTests;

public class AddUpdatePersonTests
{
    [Fact]
    public async Task AddPerson()
    {
        // arrange
        const string donald = nameof(donald);
        const string duck = nameof(duck);
        AddPersonCommand request = new(donald, duck);

        Mock<ICommand<Person>> qm = new(MockBehavior.Strict);
        qm.Setup(x => x.Add(It.IsAny<Person>()))
          .Callback((Person[] p) => p[0].Id = 1);
        qm.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(1);

        AddPersonHandler sut = new(qm.Object);

        // act
        AddPersonResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(1, result.Value.Id);
        Assert.Equal(donald, result.Value.FirstName);
        Assert.Equal(duck, result.Value.LastName);
    }

    [Fact]
    public async Task UpdatePerson()
    {
        // arrange
        const string tony = nameof(tony);
        const string stark = nameof(stark);
        UpdatePersonCommand request = new()
        {
            Id = 1,
            FirstName = tony,
            LastName = stark
        };

        string newFirstName = string.Empty;
        string newLastName = string.Empty;

        Mock<ICommand<Person>> qm = new(MockBehavior.Strict);
        qm.Setup(x => x.Update(It.IsAny<Person>(),
                               It.IsAny<Expression<Func<Person, string>>>(),
                               It.IsAny<Expression<Func<Person, string>>>()))
          .Callback((Person p,
                     Expression<Func<Person, string>>[] _) =>
           {
               newFirstName = p.FirstName;
               newLastName = p.LastName;
           });

        qm.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(1);

        UpdatePersonHandler sut = new(qm.Object);

        // act
        UpdatePersonResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(tony, result.Value.FirstName);
        Assert.Equal(tony, newFirstName);
        Assert.Equal(stark, result.Value.LastName);
        Assert.Equal(stark, newLastName);
    }
}
