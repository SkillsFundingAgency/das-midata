using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.MI.Domain.Entities
{
    public class Fraction
    {
        public long Id { get; set; }
        public string EmpRef { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCalculated { get; set; }
    }
}
