using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.MI.Domain.Entities;

namespace SFA.DAS.MI.Domain.Data.Repositories
{
    public interface IDeclarationRepository
    {
        Task<List<Declaration>> GetDeclarationsByEmpref(string empRef);
        
        Task SaveDeclaration(Declaration declaration);
    }
}