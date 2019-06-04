using AutoMapper;
using ApplModels = BT.Contacts.Application.Models;
using DomainModels = BT.Contacts.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Appl = BT.Contacts.Application.Implementation;
using BT.Contacts.Infrastructure.Api.Repository;
using System.Linq;

namespace BT.Contacts.Application.Tests
{
    public class ContactsShould
    {
        private readonly Mock<ILogger<Appl.Contacts>> _loggerMocked;
        private readonly Mock<IContactRepo> _contactRepoMocked;
        private readonly Mock<IMapper> _mapperMocked;

        private readonly ApplModels.Contact _appContact1;
        private readonly ApplModels.Address _addressAppModel1;

        private readonly DomainModels.Contact _contact1;
        private readonly DomainModels.Contact _contact2;
        private readonly List<DomainModels.Contact> _contacts;

        private const string FirstName = "John";
        private const string LastName = "Doe";
        private const string BusinessName = "Bank of America";
        private const int addressId1 = 1;
        private const int addressId2 = 2;
        private const int addressId3 = 3;

        private const int contactId11 = 11;
        private const int contactId12 = 12;

        private const string zipcode1 = "10001";
        private const string zipcode2 = "20002";

        public ContactsShould()
        {
            //Assign
            _loggerMocked = new Mock<ILogger<Appl.Contacts>>();
            _contactRepoMocked = new Mock<IContactRepo>();
            _mapperMocked = new Mock<IMapper>();

            _addressAppModel1 = new ApplModels.Address
            {
                ContactId = contactId11,
                Street = "124",
                City = "Team city",
                State = "Hello State",
                ZipCode = zipcode1
            };

            _appContact1 = new ApplModels.Contact
            {
                FirstName = "John",
                LastName = "Doe",
                BusinessName = "NA"
            };

            _contact1 = new DomainModels.Contact
            {
                ContactId = 1,
                FirstName = "John",
                LastName = "Doe",
                BusinessName = "NA",
                Addresses = new List<DomainModels.Address> { new DomainModels.Address {
                    Street = "street",
                    City = "city",
                    State = "state",
                    ContactId = 1,
                    ZipCode = "123456"
                } },
                CreatedDate = DateTime.Now.AddDays(-5),
                UpdatedDate = DateTime.Now.AddDays(-4)
            };

            _contact2 = new DomainModels.Contact
            {
                ContactId = 2,
                FirstName = "BoA",
                LastName = "BoA",
                BusinessName = "Bank Of America",
                CreatedDate = DateTime.Now.AddMonths(-5),
                UpdatedDate = DateTime.Now
            };

            _mapperMocked.Setup(x => x.Map<ApplModels.Address>(It.IsAny<DomainModels.Address>()))
                .Returns((DomainModels.Address source) => new ApplModels.Address()
                {
                    AddressId = 123,
                    ContactId = 11,
                    Street = "2727",
                    City = "Houston",
                    State = "Texas",
                    ZipCode = "123456"
                });

            _mapperMocked.Setup(x => x.Map<ApplModels.Contact>(It.IsAny<DomainModels.Contact>()))
                .Returns((DomainModels.Contact source) => new ApplModels.Contact()
                {
                    ContactId = 1,
                    FirstName = FirstName,
                    LastName = LastName,
                    BusinessName = BusinessName
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

            _mapperMocked.Setup(x => x.Map<IEnumerable<ApplModels.Contact>>(It.IsAny<IEnumerable<DomainModels.Contact>>()))
                .Returns((List<DomainModels.Contact> source) => new List<ApplModels.Contact>() {
                    new ApplModels.Contact {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BusinessName = "NA",
                    Addresses = new List<ApplModels.Address>{
                        new ApplModels.Address { ContactId = 1, Street = "Street", City = "City"}
                    }
                },
                    new ApplModels.Contact {
                    ContactId = 2,
                    FirstName = "BoA",
                    LastName = "BoA",
                    BusinessName = "Bank Of America"
                } });

            _contacts = new List<DomainModels.Contact> { _contact1, _contact2 };
        }

        [Fact]
        private void ThrowExceptionWhenFirstLastAndBusinessNamesEmptyWhileAdd()
        {
            //Act
            var obj = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            Assert.Throws<ArgumentException>(() => obj.Add(new ApplModels.Contact
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                BusinessName = string.Empty
            }));
        }

        [Fact]
        private void ThrowExceptionWhenFirstEmptyWhileAdd()
        {
            //Act
            var obj = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            Assert.Throws<ArgumentException>(() => obj.Add(new ApplModels.Contact
            {
                FirstName = string.Empty,
                LastName = "LastName",
                BusinessName = string.Empty
            }));
        }

        [Fact]
        private void ThrowExceptionWhenLastEmptyWhileAdd()
        {
            //Act
            var obj = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            Assert.Throws<ArgumentException>(() => obj.Add(new ApplModels.Contact
            {
                FirstName = "FirstName",
                LastName = string.Empty,
                BusinessName = string.Empty
            }));
        }

        [Fact]
        private void AddContactWithFirstAndLastNames()
        {
            //Act
            var contacts = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            var result = contacts.Add(new ApplModels.Contact
            {
                FirstName = FirstName,
                LastName = LastName,
                BusinessName = string.Empty
            });

            //Assert
            result.FirstName.Should().Be(FirstName);
            result.LastName.Should().Be(LastName);
        }

        [Fact]
        private void AddContactWithBusinessName()
        {
            //Act
            var contacts = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            var result = contacts.Add(new ApplModels.Contact
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                BusinessName = BusinessName
            });

            //Assert
            result.BusinessName.Should().Be(BusinessName);
        }

        [Fact]
        private void AddContactWithBusinessNameAndAddress()
        {
            //Act
            var contacts = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            var result = contacts.Add(new ApplModels.Contact
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                BusinessName = BusinessName,
                Addresses = new List<ApplModels.Address> { new ApplModels.Address { Street = "street", City = "city", State = "state", ZipCode = "10001"} }
            });

            //Assert
            result.BusinessName.Should().Be(BusinessName);
        }

        [Fact]
        private void ThrowExceptionWhenContactIdIsLessThanZeroWhileGet()
        {
            //Act
            var obj = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Get(-1));
        }

        [Fact]
        private void ThrowExceptionWhenContactIdIsZeroWhileGet()
        {
            //Act
            var obj = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Get(0));
        }

        [Fact]
        private void GetResult()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.Get(It.IsAny<int>())).Returns(_contact1);

            var contactResult = contact.Get(1);

            //Assert
            contactResult.Should().NotBeNull();
            contactResult.ContactId.Should().Be(1);
        }

        [Fact]
        private void GetAllWithNoResult()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.GetAll()).Returns(new List<DomainModels.Contact>());

            var contactsResult = contact.GetAll();

            //Assert
            contactsResult.Should().BeNull();
        }

        [Fact]
        private void GetAllResult()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.GetAll()).Returns(_contacts);

            var contactsResult = contact.GetAll().ToList();

            //Assert
            contactsResult.Should().NotBeNull();
            contactsResult[0].ContactId.Should().Be(1);
            contactsResult[1].ContactId.Should().Be(2);
        }

        [Fact]
        private void ThrowExceptionWhenEmptyZipcode()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.GetAll(It.IsAny<string>())).Returns(_contacts);

            Assert.Throws<ArgumentException>(() => contact.GetAll(string.Empty).ToList());
        }

        [Fact]
        private void ThrowExceptionWhenNullZipcode()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.GetAll(It.IsAny<string>())).Returns(_contacts);

            Assert.Throws<ArgumentNullException>(() => contact.GetAll(null).ToList());
        }

        [Fact]
        private void GetAllByZipcode()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.GetAll(It.IsAny<string>())).Returns(_contacts);

            var contactsResult = contact.GetAll("12345").ToList();

            //Assert
            contactsResult.Should().NotBeNull();
            contactsResult[0].ContactId.Should().Be(1);
            contactsResult[0].Addresses.Count.Should().Be(1);

            contactsResult[1].ContactId.Should().Be(2);
        }

        [Fact]
        private void ThrowExceptionWhenIdIsZeroWhileRemoveAddress()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            Assert.Throws<ArgumentOutOfRangeException>(() => contact.Remove(0));
        }

        [Fact]
        private void ThrowExceptionWhenIdIsLessthanZeroWhileRemoveAddress()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            Assert.Throws<ArgumentOutOfRangeException>(() => contact.Remove(-1));
        }

        [Fact]
        private void RemoveAddress()
        {
            //Act
            var contact = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);
            _contactRepoMocked.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            var result = contact.Remove(123);

            //Assert
            result.Should().BeTrue();
        }
    }
}
