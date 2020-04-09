using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class TlEstimeFileType
    {
        public TlEstimeFileType()
        {
            TdLoad = new HashSet<TdLoad>();
            TlInputVariable = new HashSet<TlInputVariable>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public int FileTypeId { get; set; }
        public int? ColumnDelimiterId { get; set; }
        public int? SheetNumber { get; set; }
        public bool IsUniform { get; set; }

        public TlColumnDelimiter ColumnDelimiter { get; set; }
        public TlFileType FileType { get; set; }
        public ICollection<TdLoad> TdLoad { get; set; }
        public ICollection<TlInputVariable> TlInputVariable { get; set; }
    }
}
