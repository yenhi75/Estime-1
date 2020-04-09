using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TcCodeSetType
    {
        public TcCodeSetType()
        {
            TcCodeSet = new HashSet<TcCodeSet>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public ICollection<TcCodeSet> TcCodeSet { get; set; }
    }
}
