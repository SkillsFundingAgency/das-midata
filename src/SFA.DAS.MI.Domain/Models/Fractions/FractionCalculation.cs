using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Fractions
{
    public class FractionCalculation
    {
        [JsonProperty("calculatedAt")]
        public string CalculatedAt { get; set; }

        [JsonProperty("fractions")]
        public List<Fraction> Fractions { get; set; }
    }
}