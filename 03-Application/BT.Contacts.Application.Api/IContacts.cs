﻿using BT.Contacts.Application.Models;
using System.Collections.Generic;

namespace BT.Contacts.Application.Api
{
    public interface IContacts
    {
        Contact Add(Contact contact);

        Contact Get(int contactId);

        IEnumerable<Contact> GetAll();

        bool Remove(int contactId);
    }
}
