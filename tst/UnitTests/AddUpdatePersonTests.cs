using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.AddPerson;
using Brandaris.Features.UpdatePerson;
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

        AddPersonCommand request = new(donald, duck, true);

        Mock<ICommand<Person>> personCommand = new(MockBehavior.Strict);
        Mock<ICommand<PersonPreCheck>> personPreCheckCommand = new(MockBehavior.Strict);

        personCommand.Setup(x => x.Add(It.IsAny<Person>()))
                     .Callback((Person[] p) => p[0]
                                  .Id = 1);
        personCommand.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(1);

        AddPersonHandler sut = new(personCommand.Object, personPreCheckCommand.Object);

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
        UpdatePersonCommand request = new(1, tony, stark, true);

        Mock<ICommand<Person>> personCommand = new(MockBehavior.Strict);
        Mock<ICommand<PersonPreCheck>> personPreCheckCommand = new(MockBehavior.Strict);

        personCommand.Setup(x => x.Attach(It.IsAny<Person>()));
        personCommand.Setup(x => x.Update(It.IsAny<Person>(),
                                          It.IsAny<Expression<Func<Person, It.IsAnyType>>>()));

        personCommand.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(1);

        UpdatePersonHandler sut = new(personCommand.Object, personPreCheckCommand.Object);

        // act
        UpdatePersonResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(tony, result.Value.FirstName);
        Assert.Equal(stark, result.Value.LastName);
    }
}
