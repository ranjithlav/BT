using BT.Contacts.Application.Api;
using BT.Contacts.Application.Models;
using BT.Contacts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BT.NetCoreClient.Controllers
{
    [Route("api")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContacts _contacts;

        public ContactController(ILogger<ContactController> logger, IContacts contacts)
        {
            logger.CheckNull();
            contacts.CheckNull();

            _logger = logger;
            _contacts = contacts;
        }

        [HttpPost, Route("contact")]
        public IActionResult Add([FromBody]Contact contact)
        {
            contact.CheckNull();
            _logger.LogInformation("Add contact info");
            
            var result = _contacts.Add(contact);

            return Ok(result);
        }

        [HttpDelete, Route("contact")]
        public IActionResult Delete(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            _logger.LogInformation($"Delete contact info of contactId: '{contactId}'");

            var result = _contacts.Remove(contactId);

            return Ok(result);
        }

        [HttpGet, Route("contacts")]
        public IActionResult GetAllContactInfo()
        {
            _logger.LogInformation("Get all contact info");
            var result = _contacts.GetAll();

            return Ok(result);
        }

        [HttpGet, Route("contacts/{zipcode}")]
        public IActionResult GetAllContactInfo(string zipcode)
        {
            zipcode.CheckNullOrEmpty(nameof(zipcode));
            _logger.LogInformation($"Get all contact info for zipcode: {zipcode}");
            var result = _contacts.GetAll(zipcode);

            return Ok(result);
        }

        [HttpGet, Route("contact/{contactId}")]
        public IActionResult GetContactInfo(int contactId)
        {
            contactId.CheckLessThanOrEqual(0, nameof(contactId));
            _logger.LogInformation($"Get contact info for id: '{contactId}'");

            var result = _contacts.Get(contactId);

            return Ok(result);
        }

    }
}