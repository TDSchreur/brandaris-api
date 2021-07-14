using Data.Entities;
using DataAccess;
using FluentValidation;

namespace Features.UpdatePerson
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdateProductCommandValidator(IQuery<Person> query)
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
            RuleFor(x => x).CustomAsync(async (command, context, cancellationToken) =>
            {
                bool exists = await query.AnyAsync(x => x.Id == command.Id, cancellationToken);

                if (!exists)
                {
                    context.AddFailure("User not found");
                }
            });
        }
    }
}
