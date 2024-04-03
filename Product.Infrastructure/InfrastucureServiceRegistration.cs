using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Contracts.Services;
using Product.Infrastructure.Services;
using StackExchange.Redis;

namespace Product.Infrastructure;

public static class InfrastucureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .SetConnection(configuration)         
            .AddServices();

    private static IServiceCollection SetConnection(this IServiceCollection services, IConfiguration configuration)
        => services            
            .AddSingleton<IDatabase>(cfg =>
            {
                var redisConnection =
                    ConnectionMultiplexer
                        .Connect(configuration.GetConnectionString("RedisConnectionString") ?? string.Empty);
                return redisConnection.GetDatabase();
            });  

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddTransient<ICacheService, CacheService>();
                  

}
