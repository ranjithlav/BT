using AutoMapper;
using BT.Contacts.Application.Implementation;
using BT.Contacts.Infrastructure.Api.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ApplModels = BT.Contacts.Application.Models;
using DomainModels = BT.Contacts.Domain.Entities;

namespace BT.Contacts.Application.Tests
{
    public class AddressesShould
    {
        private readonly Mock<ILogger<Addresses>> _loggerMocked;
        private readonly Mock<IAddressRepo> _addressRepoMocked;
        private readonly Mock<IMapper> _mapperMocked;

        private readonly ApplModels.Address _addressAppModel1;

        private readonly DomainModels.Address _address1;
        private readonly DomainModels.Address _address2;
        private readonly List<DomainModels.Address> _addresses;

        private const int addressId1 = 1;
        private const int addressId2 = 2;
        private const int addressId3 = 3;

        private const int contactId11 = 11;
        private const int contactId12 = 12;

        private const string zipcode1 = "10001";
        private const string zipcode2 = "20002";

        public AddressesShould()
        {
            //Assign
            _loggerMocked = new Mock<ILogger<Addresses>>();
            _addressRepoMocked = new Mock<IAddressRepo>();
            _mapperMocked = new Mock<IMapper>();

            _addressAppModel1 = new ApplModels.Address
            {
                ContactId = contactId11,
                Street = "124",
                City = "Team city",
                State = "Hello State",
                ZipCode = zipcode1
            };

            _address1 = new DomainModels.Address
            {
                AddressId = addressId1,
                ContactId = contactId11,
                Street = "2727",
                City = "Houston",
                State = "Texas",
                ZipCode = zipcode1,
                CreatedDate = DateTime.Now.AddDays(-5),
                UpdatedDate = DateTime.Now.AddDays(-4)
            };

            _address2 = new DomainModels.Address
            {
                AddressId = addressId2,
                ContactId = contactId12,
                Street = "2727",
                City = "Austin",
                State = "Texas",
                ZipCode = zipcode2,
                CreatedDate = DateTime.Now.AddMonths(-5),
                UpdatedDate = DateTime.Now
            };

            _mapperMocked.Setup(x => x.Map<ApplModels.Address>(It.IsAny<DomainModels.Address>()))
                .Returns((DomainModels.Address source) => new ApplModels.Address()
                {
                    AddressId = addressId1,
                    ContactId = 11,
                    Street = "2727",
                    City = "Houston",
                    State = "Texas",
                    ZipCode = zipcode1
                });

            _mapperMocked.Setup(x => x.Map<IEnumerable<ApplModels.Address>>(It.IsAny<IEnumerable<DomainModels.Address>>()))
                .Returns((List<DomainModels.Address> source) => new List<ApplModels.Address>() {
                    new ApplModels.Address {
                    AddressId = addressId1,
                    ContactId = 11,
                    Street = "2727",
                    City = "Houston",
                    State = "Texas",
                    ZipCode = zipcode1
                },
                    new ApplModels.Address {
                    AddressId = addressId2,
                    ContactId = contactId11,
                    Street = "2727",
                    City = "Austin",
                    State = "Texas",
                    ZipCode = zipcode2
                } ,
                    new ApplModels.Address {
                    AddressId = addressId3,
                    ContactId = 11,
                    Street = "1111",
                    City = "Dallas",
                    State = "Texas",
                    ZipCode = zipcode1
                } });

            _addresses = new List<DomainModels.Address> { _address1, _address2 };
        }

        [Fact]
        private void ThrowExceptionWhenAddressIdIsZeroWhileGetByAddressId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.GetByAddressId(0));
        }

        [Fact]
        private void ThrowExceptionWhenAddressIdIsLessThanZeroWhileGetByAddressId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.GetByAddressId(-1));
        }

        [Fact]
        private void GetByAddressId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);
            var result = addresses.GetByAddressId(addressId1);

            //Assert
            result.Should().NotBeNull();
            result.AddressId.Should().Be(addressId1);
        }

        [Fact]
        private void ThrowExceptionWhenAddressIdIsZeroWhileGetByContactId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.GetByContactId(0));
        }

        [Fact]
        private void ThrowExceptionWhenAddressIdIsLessThanZeroWhileGetByContactId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.GetByContactId(-1));
        }

        [Fact]
        private void GetByContactId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByContactId(It.IsAny<int>())).Returns(_addresses);
            var result = addresses.GetByContactId(contactId11).ToList();

            //Assert
            result.Should().NotBeNull();
            result[0].AddressId.Should().Be(addressId1);
        }

        [Fact]
        private void ThrowExceptionWhenNullZipCodeWhileGetByContactId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentNullException>(() => addresses.GetByZipCode(null));
        }

        [Fact]
        private void ThrowExceptionWhenEmptyZipCodeWhileGetByContactId()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByAddressId(It.IsAny<int>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.GetByZipCode(string.Empty));
        }

        [Fact]
        private void GetByZipcode()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindByZipCode(It.IsAny<string>())).Returns(_addresses);
            var result = addresses.GetByZipCode(zipcode1).ToList();

            //Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result[0].ZipCode.Should().Be(zipcode1);
            result[1].ZipCode.Should().Be(zipcode2);
        }

        [Fact]
        private void GetAll()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.FindAll()).Returns(_addresses);
            var result = addresses.GetAll().ToList();

            //Assert
            result.Should().NotBeNull();
            result[0].AddressId.Should().Be(addressId1);
            result[1].AddressId.Should().Be(addressId2);
            result[2].AddressId.Should().Be(addressId3);
        }

        [Fact]
        private void ThrowExceptionWhenContactIdIsZero()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=> addresses.Add(new ApplModels.Address
            {
                ContactId = 0,
                Street = "124",
                City = "Team city",
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenContactIdIsLessthanZero()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = -1,
                Street = "124",
                City = "Team city",
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenStreetIsEmpty()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = string.Empty,
                City = "Team city",
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenStreetIsNull()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = null,
                City = "Team city",
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenCityIsEmpty()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = string.Empty,
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenCityIsNull()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = null,
                State = "Hello State",
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenStateIsEmpty()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = "City",
                State = string.Empty,
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void ThrowExceptionWhenZipcodeIsNull()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = "City",
                State = "state",
                ZipCode = null
            }));
        }

        [Fact]
        private void ThrowExceptionWhenZipcodeIsEmpty()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = "City",
                State = "state",
                ZipCode = string.Empty
            }));
        }

        [Fact]
        private void ThrowExceptionWhenStateIsNull()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);

            //Assert
            Assert.Throws<ArgumentException>(() => addresses.Add(new ApplModels.Address
            {
                ContactId = 1234,
                Street = "Street",
                City = "City",
                State = null,
                ZipCode = "16050"
            }));
        }

        [Fact]
        private void AddAddress()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Add(It.IsAny<DomainModels.Address>())).Returns(_address1);
            var result = addresses.Add(_addressAppModel1);

            //Assert
            result.Should().NotBeNull();
            result.ContactId.Should().Be(_address1.ContactId);
        }

        [Fact]
        private void ThrowExceptionWhenIdIsZeroWhileRemoveAddress()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            Assert.Throws<ArgumentOutOfRangeException>(()=> addresses.Remove(0));
        }

        [Fact]
        private void ThrowExceptionWhenIdIsLessthanZeroWhileRemoveAddress()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            Assert.Throws<ArgumentOutOfRangeException>(() => addresses.Remove(-1));
        }

        [Fact]
        private void RemoveAddress()
        {
            //Act
            var addresses = new Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);
            _addressRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            var result = addresses.Remove(123);

            //Assert
            result.Should().BeTrue();
        }
    }
}
