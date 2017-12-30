using Http.Mvc.Listner.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Http.Mvc.Listner.Services
{
    public class ContactDataValidatorService
    {
        enum ValidationRules { LowerAge = 21, UpperAge = 110, IdLength = 24 }
        IEnumerable<Contact> _contacts;
        public ContactDataValidatorService(IEnumerable<Contact> contacts)
        {
            _contacts = contacts;
        }

        public IEnumerable<Contact> ValidateJsonValues()
        {
            Func<Contact, int, bool> idFieldLengthChecker = (input, len) => input.Id.Length == len;
            Func<Contact, int, int, bool> ageValueChecker = (input, lower, upper) => input.Age > lower && input.Age < upper;
            Func<Contact, bool> emailAddressChecker = input => Regex.IsMatch(input.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return IsFieldDataValid(idFieldLengthChecker, ageValueChecker, emailAddressChecker);
        }

        IEnumerable<Contact> IsFieldDataValid(Func<Contact, int, bool> idFieldDataLengthChecker,
                                              Func<Contact, int, int, bool> ageValueChecker,
                                              Func<Contact, bool> emailAddressChecker)
        {
            var invalidContacts = new List<Contact>();
            foreach (var contact in _contacts)
            {
                if (!idFieldDataLengthChecker(contact, (int)ValidationRules.IdLength))
                {
                    contact.SetInvalidReason("Invalid Id Length");
                    invalidContacts.Add(contact);
                }

                if (!ageValueChecker(contact, (int)ValidationRules.LowerAge, (int)ValidationRules.UpperAge))
                {
                    contact.SetInvalidReason("Invalid Age Value");
                    invalidContacts.Add(contact);
                }

                if (!emailAddressChecker(contact))
                {
                    contact.SetInvalidReason("Invalid Email Address");
                    invalidContacts.Add(contact);
                }
            }
            return invalidContacts;
        }
    }
}
