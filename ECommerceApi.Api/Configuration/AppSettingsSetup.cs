using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using ECommerceApi.Data.Options;
using Serilog;

namespace ECommerceApi.Api.Configuration;

public static class AppSettingsSetup
{
    public static Task<IServiceCollection>  AddAppSettingsConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        // Register base options
        AddBaseOptions(services, configuration);


        return Task.FromResult(services);
    }
 // public static async Task<(IServiceCollection Services, SecretOptions Secrets)> AddAppSettingsConfiguration(
 //        this IServiceCollection services,
 //        IConfiguration configuration,
 //        IWebHostEnvironment environment)
 //    {
 //        // Register base options
 //        AddBaseOptions(services, configuration);
 //
 //        // Register and get secrets based on environment
 //        var secrets = await AddSecretOptions(services, configuration, environment);
 //
 //        return (services, secrets);
 //    }

    private static void AddBaseOptions(IServiceCollection services, IConfiguration configuration)
    {

        services.AddOptions<ImageProcessingOptions>()
            .Bind(configuration.GetSection(ImageProcessingOptions.SectionName))
            .ValidateDataAnnotations();

        services.AddOptions<SecretOptions>()
            .Bind(configuration.GetSection(SecretOptions.SectionName))
            .ValidateDataAnnotations();
    }

    private static async Task<SecretOptions> AddSecretOptions(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var developmentSecrets = configuration.GetSection(SecretOptions.SectionName)
                .Get<SecretOptions>() ?? throw new InvalidOperationException("Failed to get development secrets");

            services.AddOptions<SecretOptions>()
                .Bind(configuration.GetSection(SecretOptions.SectionName))
                .ValidateDataAnnotations();

            return developmentSecrets;
        }

        return await ConfigureProductionSecrets(services, configuration);
    }

    private static async Task<SecretOptions> ConfigureProductionSecrets(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAWSService<IAmazonSecretsManager>();

        try
        {
            const string secretName = "ecommerce/api/production";
            const string region = "eu-north-1";
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
            var request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to get secret value from AWS Secrets Manager: {e.Message}");
            }

            // var response = await secretsManager.GetSecretValueAsync(new GetSecretValueRequest
            // {
            //     SecretId = secretName
            // });

            var secrets = JsonSerializer.Deserialize<SecretOptions>(response.SecretString)
                          ?? throw new InvalidOperationException(
                              "Failed to deserialize secrets from AWS Secrets Manager");

            services.AddOptions<SecretOptions>()
                .Configure(options =>
                {
                    options.ConnectionStrings = secrets.ConnectionStrings;
                    options.EmailSettings = secrets.EmailSettings;
                    options.JWT = secrets.JWT;
                    options.AWS = secrets.AWS;
                })
                .ValidateDataAnnotations();

            return secrets;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to configure AWS Secrets");
            throw new Exception("Failed to configure AWS Secrets", ex);
        }
    }
}