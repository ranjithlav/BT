using AutoMapper;
using BT.Contacts.Application.Api;
using BT.Contacts.Application.Models;
using BT.Contacts.Common;
using BT.Contacts.Infrastructure.Api.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Entity = BT.Contacts.Domain.Entities;

namespace BT.Contacts.Application.Implementation
{
    public class Contacts : IContacts
    {
        private readonly ILogger<Contacts> _logger;
        private readonly IContactRepo _contactRepo;
        private readonly IMapper _mapper;

        public Contacts(ILogger<Contacts> logger, IMapper mapper, IContactRepo contactRepo)
        {
            logger.CheckNull();
            mapper.CheckNull();
            contactRepo.CheckNull();

            _logger = logger;
            _mapper = mapper;
            _contactRepo = contactRepo;
        }

        public Contact Add(Contact contact)
        {
            _logger.LogInformation($"Add Contact");
            contact.Validate();
            return _mapper.Map<Contact>(_contactRepo.Add(_mapper.Map<Entity.Contact>(contact)));
        }

        public Contact Get(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            _logger.LogInformation($"Get contact information for id: '{contactId}'");

            return _mapper.Map<Contact>(_contactRepo.Get(contactId));
        }

        public IEnumerable<Contact> GetAll()
        {
            _logger.LogInformation("Get all contact information");

            var contactFromRepo = _contactRepo.GetAll();

            if(contactFromRepo.Any())
                return _mapper.Map<IEnumerable<Contact>>(contactFromRepo);
            
            return null;
        }

        public bool Remove(int contactId) {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            return _contactRepo.Delete(contactId);
        }
    }
}
