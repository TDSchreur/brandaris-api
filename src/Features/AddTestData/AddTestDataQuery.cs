using MediatR;

namespace Brandaris.Features.AddTestData;

public record AddTestDataQuery : IRequest<bool>;
