using ECommerceApi.Core.Base.MiddleWare;

namespace ECommerceApi.Api.Configuration;

public static class ApplicationPipeline
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
            c.RoutePrefix = string.Empty;
            c.DisplayRequestDuration();
        });

        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
    }
}
