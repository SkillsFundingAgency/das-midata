using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.MI.Domain.Models.Fractions;

namespace SFA.DAS.MI.Domain.Data.Repositories
{
    public interface IFractionsRepository
    {
        Task<List<FractionCalculation>> GetFractionsByEmpref(string empRef);
    }
}
