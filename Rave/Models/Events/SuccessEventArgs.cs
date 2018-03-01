using System;

namespace RaveDotNet.Models.Events
{
    public class SuccessEventArgs: EventArgs
    {
        public PaymentRequestModel Request { get; set; }
        public ResponseModel<PaymentResponseModel> Response { get; set; }
    }
}
