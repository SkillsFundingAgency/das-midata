using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.MI.Domain.Models.MiData
{
    public class MiRow
    {
        public string EmpRef { get; set; }
        public string PayrollYear { get; set; }
        public string PayrollMonth { get; set; }
        public DateTime SubmissionDate => DateTime.Now;
        public string LevyDueYtd { get; set; }
        public string LevyAllowanceForYear { get; set; }
        public string CessationDate { get; set; }
        public string FractionAmount { get; set; }
        public DateTime FractionDateCalculated => DateTime.Now;
    }

   
}
