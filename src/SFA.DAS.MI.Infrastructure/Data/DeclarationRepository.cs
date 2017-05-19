using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Entities;
using SFA.DAS.MI.Domain.Interfaces;

namespace SFA.DAS.MI.Infrastructure.Data
{
    public class DeclarationRepository : BaseRepository, IDeclarationRepository
    {
        public DeclarationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Declaration>> GetDeclarationsByEmpref(string empRef)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@empRef", empRef, DbType.String);

                return await c.QueryAsync<Declaration>(
                    sql: "[GetDeclarations_ByEmpRef]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return result.ToList();
        }
    }
}
