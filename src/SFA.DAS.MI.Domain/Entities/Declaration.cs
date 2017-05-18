using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.MI.Domain.Entities
{
    public class Declaration
    {
        public long Id { get; set; }
        public string EmpRef { get; set; }
        public string PayrollYear { get; set; }
        public int PayrollMonth { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal LevyDueYtd { get; set; }
        public decimal LevyAllowanceForYear { get; set; }
        public DateTime? CeasationDate { get; set; }
    }
}
