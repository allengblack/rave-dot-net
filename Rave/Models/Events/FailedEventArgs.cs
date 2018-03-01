using System;

namespace RaveDotNet.Models.Events
{
    public class FailedEventArgs: EventArgs
    {
        public PaymentRequestModel Request { get; set; }
        public ResponseModel<PaymentResponseModel> Response { get; set; }
    }
}
