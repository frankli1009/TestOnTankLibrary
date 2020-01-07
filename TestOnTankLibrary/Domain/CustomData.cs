using System;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Domain
{
    /// <summary>
    /// The custom data class to hold a custom data record/structure
    /// </summary>
    public class CustomData : ICustomData
    {
        /// <summary>
        /// The count of params to form a valid custom data.
        /// </summary>
        public virtual int ParamCount => 2;
        public string Key { get; set; }
        public string Value { get; set; }

        public CustomData()
        {
        }

        public string GetValue() => Value;

        /// <summary>
        /// Check whether the keys are the same.
        /// </summary>
        /// <param name="key">The specific key value to be compared with.</param>
        /// <returns>Return true if the keys are the same, otherwise return false</returns>
        public bool IsKey(string key) => Key == key;

        /// <summary>
        /// Set Key and Value from string[] data
        /// </summary>
        /// <param name="errorMessage">The error message that explains the failure of setting data.</param>
        /// <param name="list">The string[] data that is used to set Key and Value.</param>
        /// <returns>Return true if successfully set data otherwise return false</returns>
        public virtual bool SetData(out string errorMessage, params string[] list)
        {
            if (list.Length < ParamCount)
            {
                errorMessage = "Data column(s) missing.";
                throw new InvalidCustomDataException(errorMessage);
            }

            Key = list[0];
            Value = list[1];
            errorMessage = string.Empty;
            return true;
        }
    }
}
