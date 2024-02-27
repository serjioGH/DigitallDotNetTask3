﻿namespace Cloth.Persistence.PostgreSQL.Extensions;

using Cloth.Application.Interfaces;
using Cloth.Application.Interfaces.Repositories;
using Cloth.Infrastructure;
using Cloth.Infrastructure.Configuration;
using Cloth.Persistence.PostgreSQL.Context;
using Cloth.Persistence.Ef.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cloth.Persistence.PostgreSQL.Repositories;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterEfPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        services.AddDbContext<DbContext, ClothInventoryDbContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseNpgsql(configuration.GetConnectionString("ClothInventoryNpgsqlDbConnection"));
        });

        services.Configure<MockyHttpConfiguration>(configuration.GetSection("MockyClient"));
        services.AddScoped<IClothRepository, ClothRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IBasketLineRepository, BasketLineRepository>();
        services.AddScoped<ISizeRepository, SizeRepository>();
        services.AddScoped<IClothSizeRepository, ClothSizeRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddHttpClient<MockyHttpServiceClient>();
        services.AddScoped<IMockyHttpServiceClient, MockyHttpServiceClient>();

        return services;
    }
}