using System;

namespace Rave.Models.Events
{
    public class RequeryErrorEventArgs: EventArgs
    {
        public PaymentRequestModel Request { get; set; }
        public Exception Error { get; set; }   
    }
}
