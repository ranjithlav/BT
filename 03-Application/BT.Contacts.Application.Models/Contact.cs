using System;
using System.Collections.Generic;

namespace BT.Contacts.Application.Models
{
    /// <summary>
    /// Contact details
    /// </summary>
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BusinessName { get; set; }

        public ContactType Type { get; set; }

        public List<Address> Addresses { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(BusinessName))
            {
                throw new ArgumentException(ValidationMessages.EitherNameComboMandatory);
            }
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                if (string.IsNullOrEmpty(BusinessName))
                {
                    throw new ArgumentException(ValidationMessages.FirstAndLastNameMandatory);
                }
            }
            return true;
        }
    }
}
