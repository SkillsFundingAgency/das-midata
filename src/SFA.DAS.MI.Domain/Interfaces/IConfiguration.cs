using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.MI.Domain.Interfaces
{
    public interface IConfiguration
    {
        string DatabaseConnectionString { get; set; }

    }
}
