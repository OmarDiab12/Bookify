using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Bookify.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validator.Any()) return await next();
        
        var context =  new ValidationContext<TRequest>(request);

        var validationErrors = _validator.Select(v => v.Validate(context)).Where(vr => vr.Errors.Any())
            .SelectMany(vr => vr.Errors)
            .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage)).ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }
        
        return await next();
    }
}