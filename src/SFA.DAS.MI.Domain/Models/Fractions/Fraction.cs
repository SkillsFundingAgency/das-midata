using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Fractions
{
    public class Fraction
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}