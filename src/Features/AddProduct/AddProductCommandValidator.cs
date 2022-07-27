using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using FluentValidation;

namespace Brandaris.Features.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator(IQuery<Product> query)
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(50);
        RuleFor(x => x)
           .CustomAsync(async (command, context, cancellationToken) =>
            {
                bool exists = await query.AnyAsync(x => x.Name == command.Name,
                                                   cancellationToken);

                if (exists)
                {
                    context.AddFailure("Productname must be unique");
                }
            });
    }
}