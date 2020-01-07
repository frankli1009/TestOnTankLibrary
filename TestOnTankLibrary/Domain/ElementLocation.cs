using System;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Domain
{
    public class ElementLocation : CustomData
    {
        public ElementLocationType LocationType { get; set; }

        public ElementLocation()
        {
        }

        public override int ParamCount => 3;

        public override bool SetData(out string errorMessage, params string[] list)
        {
            if (list.Length < ParamCount)
            {
                errorMessage = "Data column(s) missing.";
                throw new InvalidCustomDataException(errorMessage);
            }

            ElementLocationType locationType;
            if (!Enum.TryParse(list[1], out locationType))
            {
                errorMessage = $"Unknown location type: {list[1]}";
                throw new InvalidCustomDataException(errorMessage);
            }

            Key = list[0];
            LocationType = locationType;
            Value = list[2];
            errorMessage = string.Empty;
            return true;
        }
    }
}
