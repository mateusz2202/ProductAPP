using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Contracts.Repositories;
using Product.Presentation.Repositories;

namespace Product.Presentation;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    => services
            .AddDbContext<ProductDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")))
            .AddRepositories();


    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

}
