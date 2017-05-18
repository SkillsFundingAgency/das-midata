using System.Threading.Tasks;
using SFA.DAS.MI.Application.Validation;

namespace SFA.DAS.MI.Application.Queries.GetDeclarations
{
    public class GetDeclarationsValidator : IValidator<GetDeclarationsRequest>
    {
        public Task<ValidationResult> ValidateAsync(GetDeclarationsRequest item)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrEmpty(item.EmpRef))
            {
                validationResult.AddError(nameof(item.EmpRef));
            }

            return Task.FromResult(validationResult);
        }
    }
}
