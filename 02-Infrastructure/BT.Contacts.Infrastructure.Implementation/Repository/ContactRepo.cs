using BT.Contacts.Common;
using BT.Contacts.Domain;
using BT.Contacts.Domain.Entities;
using BT.Contacts.Infrastructure.Api.Repository;
using BT.Contacts.Infrastructure.Implementation.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace BT.Contacts.Infrastructure.Implementation.Repository
{
    public class ContactRepo : IContactRepo
    {
        private readonly ILogger<ContactRepo> _logger;
        private readonly IOptions<DB> _dbOptions;

        private readonly ContactsDbContext context;

        public ContactRepo(ILogger<ContactRepo> logger, IOptions<DB> dbOptions)
        {
            logger.CheckNull();
            dbOptions.CheckNull();

            _logger = logger;
            _dbOptions = dbOptions;

            context = new ContactsDbContext(_dbOptions);
        }

        public Contact Add(Contact contact)
        {
            context.Add(contact);
            context.SaveChanges();

            return contact;
        }

        public Contact Get(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            return context.Contact.Where(i => i.ContactId == contactId).FirstOrDefault();
        }

        public IEnumerable<Contact> GetAll()
        {
            return context.Contact;
        }

        public bool Delete(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            //var contact = context.Contact.Find(contactId);
            context.Contact.Remove(new Contact { ContactId = contactId });
            return true;
        }
    }
}
