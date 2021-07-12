using System;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public sealed class GetProductTestsFixture : IDisposable
    {
        public GetProductTestsFixture()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("Products").Options;
            Context = new DataContext(options);
            Context.Products.Add(new Product { Id = 1, Name = "Appel" });
            Context.Products.Add(new Product { Id = 2, Name = "Banaan" });
            Context.Products.Add(new Product { Id = 3, Name = "Peer" });
            Context.Products.Add(new Product { Id = 4, Name = "Sinasappel" });
            Context.SaveChanges();
        }

        public DataContext Context { get; }

        public void Dispose() => Context.Dispose();
    }
}
