using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.MI.Domain.Interfaces;

namespace SFA.DAS.MI.Domain.Configuration
{
    public class MiFileReaderConfiguration : IConfiguration
    {
        public string DatabaseConnectionString { get; set; }
        public string FileSystemPath { get; set; }
    }
}
