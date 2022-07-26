using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.AddProduct;

public class AddProductHandler : IRequestHandler<AddProductCommand, AddProductResponse>
{
    private readonly ICommand<Product> _command;

    public AddProductHandler(ICommand<Product> command)
    {
        _command = command;
    }

    public async Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Name = request.Name
        };
        _command.Add(product);

        await _command.SaveChangesAsync(cancellationToken);

        return new AddProductResponse(new ProductModel(product.Id, product.Name));
    }
}