using Amazon;
using Amazon.CloudFront;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using ECommerceApi.Data.Options;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ECommerceApi.Service;

public static class ModuleServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Entity Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<IProductVariantService, ProductVariantService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IWishlistService, WishlistService>();

        var secrets = services.BuildServiceProvider().GetRequiredService<IOptions<SecretOptions>>().Value;

        // Configure AWS S3 client
        services.AddAWSService<IAmazonS3>(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(
                secrets.AWS.AccessKey,
                secrets.AWS.SecretKey),
            Region = RegionEndpoint.GetBySystemName(secrets.AWS.Region)
        });

        // Configure AWS CloudFront client
        services.AddAWSService<IAmazonCloudFront>(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(
                secrets.AWS.AccessKey,
                secrets.AWS.SecretKey),
            Region = RegionEndpoint.GetBySystemName(secrets.AWS.Region)
        });


        services.AddScoped<IAwsStorageService, AwsStorageService>();
        services.AddScoped<IImageProcessingService, ImageProcessingService>();

        return services;
    }
}