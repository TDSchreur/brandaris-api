using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using FluentValidation;

namespace Brandaris.Features.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IQuery<Product> query)
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(50);
        RuleFor(x => x)
           .CustomAsync(async (command, context, cancellationToken) =>
            {
                bool exists = await query.AnyAsync(x => x.Id == command.Id, cancellationToken);

                if (!exists)
                {
                    context.AddFailure("Product not found");
                }
            });
    }
}