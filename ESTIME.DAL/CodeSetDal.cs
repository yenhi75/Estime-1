using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using ESTIME.DAL.Interface;
using ESTIME.DAL.EstimeEntity;

namespace ESTIME.DAL
{
    /******************************************************
     * This is a example Dal to retrieve data using EF
     * Author: Shan Cheng
     * Date: Mar. 10, 2020
     * Modifications:
     ******************************************************/
    public class CodeSetDal :ICodeSetDal
    {
        private string connString;
        //private DbContextOptions<EstimeContext> options;
        public CodeSetDal(string connStr)
        {
            connString = connStr;
            //options = new DbContextOptions<EstimeContext>();
        }
        public IEnumerable<CodeSet> GetCodeSetList()
        {
            IQueryable<CodeSet> codesets = null;

            var options = new DbContextOptions<EstimeContext>();
            List<CodeSet> myList = new List<CodeSet>();

            using (var context = new EstimeContext(options, connString))
            {
                //codesets = context.CodeSet.Include(cs => cs.CodeSetType).Include(cs => cs.CodeMember);
                EFHelper.CallEF(() =>
                {
                    //codesets = context.CodeSet.Include(cs => cs.CodeMember);
                    codesets = context.CodeSet.Include(cs => cs.CodeSetType).Include(cs => cs.CodeMember);
                });
                myList = codesets.ToList();
            }
            return myList;
        }
    }
}
