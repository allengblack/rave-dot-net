using System;
using System.Collections.Generic;

namespace Rave
{
    public class ConfigModel
    {
        public const string STAGING = "staging";
        public const string LIVE = "live";

        public string PublicKey { get;  set; }
        public string SecretKey { get;  set; }
        public string RedirectUrl { get;  set; }
        public string Env { get;  set; }
        public List<string> Meta { get;  set; }
        public string PayButtonText { get;  set; }
        public string StagingUrl { get;  set; }
        public string LiveUrl { get;  set; }
        public string BaseUrl { 
            get {
                if (this.Env == LIVE) {
                    return this.LiveUrl;
                }
                else {
                    return this.StagingUrl;
                }
            }
        }
    }
}
