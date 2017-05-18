using System;
using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Declarations
{
    public class Declaration
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("submissionId")]
        public long SubmissionId { get; set; }

        [JsonProperty("dateCeased")]
        public DateTime DateCeased { get; set; }

        [JsonProperty("inactiveFrom")]
        public DateTime InactiveFrom { get; set; }

        [JsonProperty("inactiveTo")]
        public DateTime InactiveTo { get; set; }

        [JsonProperty("noPaymentForPeriod")]
        public bool NoPaymentForPeriod { get; set; }

        [JsonProperty("submissionTime")]
        public string SubmissionTime { get; set; }

        [JsonProperty("payrollPeriod")]
        public PayrollPeriod PayrollPeriod { get; set; }

        [JsonProperty("levyDueYTD")]
        public decimal LevyDueYearToDate { get; set; }

        [JsonProperty("levyAllowanceForFullYear")]
        public decimal LevyAllowanceForFullYear { get; set; }
    }
}