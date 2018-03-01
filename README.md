<h1>RAVE .NET SDK</h1>

Use this library to integrate your .NET app to Rave.

Begin by looking at the `ConfigModel` class and configuring your Secret and Public keys (remember not to add those to version control!!!). 
The value of the `Env` property of the `ConfigModel` (`LIVE` or `STAGING`) determines the `BaseUrl` or API endpoints used for your app.

```
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
```

Then you should populate a `PaymentRequestModel`. Edit this to contain all your request data however the request comes. It's really just a model.

```
private PaymentRequestModel request = new PaymentRequestModel() {
    CustomerEmail = "abc@mailinator.com",
    CustomerPhone = "08021123345",
    Amount = 1000,
    Country = "Nigeria",
    Currency = "NGN",
    CustomDescription = "xyz",
    CustomerFirstname = "Abc",
    CustomerLastname = "Def",
    CustomLogo = "none",
    CustomTitle = "Mr.",
    PayButtonText = "Pay Me"
};
```

Next up is to implement Rave’s integrity checksum flow into your app and here is where you get introduced to the `RaveService` class
which does all the heavy lifting for you. The `RaveService`'s `CreateChecksum()` method returns a lowercase hashed string
of all your `request` data (from your `PaymentRequestModel` and the keys you setup in your `ConfigModel`) and sorts it for you!
This is your `integrity_hash` and it is ready to be sent to your client page.

```
public string CreateCheckSum() {
    StringBuilder sb = new StringBuilder();

    sb.Append(this.Config.PublicKey);
    sb.Append(Convert.ToString(Convert.ToInt32(this.Request.Amount)));
    sb.Append(this.Request.Country);
    sb.Append(this.Request.Currency);
    sb.Append(this.Request.CustomDescription);
    sb.Append(this.Request.CustomLogo);
    sb.Append(this.Request.CustomTitle);
    sb.Append(this.Request.CustomerEmail);
    sb.Append(this.Request.CustomerFirstname);
    sb.Append(this.Request.CustomerLastname);
    sb.Append(this.Request.CustomerPhone);
    sb.Append(1); //HostedPayment
    sb.Append(this.Request.PayButtonText);
    sb.Append(this.Request.PaymentMethod);
    sb.Append(this.Config.RedirectUrl); 
    sb.Append(this.Request.TransactionReference);

    sb.Append(this.Config.SecretKey);

    string payload = sb.ToString();
    byte[] bytes = Encoding.UTF8.GetBytes(payload);

    byte[] transformedBytes = hash.ComputeHash(bytes);

    return this.IntegrityHash = string.Join("", transformedBytes.Select(bt => bt.ToString("x2"))).ToLower();
}
```

For making your calls, you can use either the `RenderHtml()` or `RequeryTransaction()` methods.
`RenderHtml` redirects you to a modal where transactions can be made using your custom data from your request model, or in the case of a bad integrity check, an error message pops up.

```
public string RenderHtml() {
    this.CreateCheckSum();
    var transactionData = this.TransactionData;
    this.OnInit(new InitEventArgs() {

    });

    string body = Newtonsoft.Json.JsonConvert.SerializeObject(transactionData, Newtonsoft.Json.Formatting.Indented);

    return $@"<html>
                <body>
                <center>Processing...<br /><img src=""ajax-loader.gif"" /></center>
                <script type=""text/javascript"" src='{this.Config.GetUrl("flwv3-pug/getpaidx/api/flwpbf-inline.js")}'></script>
                <script>
                    document.addEventListener(""DOMContentLoaded"", function(event) {{
                        var data = {body};
                        getpaidSetup(data);
                    }});
                </script>
                </body>
            </html>";
}
```

`RequeryTransaction` dumps the raw response from the developer for use as she sees fit.

```
public async Task<ResponseModel<PaymentResponseModel>> RequeryTransaction(string transactionReference = null) {
    var client = ApiClient.GetApiClient();
    this.RequeryCount++;
    transactionReference = transactionReference ?? this.Request.TransactionReference;
    this.OnRequery(new RequeryEventArgs() {
        TransactionReference = transactionReference
    });
    var body = new {
        txref = transactionReference,
        SECKEY = this.Config.SecretKey,
        last_attempt = 1
    };

    try {
        var result = await client.Post<ResponseModel<PaymentResponseModel>>(this.Config.GetUrl("flwv3-pug/getpaidx/api/xrequery"),                              ApiClient.GetJsonContent<object>(body));

        if (result.IsSuccessful()) {
            this.OnSuccess(new SuccessEventArgs() {
                Request = this.Request,
                Response = result
            });
        }
        else if (result.IsFailed()) {
            this.OnFailed(new FailedEventArgs() {
                Request = this.Request,
                Response = result
            });
        }
        else {
            if (this.RequeryCount >= 4) {
                this.OnTimeout(new TimeoutEventArgs() {
                    Request = this.Request,
                    Response = result
                });
            }
            else {
                System.Threading.Thread.Sleep(3000); //wait for 3 seconds before retrying
                return await this.RequeryTransaction(transactionReference);
            }

        }
        return result;
    }
    catch (Exception ex) {
        this.OnRequeryError(new RequeryErrorEventArgs() {
            Error = ex
        });
        throw ex;
    }
}
```

It's that easy to use!




