using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using Features.AddProduct;
using Features.UpdateProduct;
using Moq;
using Xunit;

namespace UnitTests;

public class AddUpdateProductTest
{
    [Fact]
    public async Task AddProduct()
    {
        // arrange
        const string meloen = nameof(meloen);
        AddProductCommand request = new(meloen);

        Mock<ICommand<Product>> qm = new(MockBehavior.Strict);
        qm.Setup(x => x.Add(It.IsAny<Product>()))
          .Callback((Product[] p) => p[0].Id = 1);
        qm.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(1);

        AddProductHandler sut = new(qm.Object);

        // act
        AddProductResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(1, result.Value.Id);
    }

    [Fact]
    public async Task UpdateProduct()
    {
        // arrange
        const string meloen = nameof(meloen);
        UpdateProductCommand request = new()
        {
            Id = 1, Name = meloen
        };
        string newName = string.Empty;

        Mock<ICommand<Product>> qm = new(MockBehavior.Strict);
        qm.Setup(x => x.Update(It.IsAny<Product>(),
                               It.IsAny<Expression<Func<Product, string>>>()))
          .Callback((Product p, Expression<Func<Product, string>>[] _) => newName = p.Name);
        qm.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(1);

        UpdateProductHandler sut = new(qm.Object);

        // act
        UpdateProductResponse result = await sut.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(meloen, result.Value.Name);
        Assert.Equal(meloen, newName);
    }
}
