using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.data
{
    public class ConditionDTO
    {
        public int ConditionId { get; set; }
        public List<ConditionTokenDTO> LeftTokens { get; set; }
        public List<ConditionTokenDTO> RightTokens { get; set; }
        public ConditionTypeDTO ConditionType { get; set; }
    }
}
