using BT.Contacts.Common;
using System;
using System.Collections.Generic;

namespace BT.Contacts.Application.Models
{
    /// <summary>
    /// Contact details
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact key
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Contact First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Contact Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact Business name
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Contact Type: Person/Business
        /// </summary>
        public ContactType Type { get; set; }

        /// <summary>
        /// List of Addresses mapped with the contact
        /// </summary>
        //public List<Address> Addresses { get; set; }

        /// <summary>
        /// Contact Created date
        /// </summary>
        public DateTime CreatedDate { get; }

        /// <summary>
        /// Contact Updated date
        /// </summary>
        public DateTime UpdatedDate { get; }

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
