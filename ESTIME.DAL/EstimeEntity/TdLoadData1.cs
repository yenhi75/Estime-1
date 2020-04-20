using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TdLoadData
    {
        public TdLoadData(int loadId, int recordNumber, int inputVariableId, int refPeriodId, string variableValue)
        {
            LoadId = loadId;
            RecordNumber = recordNumber;
            InputVariableId = inputVariableId;
            RefPeriodId = refPeriodId;
            VariableValue = variableValue;
        }
    }
}
