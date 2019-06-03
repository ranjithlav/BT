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
    public class AddressesCtorShould
    {
        Mock<ILogger<Appl.Addresses>> _loggerMocked;
        Mock<IAddressRepo> _addressRepoMocked;
        Mock<IMapper> _mapperMocked;

        public AddressesCtorShould()
        {
            //Assign
            _loggerMocked = new Mock<ILogger<Appl.Addresses>>();
            _addressRepoMocked = new Mock<IAddressRepo>();
            _mapperMocked = new Mock<IMapper>();
        }

        [Fact]
        private void ThrowNullExceptionWhenLoggerIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Addresses(null, _mapperMocked.Object, _addressRepoMocked.Object));
        }

        [Fact]
        private void ThrowNullExceptionWhenMapperIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Addresses(_loggerMocked.Object, null, _addressRepoMocked.Object));
        }

        [Fact]
        private void ThrowNullExceptionWhenAddressRepoIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new Appl.Addresses(_loggerMocked.Object, _mapperMocked.Object, null));
        }

        [Fact]
        private void ExecuteConstructor()
        {
            //Act
            var addresses = new Appl.Addresses(_loggerMocked.Object, _mapperMocked.Object, _addressRepoMocked.Object);

            //Assert
            addresses.Should().NotBeNull();
        }
    }
}
