using System.Threading.Tasks;

namespace SFA.DAS.MI.Application.Validation
{
    public interface IValidator <T>
    {
        Task<ValidationResult> ValidateAsync(T item);
    }
}
