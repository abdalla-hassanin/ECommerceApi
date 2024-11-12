using ECommerceApi.Data.Options;
using Serilog;

namespace ECommerceApi.Api.Configuration;

public static class ApplicationConfiguration
{
    // public static async Task ConfigureServices(this WebApplicationBuilder builder)
    // {
    //     var (services, secrets) = await builder.SetupConfiguration();
    //     
    //     builder.ConfigureLogging(secrets);
    //     
    //     services.AddApiServices()
    //         .AddSecurityServices()
    //         .AddDataServices()
    //         .AddApplicationServices(builder.Configuration);
    // }
    public static async Task ConfigureServices(this WebApplicationBuilder builder)
    {
        var services= await builder.SetupConfiguration();
        
        builder.ConfigureLogging();
        
        services.AddApiServices()
            .AddSecurityServices()
            .AddDataServices()
            .AddApplicationServices(builder.Configuration);
    }

    private static async Task<IServiceCollection> SetupConfiguration(
        this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        
        return await builder.Services.AddAppSettingsConfiguration(
            builder.Configuration);
    }
    // private static async Task<(IServiceCollection, SecretOptions)> SetupConfiguration(
    //     this WebApplicationBuilder builder)
    // {
    //     builder.Logging.ClearProviders();
    //     
    //     return await builder.Services.AddAppSettingsConfiguration(
    //         builder.Configuration,
    //         builder.Environment);
    // }

    // private static void ConfigureLogging(
    //     this WebApplicationBuilder builder, 
    //     SecretOptions secrets)
    // {
    //     LoggingSetup.ConfigureLogging(builder, secrets);
    //     Log.Information("Starting application in {Environment}", 
    //         builder.Environment.EnvironmentName);
    // }
    private static void ConfigureLogging(
        this WebApplicationBuilder builder)
    {
        LoggingSetup.ConfigureLogging(builder);
        Log.Information("Starting application in {Environment}", 
            builder.Environment.EnvironmentName);
    }
}
