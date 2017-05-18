using SFA.DAS.MI.Domain.Interfaces;

namespace SFA.DAS.MI.Domain.Configuration
{
    public class MiApiConfiguration : IConfiguration
    {
        public string DatabaseConnectionString { get; set; }
    }
}
