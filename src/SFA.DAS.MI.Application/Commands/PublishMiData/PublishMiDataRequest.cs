using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.MI.Domain.Models.MiData;

namespace SFA.DAS.MI.Application.Commands.PublishMiData
{
    public class PublishMiDataRequest : IAsyncNotification
    {
        public MiRow Data { get; set;  }
    }
}
