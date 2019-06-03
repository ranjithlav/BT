using BT.Contacts.Application.Models;
using System.Collections.Generic;

namespace BT.Contacts.Application.Api
{
    public interface IAddresses
    {
        Address Add(Address address);

        IEnumerable<Address> GetAll();

        Address GetByAddressId(int addressId);

        IEnumerable<Address> GetByContactId(int contactId);

        IEnumerable<Address> GetByZipCode(string zipCode);

        bool Remove(int addressId);
    }
}
