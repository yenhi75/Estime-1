using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlColumnDelimiter
    {
        public TlColumnDelimiter()
        {
            TlEstimeFileType = new HashSet<TlEstimeFileType>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public ICollection<TlEstimeFileType> TlEstimeFileType { get; set; }
    }
}
