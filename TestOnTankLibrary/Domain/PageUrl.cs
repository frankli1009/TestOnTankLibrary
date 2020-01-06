using System;
namespace TestOnTankLibrary.Domain
{
    public class PageUrl : CustomData
    {
        public string PageName { get => Key; set { Key = value; } }
        public string Url { get => Value; set { Value = value; } }

        public PageUrl()
        {
        }
    }
}
