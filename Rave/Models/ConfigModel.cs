using System;
using System.Collections.Generic;

namespace Rave
{
    public class ConfigModel
    {
        public const string STAGING = "staging";
        public const string LIVE = "live";

        public string PublicKey { get; set; } = "FLWPUBK-c9bb5a3dade3ca9e8dce44569b628437-X";
    
        public string SecretKey { get; set; } = "FLWSECK-fc3a929f74002bae3d5fd08edfc05926-X";

        public string RedirectUrl { get;  set; }
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
