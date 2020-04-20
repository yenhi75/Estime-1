using System;
using System.Collections.Generic;
using System.Text;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TdLoadStaging
    {
        public TdLoadStaging(int loadId, int recordId, string recordValue)
        {
            LoadId = loadId;
            RecordId = recordId;
            RecordValue = recordValue;
        }
    }
}
