using BT.Contacts.Application.Api;
using BT.Contacts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BT.NetCoreClient.Controllers
{
    [Route("api/ContactInfo")]
    public class ContactInfoController : Controller
    {
        private readonly ILogger<ContactInfoController> _logger;
        private readonly IContacts _contacts;

        public ContactInfoController(ILogger<ContactInfoController> logger, IContacts contacts)
        {
            logger.CheckNull();
            contacts.CheckNull();

            _logger = logger;
            _contacts = contacts;
        }

        [HttpGet, Route("getall")]
        public IActionResult GetAllContactInfo()
        {
            _logger.LogInformation("Get all contact info");
            var result = _contacts.GetAll();

            return Ok(result);
        }

        [HttpGet, Route("get/{id}")]
        public IActionResult GetContactInfo(int id)
        {
            _logger.LogInformation($"Get contact info for id: '{id}'");

            var result = _contacts.Get(id);

            return Ok(result);
        }
    }
}