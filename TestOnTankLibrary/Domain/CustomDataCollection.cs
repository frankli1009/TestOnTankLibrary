using System;
using System.Collections.Generic;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Domain
{
    /// <summary>
    /// A custom data collection of CustomData
    /// </summary>
    /// <typeparam name="T">The exact type of CustomData</typeparam>
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

        /// <summary>
        /// Find the data record by key.
        /// </summary>
        /// <param name="key">The key to locate the data record.</param>
        /// <returns>The data record if found, otherwise return null.</returns>
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

        /// <summary>
        /// Load data collection from an Excel file.
        /// </summary>
        /// <param name="xlsFilePath">The file path that contains the data.</param>
        /// <param name="sheetName">The name of sheet that contains the data.</param>
        /// <returns>The data collection itself.</returns>
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
