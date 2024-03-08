﻿using NeoServer.Web.API.Helpers;

namespace NeoServer.Web.API.IoC.Modules;

public static class AutoMapperInjection
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        var scanAssemblies = AssemblyHelper.GetAllAssemblies();

        var profiles = scanAssemblies
            .SelectMany(o => o.GetTypes()
                .Where(x => x.IsClass)
                .Where(c => c.FullName?.EndsWith("Profile") ?? false)
            ).ToArray();

        services.AddAutoMapper(profiles);

        return services;
    }
}