using System;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public sealed class PersonDatabaseFixture : IDisposable
    {
        public PersonDatabaseFixture()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Test").Options;
            Context = new DataContext(options);
            Context.Persons.Add(new Person
                                {
                                    FirstName = "Dennis", LastName = "Schreur"
                                });
            Context.Persons.Add(new Person
                                {
                                    FirstName = "Tess", LastName = "Schreur"
                                });
            Context.Persons.Add(new Person
                                {
                                    FirstName = "Daan", LastName = "Schreur"
                                });
            Context.Persons.Add(new Person
                                {
                                    FirstName = "Peter", LastName = "Pan"
                                });
            Context.SaveChanges();
        }

        public DataContext Context { get; }

        public void Dispose() => Context.Dispose();
    }
}
