using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinShop.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static bool EnusereDbCreated(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return context.Database.EnsureCreated();
    }
}
