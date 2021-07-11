using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using MediatR;

namespace Features.GetPerson
{
    public class GetPersonHandler : IRequestHandler<GetPersonQuery, GetPersonResponse>
    {
        private readonly IQuery<Person> _query;

        public GetPersonHandler(IQuery<Person> query) => _query = query;

        public async Task<GetPersonResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            PersonModel person = await _query.Where(x => x.Id == request.Id)
                                                    .Select(x => new PersonModel
                                                    {
                                                        Id = x.Id,
                                                        FirstName = x.FirstName,
                                                        LastName = x.LastName
                                                    })
                                                    .FirstOrDefaultAsync(cancellationToken);

            return new GetPersonResponse(person);
        }
    }
}
