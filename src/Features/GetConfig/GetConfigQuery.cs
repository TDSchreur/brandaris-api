using MediatR;

namespace Brandaris.Features.GetConfig;

public record GetConfigQuery : IRequest<GetConfigResponse>;
