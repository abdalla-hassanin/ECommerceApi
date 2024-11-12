using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.Base.MiddleWare
{
    public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any()) return await next();
            
            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count == 0) return await next();
            logger.LogWarning("Validation failed for {RequestType}. Errors: {@ValidationErrors}", 
                typeof(TRequest).Name, failures);

            throw new ValidationException(
                $"Validation failed for {typeof(TRequest).Name}",
                failures);

        }
    }
}