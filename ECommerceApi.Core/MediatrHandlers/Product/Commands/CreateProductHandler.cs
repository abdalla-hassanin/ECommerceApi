using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Commands;

public record CreateProductCommand(
    string Name,
    string Description,
    string ShortDescription,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    string Status
) : IRequest<ApiResponse<ProductDto>>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("Short description is required.")
            .MaximumLength(500).WithMessage("Short description must not exceed 500 characters.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.CompareAtPrice)
            .GreaterThan(0).When(x => x.CompareAtPrice.HasValue)
            .WithMessage("Compare at price must be greater than 0 when provided.");
        RuleFor(x => x.CostPrice)
            .GreaterThan(0).When(x => x.CostPrice.HasValue)
            .WithMessage("Cost price must be greater than 0 when provided.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
    }
}

public class CreateProductHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<CreateProductHandler> logger) : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
{
    public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new product with name: {ProductName}", request.Name);
        try
        {
            var product = mapper.Map<Data.Entities.Product>(request);
            Data.Entities.Product createdProduct = await productService.CreateProductAsync(product, cancellationToken);
            logger.LogInformation("Product created successfully with ID: {ProductId}", createdProduct.ProductId);
            return ApiResponse<ProductDto>.Factory.Created(mapper.Map<ProductDto>(createdProduct));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating product with name: {ProductName}", request.Name);
            throw;
        }
    }
}