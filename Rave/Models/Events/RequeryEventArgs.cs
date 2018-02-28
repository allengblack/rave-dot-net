using System;

namespace Rave.Models.Events
{
    public class RequeryEventArgs: EventArgs
    {
        public string TransactionReference { get; set; }
    }
}
