using FluentValidation;
using FluentValidation.Results;

namespace YEG.Core.Pipelines.Validation
{
    public class ValidationTool
    {
        public static List<ValidationFailure> Validate(IEnumerable<IValidator> validator, object entity)
        {
            ValidationContext<object> context = new(entity);
            List<ValidationFailure> failures = validator
                                               .Select(validator => validator.Validate(context))
                                               .SelectMany(result => result.Errors)
                                               .Where(failure => failure != null)
                                               .ToList();
            return failures;
        }
    }
}
