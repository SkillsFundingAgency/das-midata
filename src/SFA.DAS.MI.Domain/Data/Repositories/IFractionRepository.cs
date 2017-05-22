using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.MI.Domain.Entities;

namespace SFA.DAS.MI.Domain.Data.Repositories
{
    public interface IFractionRepository
    {
        Task<List<Fraction>> GetFractionsByEmpref(string empRef);
        Task SaveFraction(Fraction fraction);
    }
}
