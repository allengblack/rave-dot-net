using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Rave.Tests
{
    public class RaveTests
    {
        private ConfigModel config = new ConfigModel()
        {
            Meta = new List<string>()
        };
        private PaymentRequestModel request = new PaymentRequestModel();
        

        private RaveService _raveService;

        private void BeforeEach()
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

        [Fact]
        public void Can_Render_Html()
        {
            BeforeEach();

            var path = Directory.GetCurrentDirectory() + "../../../../../render.html";
            var html = _raveService.RenderHtml();
            System.IO.File.WriteAllText(path, html);

            Assert.IsType<string>(html);
        }
    }
}
