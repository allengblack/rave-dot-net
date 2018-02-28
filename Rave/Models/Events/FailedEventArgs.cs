using System;

namespace Rave.Models.Events
{
    public class FailedEventArgs: EventArgs
    {
        public PaymentRequestModel Request { get; set; }
        public ResponseModel<PaymentResponseModel> Response { get; set; }
    }
}
