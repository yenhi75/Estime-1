using System.Collections.Generic;
using ESTIME.DAL.EstimeEntity;

namespace ESTIME.BusinessLibrary.Object
{
    public class CodeSet
    {
        public int CodeSetID { get; set; }
        public CodeSetType CodeSetTypeValue { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public string CodeSetCode { get; set; }

        public List<CodeSetMember> CodeSetMembers { get; set; }

        public CodeSet(DAL.EstimeEntity.CodeSet code)
        {
            CodeSetID = code.Id;
            CodeSetCode = code.Code;
            NameEnglish = code.NameEnglish;
            NameFrench = code.NameFrench;
            CodeSetTypeValue = new CodeSetType(code.CodeSetType);

            if (code.CodeMember != null)
            {
                CodeSetMembers = new List<CodeSetMember>();
                foreach (CodeMember cm in code.CodeMember)
                {
                    CodeSetMembers.Add(new CodeSetMember(cm));
                }
            }
        }
    }
}
