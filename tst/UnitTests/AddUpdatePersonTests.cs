using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using DataAccess;
using Features.AddPerson;
using Features.UpdatePerson;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class AddUpdatePersonTests
    {
        [Fact]
        public async Task AddPerson()
        {
            // arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(nameof(AddPerson)).Options;
            using var context = new DataContext(options);
            context.SaveChanges();
            Command<Person> command = new(context);

            AddPersonHandler sut = new(command);

            // act
            AddPersonCommand request = new()
            {
                FirstName = "Donald",
                LastName = "Duck"
            };
            AddPersonResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task UpdatePerson()
        {
            // arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(nameof(UpdatePerson)).Options;
            using (var context = new DataContext(options))
            {
                context.Persons.Add(new Person { Id = 1, FirstName = "Peter", LastName = "Pan" });
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                Command<Person> command = new(context);

                UpdatePersonHandler sut = new(command);

                // act
                UpdatePersonCommand request = new()
                {
                    Id = 1,
                    FirstName = "Tony",
                    LastName = "Stark"
                };
                UpdatePersonResponse result = await sut.Handle(request, CancellationToken.None);

                // assert
                Assert.Equal("Tony", result.Value.FirstName);
                Assert.Equal("Stark", result.Value.LastName);
            }
        }
    }
}
