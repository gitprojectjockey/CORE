using System;
using System.Collections.Generic;

namespace Http.Mvc.Listner.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Compaany { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public string Registered { get; set; }
        public List<string> InvalidReasons
        {
            get { return invalidReasons; }
        }
        List<string> invalidReasons = new List<string>();
        public void SetInvalidReason(string reason)
        {
            invalidReasons.Add(reason);
        }
    }
}
