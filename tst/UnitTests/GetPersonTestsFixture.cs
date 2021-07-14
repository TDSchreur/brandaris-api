using System;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public sealed class GetPersonTestsFixture : IDisposable
    {
        public GetPersonTestsFixture()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(nameof(GetPersonTestsFixture)).Options;
            Context = new DataContext(options);
            Context.Persons.Add(new Person { Id = 1, FirstName = "Dennis", LastName = "Schreur" });
            Context.Persons.Add(new Person { Id = 2, FirstName = "Tess", LastName = "Schreur" });
            Context.Persons.Add(new Person { Id = 3, FirstName = "Daan", LastName = "Schreur" });
            Context.Persons.Add(new Person { Id = 4, FirstName = "Peter", LastName = "Pan" });
            Context.SaveChanges();
        }

        public DataContext Context { get; }

        public void Dispose() => Context.Dispose();
    }
}
