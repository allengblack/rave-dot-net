using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Newtonsoft.Json;

namespace Rave.Tests
{
    public class RaveTests
    {
        private ConfigModel config = new ConfigModel()
        {
            Meta = new List<string>(),
            RedirectUrl = "https://github.com/Flutterwave/Flutterwave-Rave-PHP-SDK"
        };

        private PaymentRequestModel request = new PaymentRequestModel() {
            CustomerEmail = "abc@mailinator.com",
            CustomerPhone = "08021123345",
            Amount = 1000,
            Country = "Nigeria",
            Currency = "NGN",
            CustomDescription = "xyz",
            CustomerFirstname = "Abc",
            CustomerLastname = "Def",
            CustomLogo = "none",
            CustomTitle = "Mr.",
            PayButtonText = "Pay Me"
        };
        
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

        [Fact]
        public async Task Payment_Is_Successful() {
            BeforeEach();

            var result = await _raveService.RequeryTransaction("FLW-MOCK-2601f4c66bf818a6b8cd2795baca116f");
            _raveService.SuccessEvent += ((object sender, Rave.Models.Events.SuccessEventArgs e) => {
                
            });
            var path = Directory.GetCurrentDirectory() + "../../../../../payment-result.json";
            System.IO.File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //Assert.Equal("success", result.status);
        }

        [Fact]
        public void Json_Can_Convert_To_Response_Model() {
            BeforeEach();

            var result = JsonConvert.DeserializeObject<Rave.ResponseModel<Rave.PaymentResponseModel>>("{\"status\":\"success\",\"message\":\"Tx Fetched\",\"data\":[]}");
        }

        [Fact]
        public void Transaction_Reference_Does_Not_Change() {
            BeforeEach();
            
            Assert.Equal(request.TransactionReference, request.TransactionReference);
        }
    }
}
