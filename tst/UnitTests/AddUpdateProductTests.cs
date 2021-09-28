using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using DataAccess;
using Features.AddProduct;
using Features.UpdateProduct;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class AddUpdateProductTest
    {
        [Fact]
        public async Task AddProduct()
        {
            // arrange
            List<Product> testdata = new();

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(nameof(AddProduct)).Options;
            using var context = new DataContext(options);
            context.SaveChanges();
            Command<Product> command = new(context);

            AddProductHandler sut = new(command);

            // act
            AddProductCommand request = new()
            {
                Name = "Meloen"
            };
            AddProductResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            // arrange
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(nameof(UpdateProduct)).Options;
            using (var context = new DataContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "Appel" });
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                Command<Product> command = new(context);

                UpdateProductHandler sut = new(command);

                // act
                UpdateProductCommand request = new()
                {
                    Id = 1,
                    Name = "Meloen"
                };
                UpdateProductResponse result = await sut.Handle(request, CancellationToken.None);

                // assert
                Assert.Equal("Meloen", result.Value.Name);
            }
        }
    }
}
