using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlLoadStatus
    {
        public TlLoadStatus()
        {
            TdLoad = new HashSet<TdLoad>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public ICollection<TdLoad> TdLoad { get; set; }
    }
}
