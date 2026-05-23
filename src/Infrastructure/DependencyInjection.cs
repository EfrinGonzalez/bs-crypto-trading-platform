using Microsoft.Extensions.DependencyInjection;

namespace BsCryptoTrading.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Phase 1 foundation: register persistence, kafka, outbox, chain adapters.
        return services;
    }
}
