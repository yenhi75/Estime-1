using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class CodeMember
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeSetId { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public int? DisplayOrder { get; set; }

        public CodeSet CodeSet { get; set; }
    }
}
