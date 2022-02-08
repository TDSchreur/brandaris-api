using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using FluentValidation;

namespace Brandaris.Features.AddPerson;

public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
    public AddPersonCommandValidator(IQuery<Person> query)
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
        RuleFor(x => x).CustomAsync(async (command, context, cancellationToken) =>
        {
            bool exists = await query.AnyAsync(x => x.FirstName == command.FirstName &&
                                                    x.LastName == command.LastName,
                                               cancellationToken);

            if (exists)
            {
                context.AddFailure("Combination firstname / lastname must be unique");
            }
        });
    }
}
