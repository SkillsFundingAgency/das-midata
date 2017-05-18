using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.MI.Domain.Models.Declarations;

namespace SFA.DAS.MI.Domain.Data.Repositories
{
    public interface IDeclarationRepository
    {
        Task<List<Declaration>> GetDeclarationsByEmpref(string empRef);
    }
}