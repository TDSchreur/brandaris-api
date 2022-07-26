using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using FluentValidation;

namespace Brandaris.Features.ApprovePerson
{
    public class ApprovePersonCommandValidator : AbstractValidator<ApprovePersonCommand>
    {
        public ApprovePersonCommandValidator(IQuery<Person> personQuery,
                                             IQuery<PersonPreCheck> personPreCheckQuery)
        {
            RuleFor(x => x).CustomAsync(async (command, context, cancellationToken) =>
            {
                PersonPreCheck personPreCheck = await personPreCheckQuery.FirstOrDefaultAsync(x => x.Id == command.Id);

                bool exists = await personQuery.AnyAsync(x => x.FirstName == personPreCheck.FirstName &&
                                                              x.LastName == personPreCheck.LastName,
                                                         cancellationToken);

                if (exists)
                {
                    context.AddFailure("Combination firstname / lastname must be unique");
                }
            });
        }
    }
}
