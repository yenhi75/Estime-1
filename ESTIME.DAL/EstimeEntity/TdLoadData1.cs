using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public class TdLoadData1
    {
        public long Id { get; set; }
        public int LoadId { get; set; }
        public int RecordNumber { get; set; }
        public int InputVariableId { get; set; }
        public int RefPeriodId { get; set; }
        public string VariableValue { get; set; }

        public TlInputVariable InputVariable { get; set; }
        public TdLoad Load { get; set; }
        public TcRefPeriod RefPeriod { get; set; }

        public TdLoadData1(int loadId, int recordNumber, int inputVariableId, int refPeriodId, string variableValue)
        {
            LoadId = loadId;
            RecordNumber = recordNumber;
            InputVariableId = inputVariableId;
            RefPeriodId = refPeriodId;
            VariableValue = variableValue;
        }
    }
}
