using BT.Contacts.Common;
using BT.Contacts.Domain;
using EntityModel = BT.Contacts.Domain.Entities;
using BT.Contacts.Infrastructure.Api.Repository;
using BT.Contacts.Infrastructure.Implementation.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BT.Contacts.Infrastructure.Implementation.Repository
{
    public class ContactRepo : IContactRepo
    {
        private readonly ILogger<ContactRepo> _logger;
        private readonly IOptions<DB> _dbOptions;

        private readonly ContactsDbContext _context;

        public ContactRepo(ILogger<ContactRepo> logger, IOptions<DB> dbOptions)
        {
            logger.CheckNull();
            dbOptions.CheckNull();

            _logger = logger;
            _dbOptions = dbOptions;

            _context = new ContactsDbContext(_dbOptions);
        }

        public EntityModel.Contact Add(EntityModel.Contact contact)
        {
            contact.CreatedDate = DateTime.UtcNow;
            _context.Add(contact);
            _context.SaveChanges();

            return contact;
        }

        public EntityModel.Contact Get(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            return _context.Contacts
                .Where(contact => contact.ContactId == contactId)
                .Include(contactAddress => contactAddress.Addresses)
                .FirstOrDefault();
        }

        public IEnumerable<EntityModel.Contact> GetAll()
        {
            return _context.Contacts
                .Include(contactAddress => contactAddress.Addresses);
        }

        public IEnumerable<EntityModel.Contact> GetAll(string zipcode)
        {
            return (_context.Contacts
                .Include(contactAddress => contactAddress.Addresses))
                .Select(m => new EntityModel.Contact
                {
                    Addresses = m.Addresses.Where(z => z.ZipCode.Equals(zipcode)).ToList(),
                    BusinessName = m.BusinessName,
                    ContactId = m.ContactId,
                    CreatedDate = m.CreatedDate,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Type = m.Type,
                    UpdatedDate = m.UpdatedDate
                }).ToList();
        }

        public bool Delete(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            //var contact = context.Contact.Find(contactId);
            _context.Contacts.Remove(new EntityModel.Contact { ContactId = contactId });
            return true;
        }
    }
}
