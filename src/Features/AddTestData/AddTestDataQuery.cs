using MediatR;

namespace Features.AddTestData;

public record AddTestDataQuery : IRequest<bool>;
