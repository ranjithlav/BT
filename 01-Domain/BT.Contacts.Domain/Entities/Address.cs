using System.ComponentModel.DataAnnotations;

namespace BT.Contacts.Domain.Entities
{
    public class Address : EntityBase
    {
        [Key]
        public int AddressId { get; set; }

        public int ContactId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public Contact Contact { get; set; }
    }
}
