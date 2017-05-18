using MediatR;

namespace SFA.DAS.MI.Application.Queries.GetDeclarations
{
    public class GetDeclarationsRequest : IAsyncRequest<GetDeclarationsResponse>
    {
        public string EmpRef { get; set; }
    }
}
