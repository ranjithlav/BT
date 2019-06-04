using AutoMapper;
using BT.Contacts.Application.Api;
using BT.Contacts.Application.Models;
using Entity = BT.Contacts.Domain.Entities;
using BT.Contacts.Common;
using BT.Contacts.Infrastructure.Api.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BT.Contacts.Application.Implementation
{
    /// <summary>
    /// Handle Address
    /// </summary>
    public class Addresses : IAddresses
    {
        private readonly ILogger<Addresses> _logger;
        private readonly IAddressRepo _addressRepo;
        private readonly IMapper _mapper;

        public Addresses(ILogger<Addresses> logger, IMapper mapper, IAddressRepo addressRepo)
        {
            logger.CheckNull();
            mapper.CheckNull();
            addressRepo.CheckNull();

            _logger = logger;
            _mapper = mapper;
            _addressRepo = addressRepo;
        }

        public virtual Address Add(Address address)
        {
            _logger.LogInformation($"Add Address");
            return address.Validate(true) ? _mapper.Map<Address>(_addressRepo.Add(_mapper.Map<Entity.Address>(address))) : null;
        }

        /// <summary>
        /// Get all addresses
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Address> GetAll()
        {
            _logger.LogInformation("Get all Address");

            var repo = _addressRepo.FindAll();
            return _mapper.Map<IEnumerable<Address>>(repo);
        }

        /// <summary>
        /// Get Address by addressId
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public virtual Address GetByAddressId(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            _logger.LogInformation($"Get Address info of addressId: '{addressId}'");

            return _mapper.Map<Address>(_addressRepo.FindByAddressId(addressId));
        }

        /// <summary>
        /// Get Address by contactId
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Address> GetByContactId(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            _logger.LogInformation($"Get Address info of contactId: '{contactId}'");

            return _mapper.Map<IEnumerable<Address>>(_addressRepo.FindByContactId(contactId));
        }

        /// <summary>
        /// Get Address by zipCode
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public virtual IEnumerable<Address> GetByZipCode(string zipCode)
        {
            zipCode.CheckNullOrEmpty(nameof(zipCode));
            _logger.LogInformation($"Get Address info of zipCode: '{zipCode}'");

            return _mapper.Map<IEnumerable<Address>>(_addressRepo.FindByZipCode(zipCode));
        }

        public virtual bool Remove(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));
            _logger.LogInformation($"Remove address id: {addressId}");

            return _addressRepo.Delete(addressId);
        }
    }
}
