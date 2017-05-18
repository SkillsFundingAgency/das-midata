using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.MI.Application.Validation;

namespace SFA.DAS.MI.Application.Queries.GetFractions
{
    public class GetFractionsValidator : IValidator<GetFractionsRequest>
    {
        public Task<ValidationResult> ValidateAsync(GetFractionsRequest item)
        {
            throw new NotImplementedException();
        }
    }
}
