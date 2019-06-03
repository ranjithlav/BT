using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BT.Contacts.Domain.Entities
{
    public class Contact : EntityBase
    {
        [Key]
        public int ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BusinessName { get; set; }

        public string Type { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
