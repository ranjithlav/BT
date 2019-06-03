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
    }
}
