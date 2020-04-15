using System;
using System.Collections.Generic;
using System.Text;
using ESTIME.DAL.EstimeEntity;

namespace ESTIME.DAL.Interface
{
    public interface IDataLoaderDal
    {
        int GetEstimeFileTypeId(string estimeFileTypeCode);
        TlEstimeFileType GetEstimeFileTypeByCode(string estimeFileTypeCode);
        int GetLoadStatusId(string loadStatusCode);
        TlLoadStatus GetLoadStatusByCode(string loadStatusCode);
        int GetRefPeriodId(string refPeriodCode);
        TcRefPeriod GetRefPeriodByCode(string refPeriodCode);
        TdLoad AddTdLoad(TdLoad newLoad);
        void UpdateTdLoad(TdLoad curLoad);
        IEnumerable<TlInputCoordinate> GetInputCoordinateListByEstimeFileType(int estimeFileTypeId);
        bool LoadTextDataFileByBulk(int loadId, int refPeriodId);
        bool AddTdLoadStaging(int loadId, int refPeriodId, List<TdLoadStaging> newLoadStaging);
        bool AddTdLoadData(int loadId, int refPeriodId, List<TdLoadData>myData);

    }
}
