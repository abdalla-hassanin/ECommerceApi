using ECommerceApi.Api.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    await builder.ConfigureServices();
    var app = builder.Build();
    app.ConfigurePipeline();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}