using System;

namespace Rave
{
    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public string CustomDescription { get; set; }
        public string CustomLogo { get; set; }
        public string CustomTitle { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; } = "both";
    }
}
