using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace YEG.Core.Pipelines.Validation
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<ValidationFailure> validationFailures =  ValidationTool.Validate(_validators, request);
            if (validationFailures.Any())
                throw new ValidationException(validationFailures);

            return next();
        }
    }
}
