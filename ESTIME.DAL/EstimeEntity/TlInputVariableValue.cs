using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlInputVariableValue
    {
        public int Id { get; set; }
        public int RecordNumber { get; set; }
        public int InputVariableId { get; set; }
        public string VariableValue { get; set; }

        public TlInputVariable InputVariable { get; set; }
    }
}
