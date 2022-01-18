using MediatR;

namespace Features.GetConfig;

public record GetConfigQuery : IRequest<GetConfigResponse>;
