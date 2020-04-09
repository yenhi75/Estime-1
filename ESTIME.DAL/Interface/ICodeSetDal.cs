using System;
using System.Collections.Generic;
using ESTIME.DAL.EstimeEntity;

namespace ESTIME.DAL.Interface
{
    public interface ICodeSetDal
    {
        IEnumerable<CodeSet> GetCodeSetList();
        //CodeSet GetCodeSet(int codesetId);
        //CodeSet GetCodeSetByCodeSetCode(int codesetCode);
        //bool AddCodeSet(CodeSet codeset);
        //bool UpdateCodeSet(CodeSet codeset);
        //bool DeleteCodeSet(int codesetId);
    }
}
