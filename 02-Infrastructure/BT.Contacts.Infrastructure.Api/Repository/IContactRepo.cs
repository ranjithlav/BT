using BT.Contacts.Domain.Entities;
using System.Collections.Generic;

namespace BT.Contacts.Infrastructure.Api.Repository
{
    public interface IContactRepo
    {
        Contact Add(Contact contact);

        IEnumerable<Contact> GetAll();

        Contact Get(int contactId);

        bool Delete(int contactId);
    }
}
