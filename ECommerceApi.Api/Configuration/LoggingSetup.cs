using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using ECommerceApi.Data.Options;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;

namespace ECommerceApi.Api.Configuration;

    public static class LoggingSetup
    {
        public static void ConfigureLogging(
            WebApplicationBuilder builder)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration);

            if (builder.Environment.IsDevelopment())
            {
                ConfigureDevelopmentLogging(loggerConfiguration);
            }
            else
            {
                ConfigureProductionLogging(loggerConfiguration, builder);
            }

            builder.Host.UseSerilog(loggerConfiguration.CreateLogger());
        }
        // public static void ConfigureLogging(
        //     WebApplicationBuilder builder,
        //     SecretOptions secretOptions)
        // {
        //     var loggerConfiguration = new LoggerConfiguration()
        //         .ReadFrom.Configuration(builder.Configuration);
        //
        //     if (builder.Environment.IsDevelopment())
        //     {
        //         ConfigureDevelopmentLogging(loggerConfiguration);
        //     }
        //     else
        //     {
        //         ConfigureProductionLogging(loggerConfiguration, secretOptions);
        //     }
        //
        //     builder.Host.UseSerilog(loggerConfiguration.CreateLogger());
        // }

        private static void ConfigureDevelopmentLogging(LoggerConfiguration loggerConfiguration)
        {
            // Configure Seq or any other development logging
            Log.Information("Logging configured for Development environment using Seq");
        }

        private static void ConfigureProductionLogging(LoggerConfiguration loggerConfiguration,
            WebApplicationBuilder builder)
        {
            var secretOptions = builder.Configuration.GetSection(SecretOptions.SectionName)
                .Get<SecretOptions>() ?? throw new InvalidOperationException("Failed to get secrets");
            var credentials = new BasicAWSCredentials(
                secretOptions.AWS.AccessKey,
                secretOptions.AWS.SecretKey
            );

            var cloudWatchLogsClient = new AmazonCloudWatchLogsClient(
                credentials,
                RegionEndpoint.GetBySystemName(secretOptions.AWS.Region)
            );

            var cloudWatchSinkOptions = new CloudWatchSinkOptions
            {
                LogGroupName = $"/ecommerce-api/{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}/logs",
                TextFormatter = new JsonFormatter(),
                MinimumLogEventLevel = LogEventLevel.Information,
                BatchSizeLimit = 100,
                QueueSizeLimit = 10000,
                Period = TimeSpan.FromSeconds(10),
                CreateLogGroup = true,
                LogStreamNameProvider = new DefaultLogStreamProvider()
            };

            loggerConfiguration.WriteTo.AmazonCloudWatch(
                cloudWatchSinkOptions,
                cloudWatchLogsClient
            );

            Log.Information("Logging configured for Production environment using CloudWatch");
        }
    }
