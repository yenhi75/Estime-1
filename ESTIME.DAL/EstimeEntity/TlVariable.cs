using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlVariable
    {
        public TlVariable()
        {
            TlInputVariable = new HashSet<TlInputVariable>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public string DataType { get; set; }
        public int? MaxLength { get; set; }
        public int? NumericPrecision { get; set; }
        public int? CodeSetId { get; set; }

        public TcCodeSet CodeSet { get; set; }
        public ICollection<TlInputVariable> TlInputVariable { get; set; }
    }
}
