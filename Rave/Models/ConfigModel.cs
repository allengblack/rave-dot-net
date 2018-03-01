using System;
using System.Collections.Generic;

namespace RaveDotNet
{
    public class ConfigModel
    {
        public const string STAGING = "staging";
        public const string LIVE = "live";

        public string PublicKey { get; set; } = "APP-PUBLIC-KEY";
    
        public string SecretKey { get; set; } = "APP-SECRET-KEY";

        public string RedirectUrl { get;  set; }

        //Set as LIVE or STAGING
        public string Env { get;  set; }

        public List<string> Meta { get;  set; }
        public string PayButtonText { get;  set; }
        public string StagingUrl { get;  set; } = "https://rave-api-v2.herokuapp.com";
        public string LiveUrl { get;  set; } = "https://api.ravepay.co";
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

        public string GetUrl(string url) {
            return $"{this.BaseUrl}/{url}";
        }
    }
}
