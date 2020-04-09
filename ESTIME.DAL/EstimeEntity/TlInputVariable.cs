using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlInputVariable
    {
        public TlInputVariable()
        {
            TdLoadData = new HashSet<TdLoadData>();
            TlInputCoordinate = new HashSet<TlInputCoordinate>();
            TlInputVariableValue = new HashSet<TlInputVariableValue>();
        }

        public int Id { get; set; }
        public int EstimeFileTypeId { get; set; }
        public int VariableId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsParameter { get; set; }

        public TlEstimeFileType EstimeFileType { get; set; }
        public TlVariable Variable { get; set; }
        public ICollection<TdLoadData> TdLoadData { get; set; }
        public ICollection<TlInputCoordinate> TlInputCoordinate { get; set; }
        public ICollection<TlInputVariableValue> TlInputVariableValue { get; set; }
    }
}
