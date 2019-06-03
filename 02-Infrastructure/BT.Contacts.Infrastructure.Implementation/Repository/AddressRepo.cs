﻿using BT.Contacts.Common;
using BT.Contacts.Domain;
using BT.Contacts.Domain.Entities;
using BT.Contacts.Infrastructure.Api.Repository;
using BT.Contacts.Infrastructure.Implementation.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BT.Contacts.Infrastructure.Implementation.Repository
{
    public class AddressRepo : IAddressRepo
    {
        private readonly ILogger<AddressRepo> _logger;
        private readonly IOptions<DB> _dbOptions;

        private readonly AddressDbContext _context;

        public AddressRepo(ILogger<AddressRepo> logger, IOptions<DB> dbOptions)
        {
            logger.CheckNull();
            dbOptions.CheckNull();

            _logger = logger;
            _dbOptions = dbOptions;

            _context = new AddressDbContext(_dbOptions);
        }

        public virtual Address Add(Address address)
        {
            address.CreatedDate = DateTime.UtcNow;
            _context.Add(address);
            _context.SaveChanges();

            return address;
        }

        public virtual IEnumerable<Address> FindAll()
        {
            return _context.Addresses;
        }

        public virtual Address FindByAddressId(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            return _context.Addresses.Where(i => i.AddressId == addressId).FirstOrDefault();
        }

        public virtual IEnumerable<Address> FindByContactId(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            return _context.Addresses.Where(i => i.ContactId == contactId);
        }

        public virtual IEnumerable<Address> FindByZipCode(string zipCode)
        {
            zipCode.CheckNullOrEmpty(zipCode);
            return _context.Addresses.Where(i => i.ZipCode.Equals(zipCode));
        }

        public virtual bool Delete(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            _context.Addresses.Remove(new Address { AddressId = addressId });
            return true;
        }
    }
}
