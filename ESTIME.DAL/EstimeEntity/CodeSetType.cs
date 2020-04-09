using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class CodeSetType
    {
        public CodeSetType()
        {
            CodeSet = new HashSet<CodeSet>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public ICollection<CodeSet> CodeSet { get; set; }
    }
}
