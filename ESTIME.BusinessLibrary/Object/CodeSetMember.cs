using System;
using System.Collections.Generic;
using System.Text;
using ESTIME.DAL.EstimeEntity;

namespace ESTIME.BusinessLibrary.Object
{
    public class CodeSetMember
    {
        public int CodeSetMemberID { get; set; }
        public int CodeSetID { get; set; }
        public string CodeSetMemberValue { get; set; }
        public string NameEnglish { get; set; }
        public string NameFrench { get; set; }
        public Nullable<int> DisplayOrder { get; set; }

        public CodeSetMember(CodeMember code)
        {
            CodeSetMemberID = code.Id;
            CodeSetID = code.CodeSetId;
            CodeSetMemberValue = code.Code;
            NameEnglish = code.NameEnglish;
            NameFrench = code.NameFrench;
            DisplayOrder = code.DisplayOrder;
        }
        public CodeSetMember() { }
    }
}
