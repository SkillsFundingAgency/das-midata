using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.MI.Application.Queries.GetFractions
{
    public class GetFractionsRequest : IAsyncRequest<GetFractionsResponse>
    {
        public string EmpRef { get; set; }
    }
}
