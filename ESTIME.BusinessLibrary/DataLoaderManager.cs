using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
//using Excel = Microsoft.Office.Interop.Excel;

using ESTIME.DAL.Interface;
using ESTIME.DAL;
using ESTIME.DAL.EstimeEntity;


namespace ESTIME.BusinessLibrary
/******************************************************
* This class transforms various input files to a 
* standardized CSV format 
* Author: Claire Hendrickson-Jones
* Date: Mar. 11, 2020
* Modifications:
******************************************************/
{
    public class DataLoaderManager : ManagerBase
    {
        protected readonly IDataLoaderDal dal;


        //private static string filePath;
        ////private static int fileType;
        //private string output;
        //private StreamWriter outputStream;

        //private int estimeFileTypeId;
        private TlEstimeFileType estimeFileType;
        private int refPeriodId;
        private TdLoad curLoad;
        int ws;
        private bool loadSuccess;
        private string loadErr = string.Empty;

        public DataLoaderManager(IConfiguration config)
            : base(config)
        {
            dal = new DataLoaderDal(connectionString);
        }
        public DataLoaderManager() : base() { }

        //public DataLoadManager(string filePath, int fileType)
        //{
        //    DataLoadManage.filePath = filePath;
        //    DataLoadManage.fileType = fileType;

        //}

        //public void InterprovincialMigrationSubTable(Excel.Worksheet ws, string tableName, string tableSubpart)
        //{
        //    short intStart=1;
        //    short intCol;
        //    short line, dataLineStart, dataLineEnd, provFromId, provToId;
        //    string prov;

        //    //Inter
        //    //ws.Cells.Text;

        //    for (int i =1; i< ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row; i++)
        //    {
        //        //Excel.Range r = ws.Cells[intStart, 1];


        //        //if (ws.Cells[intStart, 1].te= "K"){ }


        //    }
        //}

        public long StartLoading(string refPeriodCode, string estimeFileTypeCode, string fileName, string userName)
        {
            estimeFileType = dal.GetEstimeFileTypeByCode(estimeFileTypeCode);
            refPeriodId = dal.GetRefPeriodId(refPeriodCode);
            int loadStatusId = dal.GetLoadStatusId("R");
            TdLoad newLoad = new TdLoad()
            {
                Id = 0,
                FilePath = fileName,
                EstimeFileTypeId = estimeFileType.Id,
                StartTime = System.DateTime.Now,
                LoadStatusId = loadStatusId,
                UserId = userName
            };
            curLoad = dal.AddTdLoad(newLoad);

            //Extract worksheet

            return curLoad.Id;
        }

        public bool TryLoadData()
        {
            try
            {
                if (estimeFileType.FileType.Extension == ".txt" || estimeFileType.FileType.Extension == ".csv")
                {
                    //loading text file
                    loadSuccess = dal.LoadTextDataFileByBulk(curLoad.Id, refPeriodId);

                    //new code to construct the loadStagings list from the text file
                    //To test, comment the call above and uncomment the code below
                    //List<TdLoadStaging> loadStagings = new List<TdLoadStaging>();
                    //loadSuccess = dal.AddTdLoadStaging(curLoad.Id, refPeriodId, loadStagings);
                }
                else if (estimeFileType.FileType.Extension == ".xlsx")
                {
                    //loading exel file
                    if (ws == null)
                    {
                        loadErr = "Empty Worksheet!";
                        loadSuccess = false;
                    }
                    else
                    {

                        List<TlInputCoordinate> inputCoordinates = dal.GetInputCoordinateListByEstimeFileType(estimeFileType.Id).ToList();

                        //use the input coordinates to converte the data in ws to a list of TdLoadData
                        List<TdLoadData> myData = new List<TdLoadData>();


                        //Add new data and save to database
                       loadSuccess = dal.AddTdLoadData(curLoad.Id, refPeriodId, myData);
                    }
                }
                return loadSuccess;
            }
            catch (Exception e)
            {
                loadErr = e.Message;
                loadSuccess = false;
                return loadSuccess;
            }
            finally
            {
                EndLoading();
            }
        }

        private void EndLoading()
        {
            curLoad.EndTime = System.DateTime.Now;
            int loadStatusId;
            if (loadSuccess)
            {
                loadStatusId = dal.GetLoadStatusId("C");
            }
            else
            {
                loadStatusId = dal.GetLoadStatusId("F");
            }
            curLoad.LoadStatusId = loadStatusId;
            if (!string.IsNullOrEmpty(loadErr))
            {
                curLoad.ErrorMessage = loadErr;
            }
           
            dal.UpdateTdLoad(curLoad);

        }
        public void WriteToLog(string filePath, string fileContents)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, fileContents);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
