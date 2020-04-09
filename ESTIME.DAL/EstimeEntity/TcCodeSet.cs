using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TcCodeSet
    {
        public TcCodeSet()
        {
            TcCodeMember = new HashSet<TcCodeMember>();
            TlVariable = new HashSet<TlVariable>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeSetTypeId { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public TcCodeSetType CodeSetType { get; set; }
        public ICollection<TcCodeMember> TcCodeMember { get; set; }
        public ICollection<TlVariable> TlVariable { get; set; }
    }
}
