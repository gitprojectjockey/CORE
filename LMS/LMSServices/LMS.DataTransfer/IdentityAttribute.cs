using System;

namespace LMS.DataTransfer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IdentityAttribute : Attribute
    {
        public IdentityAttribute(string Id)
        {
            _id = Id;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
        }
    }
}
