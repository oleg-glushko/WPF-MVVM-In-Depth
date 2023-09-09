using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop;

public static class ContainerHelper
{
    private readonly static ServiceProvider _container;
    public static ServiceProvider Container => _container;

    static ContainerHelper()
    {
        ServiceCollection services = new();
        ConfigureServices(services);
        _container = services.BuildServiceProvider();
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton((service) => new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false)
           .Build());

        using var scope = services.BuildServiceProvider().CreateScope();
        var configuration = scope.ServiceProvider.GetService<IConfigurationRoot>();
        var connectionString = configuration.GetConnectionString("ZzaConn");

        services.AddDbContext<ZzaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddSingleton<ICustomersRepository, CustomersRepository>();
        //services.AddSingleton<IOrdersRepository, OrdersRepository>();
    }
}
