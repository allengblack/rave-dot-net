using System;

namespace RaveDotNet.Models.Events
{
    public class RequeryErrorEventArgs: EventArgs
    {
        public PaymentRequestModel Request { get; set; }
        public Exception Error { get; set; }   
    }
}
