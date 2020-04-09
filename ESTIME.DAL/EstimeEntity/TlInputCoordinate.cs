using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlInputCoordinate
    {
        public int Id { get; set; }
        public int RecordNumber { get; set; }
        public int InputVariableId { get; set; }
        public int ColumnNumber { get; set; }
        public int? RowNumber { get; set; }

        public TlInputVariable InputVariable { get; set; }
    }
}
