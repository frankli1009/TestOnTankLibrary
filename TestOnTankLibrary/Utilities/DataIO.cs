using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using TestOnTankLibrary.Domain;

namespace TestOnTankLibrary.Utilities
{
    public class DataIO
    {
        public static List<T> ReadDataFromExcelFile<T>(string xlsFilePath,
            string sheetName, int colCount, out string errorMessage) where T : CustomData
        {
            errorMessage = string.Empty;
            List<T> list = new List<T>();
            try
            {
                //XSSFWorkbook wb;
                //HSSFWorkbook wb;
                //using (FileStream file = new FileStream(xlsFilePath, FileMode.Open, FileAccess.Read))
                //{
                //    wb = new XSSFWorkbook(file);
                //    wb = new HSSFWorkbook(file);
                //}

                IWorkbook wb = WorkbookFactory.Create(xlsFilePath);
                ISheet sheet = wb.GetSheet(sheetName);
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                    {
                        string[] data = new string[colCount];
                        bool allEmpty = true;
                        for (int i=0; i<colCount; i++)
                        {
                            data[i] = sheet.GetRow(row).GetCell(i).StringCellValue;
                            allEmpty &= string.IsNullOrWhiteSpace(data[i]);
                        }

                        if (allEmpty)
                        {
                            break;
                        }
                        string errMsg;
                        T t = (T)Activator.CreateInstance(typeof(T));
                        if (t.SetData(out errMsg, data))
                        {
                            list.Add(t);
                        }
                        else
                        {
                            errorMessage = errMsg;
                            break;
                        }
                    }
                }
                if(string.IsNullOrEmpty(errorMessage))
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return null;
            }
        }
    }
}
