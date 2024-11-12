using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Commands;

public record UpdateProductImageCommand(
    string ImageId,
    IFormFile? ImageFile,
    string AltText,
    int DisplayOrder
) : IRequest<ApiResponse<ProductImageDto>>;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(x => x.ImageId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ImageId must be a valid ULID.");
            
        RuleFor(x => x.AltText).NotEmpty().WithMessage("AltText is required.");
        RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0).WithMessage("DisplayOrder must be greater than or equal to 0.");
    }
}

public class UpdateProductImageHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<UpdateProductImageHandler> logger) : IRequestHandler<UpdateProductImageCommand, ApiResponse<ProductImageDto>>
{
    public async Task<ApiResponse<ProductImageDto>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product image with ID {ImageId}", request.ImageId);
        try
        {
            var existingImage = await productImageService.GetImageByIdAsync(request.ImageId, cancellationToken);
            if (existingImage is null)
            {
                logger.LogWarning("Product image with ID {ImageId} not found", request.ImageId);
                return ApiResponse<ProductImageDto>.Factory.NotFound("Product image not found");
            }

            mapper.Map(request, existingImage);
            var updatedImage = await productImageService.UpdateImageAsync(existingImage,request.ImageFile, cancellationToken);
            logger.LogInformation("Product image with ID {ImageId} updated successfully", request.ImageId);
            return ApiResponse<ProductImageDto>.Factory.Success(mapper.Map<ProductImageDto>(updatedImage));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating product image with ID {ImageId}", request.ImageId);
            throw;
        }
    }
}
