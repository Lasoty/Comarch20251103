using System.Reflection;
using Bibliotekarz.Client.ClientServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Bibliotekarz.Client.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        return builder;
    }


    public static WebAssemblyHostBuilder AddApiClients(this WebAssemblyHostBuilder builder)
    {
        var clientInterface = typeof(IApiClient);

        var types = clientInterface.Assembly.GetExportedTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name}"),
                Implementation = t
            })
            .Where(t => t.Service != null && typeof(IApiClient).IsAssignableFrom(t.Service));

        foreach (var type in types)
        {
            Console.WriteLine($"Registering: {type.Service.Name} -> {type.Implementation.Name}");
            builder.Services.AddTransient(type.Service, type.Implementation);
        }

        return builder;
    }

    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        var clientInterface = typeof(IApiClient);

        var types = clientInterface.Assembly.GetExportedTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name}"),
                Implementation = t
            })
            .Where(t => t.Service != null && typeof(IApiClient).IsAssignableFrom(t.Service));

        foreach (var type in types)
        {
            Console.WriteLine($"Registering: {type.Service.Name} -> {type.Implementation.Name}");
            services.AddTransient(type.Service, type.Implementation);
        }

        return services;
    }
}
