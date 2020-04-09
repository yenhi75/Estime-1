using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TdLoad
    {
        public TdLoad()
        {
            TdLoadData = new HashSet<TdLoadData>();
            TdLoadStaging = new HashSet<TdLoadStaging>();
        }

        public int Id { get; set; }
        public string FilePath { get; set; }
        public int EstimeFileTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int LoadStatusId { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }

        public TlEstimeFileType EstimeFileType { get; set; }
        public TlLoadStatus LoadStatus { get; set; }
        public ICollection<TdLoadData> TdLoadData { get; set; }
        public ICollection<TdLoadStaging> TdLoadStaging { get; set; }
    }
}
