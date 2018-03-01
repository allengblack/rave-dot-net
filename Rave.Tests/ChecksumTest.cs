using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Rave.Tests
{
    public class RaveTests
    {
        private ConfigModel config = new ConfigModel();
        private PaymentRequestModel request = new PaymentRequestModel();

        private RaveService _raveService;

        public void BeforeEach()
        {
            _raveService = new RaveService(config, request);
        }

        [Fact]
        public void Can_Create_Checksum()
        {
            BeforeEach();

            var checksum = _raveService.CreateCheckSum();
            Assert.IsType<string>(checksum);
        }
    }
}
