using System.Collections.Generic;
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
            List<PersonModel> persons = await _query.Where(x => !request.HasFirstName || x.FirstName == request.FirstName)
                                                    .Where(x => !request.HasLastName || x.LastName == request.LastName)
                                                    .Select(x => new PersonModel
                                                                 {
                                                                     Id = x.Id,
                                                                     FirstName = x.FirstName,
                                                                     LastName = x.LastName
                                                                 })
                                                    .ToListAsync(cancellationToken);

            return new GetPersonResponse(persons);
        }
    }
}
