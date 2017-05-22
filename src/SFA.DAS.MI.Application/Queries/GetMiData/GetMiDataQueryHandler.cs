using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using SFA.DAS.MI.Domain.Models.MiData;

namespace SFA.DAS.MI.Application.Queries.GetMiData
{
    public class GetMiDataQueryHandler : IRequestHandler<GetMiDataRequest, GetMiDataResponse>
    {
        public GetMiDataResponse Handle(GetMiDataRequest message)
        {
            var csv = new CsvReader(File.OpenText(message.FilePath));
            csv.Configuration.Delimiter = "|";
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.RegisterClassMap<MiRowClassMap>();
            var rows = csv.GetRecords<MiRow>();
            return new GetMiDataResponse() {Data = rows.ToList(), MiFeedDate = DateTime.Now};
        }
    }

    public sealed class MiRowClassMap : CsvClassMap<MiRow>
    {
        public MiRowClassMap()
        {
            Map(m => m.EmpRef).Index(2);
            Map(m => m.PayrollYear).Index(0);
            Map(m => m.PayrollMonth).Index(1);
            Map(m => m.LevyDueYtd).Index(5);
            Map(m => m.LevyAllowanceForYear).Index(6); // TRANSPOSED IN FEED
            Map(m => m.CessationDate).Index(16);
            Map(m => m.FractionAmount).Index(7);
           


        }
    }
}
