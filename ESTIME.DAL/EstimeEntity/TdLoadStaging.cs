using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TdLoadStaging
    {
        public long Id { get; set; }
        public int LoadId { get; set; }
        public int RecordId { get; set; }
        public string RecordValue { get; set; }

        public TdLoad Load { get; set; }
    }
}
