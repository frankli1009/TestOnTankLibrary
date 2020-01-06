using System;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Domain
{
    public class CustomData : ICustomData
    {
        public virtual int ParamCount => 2;
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomData()
        {
        }

        public string GetValue() => Value;

        public bool IsKey(string key) => Key == key;

        public virtual bool SetData(out string errorMessage, params string[] list)
        {
            if (list.Length < ParamCount)
            {
                errorMessage = "Data column(s) missing.";
                throw new InvalidElementLocationException(errorMessage);
            }

            Key = list[0];
            Value = list[1];
            errorMessage = string.Empty;
            return true;
        }
    }
}
