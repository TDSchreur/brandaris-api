using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using Features.GetProduct;
using MockQueryable.Moq;
using Xunit;

namespace UnitTests
{
    public class GetProductTests
    {
        public GetProductTests()
        {
            List<Product> testdata = new()
            {
                new Product { Id = 1, Name = "Appel" },
                new Product { Id = 2, Name = "Banaan" },
                new Product { Id = 3, Name = "Peer" },
                new Product { Id = 4, Name = "Sinasappel" }
            };

            Query = new Query<Product>(testdata.AsQueryable().BuildMock().Object);
        }

        public Query<Product> Query { get; init; }

        [Theory]
        [InlineData("Banaan", new int[] { }, 1)]
        [InlineData("Peer", new int[] { }, 1)]
        [InlineData("", new[] { 1, 4 }, 2)]
        public async Task GetPersons(string name, int[] productIds, int expectedResults)
        {
            // arrange
            GetProductsHandler sut = new(Query);

            // act
            GetProductsQuery request = new()
            {
                Name = name,
                ProductIds = productIds
            };
            GetProductsResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(expectedResults, result.Value.Count);
        }

        [Theory]
        [InlineData(2, "Banaan")]
        [InlineData(3, "Peer")]
        public async Task GetProduct(int id, string name)
        {
            // arrange
            GetProductHandler sut = new(Query);

            // act
            GetProductQuery request = new()
            {
                Id = id
            };
            GetProductResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Equal(name, result.Value.Name);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnNull()
        {
            // arrange
            GetProductHandler sut = new(Query);

            // act
            GetProductQuery request = new()
            {
                Id = int.MaxValue
            };
            GetProductResponse result = await sut.Handle(request, CancellationToken.None);

            // assert
            Assert.Null(result.Value);
        }
    }
}
