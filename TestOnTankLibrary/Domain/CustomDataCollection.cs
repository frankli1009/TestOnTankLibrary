using System;
using System.Collections.Generic;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Domain
{
    public class CustomDataCollection<T> where T : CustomData
    {
        protected bool successfullyLoaded;
        protected string errorMessage;
        protected List<T> list; 

        public CustomDataCollection()
        {
            list = new List<T>();
        }

        public bool SuccessfullyLoaded => successfullyLoaded;
        public string ErrorMessage => errorMessage;

        public T Find(string key)
        {
            if (list == null) return null;

            for(int i=0; i<list.Count; i++)
            {
                if (list[i].IsKey(key))
                {
                    return list[i];
                }
            }

            return null;
        }

        public CustomDataCollection<T> LoadDataFromXls(string xlsFilePath, string sheetName)
        {
            list.Clear();
            T t = (T)Activator.CreateInstance(typeof(T));
            List<T> locs = DataIO.ReadDataFromExcelFile<T>(xlsFilePath, sheetName, t.ParamCount, out errorMessage);
            if (locs == null)
            {
                successfullyLoaded = false;
            }
            else
            {
                successfullyLoaded = true;
                list.AddRange(locs);
            }
            return this;
        }
    }
}
