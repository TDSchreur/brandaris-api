using Features.AddTestData;
using MediatR;

namespace Brandaris.Api;

public class MigratorHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public MigratorHostedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new AddTestDataQuery(), cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
