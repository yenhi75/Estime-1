using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ESTIME.DAL;
using ESTIME.DAL.Interface;

namespace ESTIME.BusinessLibrary
{
    /******************************************************
     * This is a example manager to retrieve data using EF
     * Author: Shan Cheng
     * Date: Mar. 10, 2020
     * Modifications:
     ******************************************************/
    public class CodeSetManager : ManagerBase
    {
        protected readonly ICodeSetDal dal;

        public CodeSetManager(IConfiguration config)
            : base(config)
        {
            dal = new CodeSetDal(connectionString);
        }
        public CodeSetManager() : base() { }

        public List<Object.CodeSet> GetCodesetList()
        {
            List<Object.CodeSet> lst = new List<Object.CodeSet>();

            foreach (DAL.EstimeEntity.CodeSet cs in dal.GetCodeSetList())
            {
                lst.Add(new Object.CodeSet((DAL.EstimeEntity.CodeSet)cs));
            }
            return lst;
        }
    }
}
