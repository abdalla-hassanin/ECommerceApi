using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Commands;

public record CreateProductImageCommand(
    string ProductId,
    string? VariantId,
    IFormFile ImageFile,
    string AltText,
    int DisplayOrder
) : IRequest<ApiResponse<ProductImageDto>>;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ProductId must be a valid ULID.");
            
        RuleFor(x => x.VariantId)
            .Length(26)
            .When(x => x != null)
            .WithMessage("VariantId must be a valid ULID when provided.");
            
        RuleFor(x => x.ImageFile)
            .NotNull()
            .WithMessage("ImageFile is required.");
            
        RuleFor(x => x.AltText)
            .NotEmpty()
            .WithMessage("AltText is required.");
            
        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DisplayOrder must be greater than or equal to 0.");
    }
}

public class CreateProductImageHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<CreateProductImageHandler> logger) : IRequestHandler<CreateProductImageCommand, ApiResponse<ProductImageDto>>
{
    public async Task<ApiResponse<ProductImageDto>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new product image for product {ProductId}", request.ProductId);
        try
        {
            var image = mapper.Map<Data.Entities.ProductImage>(request);
            var createdImage = await productImageService.CreateImageAsync(image,request.ImageFile, cancellationToken);
            logger.LogInformation("Product image created successfully for product {ProductId}", request.ProductId);
            return ApiResponse<ProductImageDto>.Factory.Created(mapper.Map<ProductImageDto>(createdImage));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating product image for product {ProductId}", request.ProductId);
            throw;
        }
    }
}
