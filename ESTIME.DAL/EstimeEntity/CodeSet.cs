using System;
using System.Collections.Generic;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class CodeSet
    {
        public CodeSet()
        {
            CodeMember = new HashSet<CodeMember>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeSetTypeId { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }

        public CodeSetType CodeSetType { get; set; }
        public ICollection<CodeMember> CodeMember { get; set; }
    }
}
