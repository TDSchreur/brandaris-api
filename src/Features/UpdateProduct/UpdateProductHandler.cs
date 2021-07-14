using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using Features.Models;
using MediatR;

namespace Features.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly ICommand<Product> _command;

        public UpdateProductHandler(ICommand<Product> command) => _command = command;

        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Id = request.Id,
                Name = request.Name
            };
            _command.Update(
                product,
                x => x.Name);

            await _command.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse(new ProductModel
            {
                Id = product.Id,
                Name = product.Name
            });
        }
    }
}
