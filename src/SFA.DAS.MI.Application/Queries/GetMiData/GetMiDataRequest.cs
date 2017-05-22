using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.MI.Application.Queries.GetMiData
{
    public class GetMiDataRequest : IRequest<GetMiDataResponse>
    {
        public string FilePath { get; set; }
    }
}
