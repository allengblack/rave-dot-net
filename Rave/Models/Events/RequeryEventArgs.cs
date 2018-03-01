using System;

namespace RaveDotNet.Models.Events
{
    public class RequeryEventArgs: EventArgs
    {
        public string TransactionReference { get; set; }
    }
}
