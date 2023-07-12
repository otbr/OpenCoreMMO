using NeoServer.Web.API.Helpers;

namespace NeoServer.Web.API.IoC.Modules;

public static class AutoMapperInjection
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        var scanAssemblies = AssemblyHelper.GetAllAssemblies();

        var profiles = scanAssemblies
            .Where(c => c.FullName?.EndsWith("Profile") ?? false)
            .SelectMany(o => o.DefinedTypes
                .Where(x => x.IsClass)
            );

        foreach (var profile in profiles)
            services.AddAutoMapper(profile);

        return services;
    }
}