using System;
using Rave.Events;
using Rave.Models;

namespace Rave
{
    public class RaveService: RaveServiceEventHandler
    {
        public string TxnRef { get;  set; }
        public string IntegrityHash { get;  set; }
        public string TransactionPrefix { get;  set; }
        protected ConfigModel Config { get; set; }

        public RaveService(ConfigModel config) {
            this.Config = config;
        }
    }
}
