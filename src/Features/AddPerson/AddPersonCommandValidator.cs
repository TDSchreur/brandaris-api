using Data.Entities;
using DataAccess;
using FluentValidation;

namespace Features.AddPerson
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator(IQuery<Person> query)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
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
}
