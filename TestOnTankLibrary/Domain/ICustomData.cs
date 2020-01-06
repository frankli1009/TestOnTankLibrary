using System;
namespace TestOnTankLibrary.Domain
{
    public interface ICustomData
    {
        public int ParamCount => 2;
        public bool SetData(out string errorMessage, params string[] list);
        public bool IsKey(string key);
        public string GetValue();
    }
}
