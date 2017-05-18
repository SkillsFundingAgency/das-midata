using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SFA.DAS.MI.Domain.Models.Declarations
{
    public class LevyDeclarations
    {
        [JsonProperty("empref")]
        public string EmpRef { get; set; }

        [JsonProperty("declarations")]
        public List<Declaration> Declarations { get; set; }
    }
}
