using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Interfaces;
using SFA.DAS.MI.Domain.Models.Fractions;

namespace SFA.DAS.MI.Infrastructure.Data
{
    public class FractionRepository : BaseRepository, IFractionRepository
    {
        public FractionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<FractionCalculation>> GetFractionsByEmpref(string empRef)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);

                return await c.QueryAsync<FractionCalculation>(
                    sql: "[GetFractions_ByEmpRef]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return result.ToList();
        }
    }
}