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

        private DomainModels.Contact contact1;
        private DomainModels.Contact contact2;
        private List<DomainModels.Contact> contacts;

        public ContactsShould()
        {
            //Assign
            _loggerMocked = new Mock<ILogger<Appl.Contacts>>();
            _contactRepoMocked = new Mock<IContactRepo>();
            _mapperMocked = new Mock<IMapper>();

            contact1 = new DomainModels.Contact
            {
                ContactId = 1,
                FirstName = "John",
                LastName = "Doe",
                BusinessName = "NA",
                CreatedDate = DateTime.Now.AddDays(-5),
                UpdatedDate = DateTime.Now.AddDays(-4)
            };

            contact2 = new DomainModels.Contact
            {
                ContactId = 2,
                FirstName = "BoA",
                LastName = "BoA",
                BusinessName = "Bank Of America",
                CreatedDate = DateTime.Now.AddMonths(-5),
                UpdatedDate = DateTime.Now
            };

            _mapperMocked.Setup(x => x.Map<ApplModels.Contact>(It.IsAny<DomainModels.Contact>()))
                .Returns((DomainModels.Contact source) => new ApplModels.Contact()
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BusinessName = "NA",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    UpdatedDate = DateTime.Now.AddDays(-4)
                });

            _mapperMocked.Setup(x => x.Map<IEnumerable<ApplModels.Contact>>(It.IsAny<IEnumerable<DomainModels.Contact>>()))
                .Returns((List<DomainModels.Contact> source) => new List<ApplModels.Contact>() {
                    new ApplModels.Contact {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BusinessName = "NA",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    UpdatedDate = DateTime.Now.AddDays(-4)
                },
                    new ApplModels.Contact {
                    ContactId = 2,
                    FirstName = "BoA",
                    LastName = "BoA",
                    BusinessName = "Bank Of America",
                    CreatedDate = DateTime.Now.AddMonths(-5),
                    UpdatedDate = DateTime.Now
                } });

            contacts = new List<DomainModels.Contact> { contact1, contact2 };
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
            _contactRepoMocked.Setup(repo => repo.Get(It.IsAny<int>())).Returns(contact1);

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
            _contactRepoMocked.Setup(repo => repo.GetAll()).Returns(contacts);

            var contactsResult = contact.GetAll().ToList();

            //Assert
            contactsResult.Should().NotBeNull();
            contactsResult[0].ContactId.Should().Be(1);
            contactsResult[1].ContactId.Should().Be(2);
        }
    }
}
