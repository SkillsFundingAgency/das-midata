using System;
using System.Collections.Generic;
using SFA.DAS.MI.Domain.Models.MiData;

namespace SFA.DAS.MI.Application.Queries.GetMiData
{
    public class GetMiDataResponse
    {
        public List<MiRow> Data { get; set; }
        public DateTime MiFeedDate { get; set; }

    }
}