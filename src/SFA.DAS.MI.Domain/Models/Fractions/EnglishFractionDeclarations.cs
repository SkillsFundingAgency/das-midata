using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Fractions
{
    public class EnglishFractionDeclarations
    {
        [JsonProperty("empref")]
        public string Empref { get; set; }

        [JsonProperty("fractionCalculations")]
        public List<FractionCalculation> FractionCalculations { get; set; }
    }
}
