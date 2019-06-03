using BT.Contacts.Application.Models;
using System.Collections.Generic;

namespace BT.Contacts.Application.Api
{
    public interface IContacts
    {
        Contact Get(int contactId);

        IEnumerable<Contact> GetAll();
    }
}
