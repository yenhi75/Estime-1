using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TcCodeMember
    {
        public TcCodeMember()
        {
            InverseAggTop = new HashSet<TcCodeMember>();
            InverseAggUp = new HashSet<TcCodeMember>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string AlphaCode { get; set; }
        public int CodeSetId { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public int? AggUpId { get; set; }
        public int? AggTopId { get; set; }
        public int? DisplayOrder { get; set; }

        public TcCodeMember AggTop { get; set; }
        public TcCodeMember AggUp { get; set; }
        public TcCodeSet CodeSet { get; set; }
        public ICollection<TcCodeMember> InverseAggTop { get; set; }
        public ICollection<TcCodeMember> InverseAggUp { get; set; }
    }
}
