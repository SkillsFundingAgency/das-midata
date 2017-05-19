using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.MI.Application.Validation;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Models.Fractions;

namespace SFA.DAS.MI.Application.Queries.GetFractions
{
    public class GetFractionsQueryHandler : IAsyncRequestHandler<GetFractionsRequest, GetFractionsResponse>
    {
        private readonly IValidator<GetFractionsRequest> _validator;
        private readonly IFractionRepository _fractionRepository;

        public GetFractionsQueryHandler(IValidator<GetFractionsRequest> validator, IFractionRepository fractionRepository)
        {
            _validator = validator;
            _fractionRepository = fractionRepository;
        }

        public async Task<GetFractionsResponse> Handle(GetFractionsRequest message)
        {
            var validationResult = await _validator.ValidateAsync(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            var result = await _fractionRepository.GetFractionsByEmpref(message.EmpRef);

            var fractionCalculations = result.Select(fraction => new FractionCalculation
            {
                CalculatedAt = fraction.DateCalculated.ToString("YYYY-mm-dd"),
                Fractions = new List<Fraction>
                {
                    new Fraction
                    {
                        Region = "England",
                        Value = fraction.Amount.ToString()
                    }
                }
            }).ToList();

            return new GetFractionsResponse {Fractions = new EnglishFractionDeclarations {Empref = message.EmpRef,FractionCalculations = fractionCalculations } };
        }
    }
}
