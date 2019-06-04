using BT.Contacts.Application.Api;
using BT.Contacts.Application.Models;
using BT.Contacts.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BT.NetCoreClient.Controllers
{
    [Route("api")]
    public class AddressController : Controller
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddresses _addresses;

        public AddressController(ILogger<AddressController> logger, IAddresses addresses)
        {
            logger.CheckNull();
            addresses.CheckNull();

            _logger = logger;
            _addresses = addresses;
        }

        [HttpPost, Route("addresses")]
        [ProducesResponseType(typeof(Address), StatusCodes.Status201Created)]
        public IActionResult Add([FromBody]Address address)
        {
            address.CheckNull();
            _logger.LogInformation("Add Address");
            
            var result = _addresses.Add(address);

            return Ok(result);
        }

        [HttpDelete, Route("addresses")]
        public IActionResult Delete(int addressId)
        {
            addressId.CheckLessThanOrEqual(0, nameof(addressId));

            _logger.LogInformation($"Delete contact info of contactId: '{addressId}'");
            var result = _addresses.Remove(addressId);

            return Ok(result);
        }

        [HttpGet, Route("addresses")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Get all addresses");

            var result = _addresses.GetAll();

            return Ok(result);
        }

        [HttpGet, Route("addresses/{id}")]
        public IActionResult GetAddressByAddressid(int id)
        {
            _logger.LogInformation($"Get address info for addressId: '{id}'");

            var result = _addresses.GetByAddressId(id);

            return Ok(result);
        }

        [HttpGet, Route("contacts/{contactId}/addresses")]
        public IActionResult GetAddressByContactId(int contactId)
        {
            _logger.LogInformation($"Get address info for contactId: '{contactId}'");

            var result = _addresses.GetByContactId(contactId);

            return Ok(result);
        }

        [HttpGet, Route("contacts/addresses/{zipcode}")]
        public IActionResult GetAddressByZipcode(string zipcode)
        {
            _logger.LogInformation($"Get address info for zipcode: '{zipcode}'");

            var result = _addresses.GetByZipCode(zipcode);

            return Ok(result);
        }
    }
}