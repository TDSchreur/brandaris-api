using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using DataAccess;
using Features.GetProduct;
using Xunit;

namespace UnitTests
{
    public class GetProductTests : IClassFixture<GetProductTestsFixture>
    {
        public GetProductTests(GetProductTestsFixture fixture) => Context = fixture.Context;

        public DataContext Context { get; }

        [Theory]
        [InlineData("Banaan", new int[] { }, 1)]
        [InlineData("Peer", new int[] { }, 1)]
        [InlineData("", new int[] { 1, 4 }, 2)]
        ////[InlineData("Dennis", "Schreur", 1)]
        ////[InlineData("", "Pan", 1)]
        ////[InlineData("", "", 4)]
        public async Task GetPersons(string name, int[] productIds, int expectedResults)
        {
            // arrange
            Query<Product> query = new(Context);

            GetProductsHandler sut = new(query);

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
            Query<Product> query = new(Context);

            GetProductHandler sut = new(query);

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
            Query<Product> query = new(Context);

            GetProductHandler sut = new(query);

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
