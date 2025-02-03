using Application.Attributes;
using Core;
using Core.Enums;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.AuthHandlers;
using Infrastructure.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Persistance;
using Persistance.Attributes;
using Persistance.Repositories;
using System.Reflection;
using System.Text;


namespace API.Configurations;

public static class ServiceConfiguration
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, UserRoleHandler>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.Requirements.Add(new UserRoleRequirement(UserRole.Admin)));

            options.AddPolicy("TeacherPolicy", policy =>
                policy.Requirements.Add(new UserRoleRequirement(UserRole.Teacher, UserRole.Admin)));

            options.AddPolicy("StudentPolicy", policy =>
                policy.Requirements.Add(new UserRoleRequirement(UserRole.Student, UserRole.Admin)));

            options.AddPolicy("AuthorizePolicy", policy =>
               policy.Requirements.Add(new UserRoleRequirement(UserRole.Student, UserRole.Admin, UserRole.Teacher)));

            //options.AddPolicy("AuhtorizePolicy", policy =>
            //{
            //    policy.Requirements.Add(new UserRoleRequirement("Admin"));
            //    policy.Requirements.Add(new UserRoleRequirement("Teacher"));
            //    policy.Requirements.Add(new UserRoleRequirement("Student"));
            //});


        });
    }



    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();

        // Check if JwtSettings is null
        if (jwtSettings == null)
        {
            throw new ArgumentNullException(nameof(jwtSettings), "JWT configuration section is missing or not properly loaded.");
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
    }



    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<UniversityDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString(nameof(UniversityDbContext)),
                o => ConfigureEnum(o)
            ));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddOpenApi();
    }

    public static void ConfigureRepositories<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {

        var contextType = typeof(TContext);

        var repositoryTypesWithAttribute = Assembly.Load(nameof(Persistance)).GetTypes()
           .Where(t => t.GetCustomAttribute<RepositoryAttribute>() != null)
           .ToList();

        var entityTypes = contextType
           .GetProperties()
           .Where(p => p.PropertyType.IsGenericType &&
                      p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
           .Select(p => p.PropertyType.GetGenericArguments()[0]);


        var customRepositories = repositoryTypesWithAttribute
       .ToDictionary(repoType => GetEntityTypeFromRepository(repoType), repoType => repoType);

        // Register generic repository for each entity type
        foreach (var entityType in entityTypes)
        {

            var repositoryType = typeof(Repository<>).MakeGenericType(entityType);
            var iRepositoryType = typeof(IRepository<>).MakeGenericType(entityType);

            if (customRepositories.TryGetValue(entityType, out var customRepoType))
            {
                Console.WriteLine($"{iRepositoryType} {customRepoType}");
                services.AddScoped(iRepositoryType, customRepoType);
            }
            else
            {
                services.AddScoped(iRepositoryType, repositoryType);
            }

            // Register repository with scoped lifetime
            
        }

       

        foreach (var type in repositoryTypesWithAttribute)
        {
            services.AddScoped(type);
        }


    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        var assembly = Assembly.Load(nameof(Application));
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ServiceAttribute>() != null)
            .ToList();
        foreach (var serviceType in serviceTypes)
        {

            services.AddScoped(serviceType);
        }

        services.TryAddEnumerable(ServiceDescriptor.Singleton<IFilterProvider, OverrideFilterProvider>());

    }

    public static NpgsqlDbContextOptionsBuilder ConfigureEnum(NpgsqlDbContextOptionsBuilder builder)
    {
        var enumTypes = Assembly.Load(nameof(Core)).GetTypes()
           .Where(t => t.GetCustomAttribute<EnumAttribute>() != null)
           .ToList();
        var currentBuilder = builder;
        foreach (var enumType in enumTypes)
        {
            var attribute = enumType.GetCustomAttribute<EnumAttribute>();
            if (attribute == null)
            {
                continue;
            }


            var mapEnumMethod = typeof(NpgsqlDbContextOptionsBuilder)
                .GetMethods()
                .FirstOrDefault(
                    m => m.Name == "MapEnum" &&
                    m.GetParameters()[0].ParameterType == typeof(string)
                );

            if (mapEnumMethod == null)
            {
                throw new InvalidOperationException($"MapEnum method not found for type {enumType.Name}");
            }

            // Создаем метод для текущего перечисления
            var genericMapEnumMethod = mapEnumMethod.MakeGenericMethod(enumType);

            // options.ConfigureDataSource()
            currentBuilder = genericMapEnumMethod.Invoke(currentBuilder, [attribute.DatabaseName, null, null]) as NpgsqlDbContextOptionsBuilder;

        }
        return currentBuilder;
    }


    private static Type GetEntityTypeFromRepository(Type repositoryType)
    {
        var baseType = repositoryType.BaseType;
        while (baseType != null)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Repository<>))
            {
                return baseType.GetGenericArguments().FirstOrDefault();
            }
            baseType = baseType.BaseType;
        }
        return null;
    }

}
