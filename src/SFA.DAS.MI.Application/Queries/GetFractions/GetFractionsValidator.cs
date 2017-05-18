using System.Threading.Tasks;
using SFA.DAS.MI.Application.Validation;

namespace SFA.DAS.MI.Application.Queries.GetFractions
{
    public class GetFractionsValidator : IValidator<GetFractionsRequest>
    {
        public Task<ValidationResult> ValidateAsync(GetFractionsRequest item)
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
