using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TcRefPeriod
    {
        public TcRefPeriod()
        {
            InverseAggTop = new HashSet<TcRefPeriod>();
            InverseAggUp = new HashSet<TcRefPeriod>();
            InversePrevious = new HashSet<TcRefPeriod>();
            TdLoadData = new HashSet<TdLoadData>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public int RefPeriodTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? AggUpId { get; set; }
        public int? AggTopId { get; set; }
        public int? PreviousId { get; set; }

        public TcRefPeriod AggTop { get; set; }
        public TcRefPeriod AggUp { get; set; }
        public TcRefPeriod Previous { get; set; }
        public TcRefPeriodType RefPeriodType { get; set; }
        public ICollection<TcRefPeriod> InverseAggTop { get; set; }
        public ICollection<TcRefPeriod> InverseAggUp { get; set; }
        public ICollection<TcRefPeriod> InversePrevious { get; set; }
        public ICollection<TdLoadData> TdLoadData { get; set; }
    }
}
