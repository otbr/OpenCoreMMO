using Microsoft.Extensions.DependencyInjection.Extensions;
using NeoServer.Web.API.Helpers;

namespace NeoServer.Web.API.IoC.Modules;

public static class ServiceApiInjection
{
    public static IServiceCollection AddServicesApi(this IServiceCollection services)
    {
        var scanAssemblies = AssemblyHelper.GetAllAssemblies();

        foreach (var assembly in scanAssemblies)
        {
            try
            {
                var definedTypeInterfacess = assembly.DefinedTypes
                    .Where(x => x.IsInterface)
                    .Where(c => c.FullName?.EndsWith("ApiService") ?? false);

                var definedTypesClasses = assembly.DefinedTypes
                    .Where(x => x.IsClass)
                    .Where(c => c.FullName?.EndsWith("ApiService") ?? false);

                if (definedTypeInterfacess.Any())
                {
                    foreach (var typeInfo in definedTypeInterfacess)
                    {
                        var types = definedTypesClasses
                                .Where(x => typeInfo.IsAssignableFrom(x));

                        foreach (var type in types)
                            services.TryAdd(new ServiceDescriptor(typeInfo, type, ServiceLifetime.Scoped));
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        //var servicesAndRepositories = scanAssemblies
        //    .Where(x => x.DefinedTypes != null)
        //    .SelectMany(o => o.DefinedTypes
        //        .Where(x => x.IsInterface)
        //        .Where(c => c.FullName?.EndsWith("ApiService") ?? false)
        //    );

        //foreach (var typeInfo in servicesAndRepositories)
        //{
        //    var types = scanAssemblies
        //        .SelectMany(o => o.DefinedTypes
        //            .Where(x => x.IsClass)
        //            .Where(x => typeInfo.IsAssignableFrom(x))
        //        );

        //    foreach (var type in types)
        //        services.TryAdd(new ServiceDescriptor(typeInfo, type, ServiceLifetime.Scoped));
        //}

        return services;
    }
}