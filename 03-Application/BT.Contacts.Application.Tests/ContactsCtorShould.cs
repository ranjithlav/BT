using AutoMapper;
using BT.Contacts.Infrastructure.Api.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using Appl = BT.Contacts.Application.Implementation;

namespace BT.Contacts.Application.Tests
{
    public class ContactsCtorShould
    {
        Mock<ILogger<Appl.Contacts>> _loggerMocked;
        Mock<IContactRepo> _contactRepoMocked;
        Mock<IMapper> _mapperMocked;

        public ContactsCtorShould()
        {
            //Assign
            _loggerMocked = new Mock<ILogger<Appl.Contacts>>();
            _contactRepoMocked = new Mock<IContactRepo>();
            _mapperMocked = new Mock<IMapper>();
        }

        [Fact]
        private void ThrowNullExceptionWhenLoggerIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Contacts(null, _mapperMocked.Object, _contactRepoMocked.Object));
        }

        [Fact]
        private void ThrowNullExceptionWhenMapperIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Contacts(_loggerMocked.Object, null, _contactRepoMocked.Object));
        }

        [Fact]
        private void ThrowNullExceptionWhenContactRepoIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, null));
        }

        [Fact]
        private void ExecuteConstructor()
        {
            //Act
            var contacts = new Appl.Contacts(_loggerMocked.Object, _mapperMocked.Object, _contactRepoMocked.Object);

            //Assert
            contacts.Should().NotBeNull();
        }
    }
}
