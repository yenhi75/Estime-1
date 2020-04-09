using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TcRefPeriodType
    {
        public TcRefPeriodType()
        {
            TcRefPeriod = new HashSet<TcRefPeriod>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public ICollection<TcRefPeriod> TcRefPeriod { get; set; }
    }
}
