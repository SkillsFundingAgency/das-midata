using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Declarations
{
    public class PayrollPeriod
    {
        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("month")]
        public short Month { get; set; }
    }
}