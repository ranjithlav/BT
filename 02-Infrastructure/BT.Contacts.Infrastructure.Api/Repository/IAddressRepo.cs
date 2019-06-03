using BT.Contacts.Domain.Entities;
using System.Collections.Generic;

namespace BT.Contacts.Infrastructure.Api.Repository
{
    public interface IAddressRepo
    {
        Address Add(Address address);

        IEnumerable<Address> FindAll();

        Address FindByAddressId(int addressId);

        IEnumerable<Address> FindByContactId(int contactId);

        IEnumerable<Address> FindByZipCode(string zipCode);

        bool Delete(int addressId); 
    }
}
