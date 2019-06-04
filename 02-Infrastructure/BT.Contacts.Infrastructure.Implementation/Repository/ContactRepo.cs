using BT.Contacts.Common;
using BT.Contacts.Domain;
using BT.Contacts.Infrastructure.Api.Repository;
using BT.Contacts.Infrastructure.Implementation.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityModel = BT.Contacts.Domain.Entities;

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
            contact.CreatedDate = DateTime.Now;
            contact.UpdatedDate = DateTime.Now;
            _context.Add(contact);
            return _context.SaveChanges() == 1 ? contact : null;
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
            var result =
                (from
                    cont in _context.Contacts
                 join addr in _context.Addresses
                 on cont.ContactId equals addr.ContactId
                 where addr.ZipCode == zipcode
                 select new
                 {
                     Id = cont.ContactId,
                     FN = cont.FirstName,
                     LN = cont.LastName,
                     BN = cont.BusinessName,
                     Typ = cont.Type,
                     Addrs = cont.Addresses.Where(z => z.ZipCode.Equals(zipcode)).ToList(),
                     CD = cont.CreatedDate,
                     UD = cont.UpdatedDate
                 }
                ).Distinct().ToList();

            var contacts = new List<EntityModel.Contact>();
            EntityModel.Contact contact;
            foreach (var item in result)
            {
                contact = new EntityModel.Contact();

                contact.ContactId = item.Id;
                contact.FirstName = item.FN;
                contact.LastName = item.LN;
                contact.BusinessName = item.BN;
                contact.Type = item.Typ;
                contact.Addresses = item.Addrs;
                contact.CreatedDate = item.CD;
                contact.UpdatedDate = item.UD;

                contacts.Add(contact);
            }
            return contacts;
        }

        public bool Delete(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            var contact = _context.Contacts
                           .Include(x => x.Addresses)
                           .SingleOrDefault(x => x.ContactId == contactId);
            if (contact == null)
            {
                throw new ArgumentNullException($"Contact with id: {contactId} not found");
            }
            _context.Contacts.Remove(contact);
            return _context.SaveChanges() > 0;
        }
    }
}
