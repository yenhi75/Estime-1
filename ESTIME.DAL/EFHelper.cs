using System;
using System;
using System.Collections.Generic;
using System.Data;


namespace ESTIME.DAL
{
    public class EFHelper
    {
        internal static void CallEF(Action call)
        {
            try
            {
                call();
            }
            catch (DataException ex)
            {
                //exception raised by entity framework.
                throw ex;
            }
            catch (Exception ex)
            {
                //unexpected exception
                throw ex;
            }
        }
        internal static int CallEF(Action<int> call)
        {
            try
            {
                int param = 0;
                call?.Invoke(param);
                return param;
            }
            catch (DataException ex)
            {
                //exception raised by entity framework.
                throw ex;
            }
            catch (Exception ex)
            {
                //unexpected exception
                throw ex;
            }
        }
    }
}
