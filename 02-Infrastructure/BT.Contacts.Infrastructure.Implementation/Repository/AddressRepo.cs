using BT.Contacts.Common;
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

        private readonly AddressDbContext context;

        public AddressRepo(ILogger<AddressRepo> logger, IOptions<DB> dbOptions)
        {
            logger.CheckNull();
            dbOptions.CheckNull();

            _logger = logger;
            _dbOptions = dbOptions;

            context = new AddressDbContext(_dbOptions);
        }

        public Address Add(Address address)
        {
            context.Add(address);
            context.SaveChanges();

            return address;
        }

        public IEnumerable<Address> FindAll()
        {
            return context.Address;
        }

        public Address FindByAddressId(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            return context.Address.Where(i => i.AddressId == addressId).FirstOrDefault();
        }

        public IEnumerable<Address> FindByContactId(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            return context.Address.Where(i => i.ContactId == contactId);
        }

        public IEnumerable<Address> FindByZipCode(string zipCode)
        {
            zipCode.CheckNullOrEmpty(zipCode);
            return context.Address.Where(i => i.ZipCode.Equals(zipCode));
        }

        public bool Delete(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            context.Address.Remove(new Address { AddressId = addressId });
            return true;
        }
    }
}
