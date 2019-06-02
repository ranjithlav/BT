using System;

namespace BT.Contacts.Application.Models
{
    /// <summary>
    /// Address details
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Address key
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Contact key of this address field mapped with
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// Address street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Address state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Address zipcode
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Address created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Address  updated date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
