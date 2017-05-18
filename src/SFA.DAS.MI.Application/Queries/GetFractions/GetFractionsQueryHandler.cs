using System.Threading.Tasks;
using MediatR;
using SFA.DAS.MI.Application.Validation;
using SFA.DAS.MI.Domain.Data;
using SFA.DAS.MI.Domain.Models.Fractions;

namespace SFA.DAS.MI.Application.Queries.GetFractions
{
    public class GetFractionsQueryHandler : IAsyncRequestHandler<GetFractionsRequest, GetFractionsResponse>
    {
        private readonly IValidator<GetFractionsRequest> _validator;
        private readonly IFractionsRepository _fractionsRepository;

        public GetFractionsQueryHandler(IValidator<GetFractionsRequest> validator, IFractionsRepository fractionsRepository)
        {
            _validator = validator;
            _fractionsRepository = fractionsRepository;
        }

        public async Task<GetFractionsResponse> Handle(GetFractionsRequest message)
        {
            var validationResult = await _validator.ValidateAsync(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            var result = await _fractionsRepository.GetFractionsByEmpref(message.EmpRef);

            return new GetFractionsResponse {Fractions = new EnglishFractionDeclarations {Empref = message.EmpRef,FractionCalculations = result} };
        }
    }
}
