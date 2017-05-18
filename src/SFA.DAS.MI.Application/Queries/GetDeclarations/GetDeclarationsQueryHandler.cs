using System.Threading.Tasks;
using MediatR;
using SFA.DAS.MI.Application.Validation;
using SFA.DAS.MI.Domain.Data;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Models.Declarations;

namespace SFA.DAS.MI.Application.Queries.GetDeclarations
{
    public class GetDeclarationsQueryHandler : IAsyncRequestHandler<GetDeclarationsRequest, GetDeclarationsResponse>
    {
        private readonly IValidator<GetDeclarationsRequest> _validator;
        private readonly IDeclarationRepository _declarationRepository;

        public GetDeclarationsQueryHandler(IValidator<GetDeclarationsRequest> validator, IDeclarationRepository declarationRepository)
        {
            _validator = validator;
            _declarationRepository = declarationRepository;
        }

        public async Task<GetDeclarationsResponse> Handle(GetDeclarationsRequest message)
        {
            var validationResult = await _validator.ValidateAsync(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            var result = await _declarationRepository.GetDeclarationsByEmpref(message.EmpRef);


            return new GetDeclarationsResponse {Declarations = new LevyDeclarations {EmpRef = message.EmpRef, Declarations = result} };
        }
    }
}
