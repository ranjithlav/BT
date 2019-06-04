using System;

namespace BT.Contacts.Application.Models
{
    /// <summary>
    /// Address details
    /// </summary>
    public class Address
    {
        public int AddressId { get; set; }

        public int ContactId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Validate(bool relationshipCheck)
        {
            if (relationshipCheck)
            {
                if (ContactId <= 0)
                {
                    throw new ArgumentOutOfRangeException(string.Format(ValidationMessages.GenericIntegerMandatory, nameof(ContactId)));
                }
            }
            if (string.IsNullOrEmpty(Street))
            {
                throw new ArgumentException(string.Format(ValidationMessages.GenericStringMandatory, nameof(Street)));
            }
            if (string.IsNullOrEmpty(City))
            {
                throw new ArgumentException(string.Format(ValidationMessages.GenericStringMandatory, nameof(City)));
            }
            if (string.IsNullOrEmpty(State))
            {
                throw new ArgumentException(string.Format(ValidationMessages.GenericStringMandatory, nameof(State)));
            }
            if (string.IsNullOrEmpty(ZipCode))
            {
                throw new ArgumentException(string.Format(ValidationMessages.GenericStringMandatory, nameof(ZipCode)));
            }
            return true;
        }
    }
}
