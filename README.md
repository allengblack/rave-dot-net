<h1>RAVE .NET SDK</h1>

Use this library to integrate your .NET app to Rave.

Begin by looking at the `ConfigModel` class and configuring your Secret and Public keys (remember not to add those to version control!!!). 

The value of the `Env` property of the `ConfigModel` (`LIVE` or `STAGING`) determines the `BaseUrl` or API endpoints used for your app.

Then you should populate a `PaymentRequestModel`. Edit this to contain all your request data however the request comes. It's really just a model.

Next up is to implement Rave’s integrity checksum flow into your app and here is where you get introduced to the `RaveService` class
which does all the heavy lifting for you. 

```
private ConfigModel config = new ConfigModel()
{
    Meta = new List<string>(),
    RedirectUrl = "https://github.com/Flutterwave/Flutterwave-Rave-PHP-SDK",
    Env = ConfigModel.STAGING
};
        
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

RaveService raveService = new RaveService(config, request);

```

The `RaveService`'s `CreateChecksum()` method returns a lowercase hashed string
of all your `request` data (from your `PaymentRequestModel` and the keys you setup in your `ConfigModel`) and sorts it for you!
This is your `integrity_hash` and it is ready to be sent to your client page.

For making your calls, you can use either the `RenderHtml()` or `RequeryTransaction()` methods.
`RenderHtml` redirects you to a modal where transactions can be made using your custom data from your request model, or in the case of a bad integrity check, an error message pops up.

```
string htmlSnippet = raveService.RenderHtml();

```

`RequeryTransaction` dumps the raw response from the developer for use as she sees fit.

```
ResponseModel<PaymentResponseModel> result = raveService.RequeryTransaction();

if(result.status == "success")
{
   //DO Something
}
else
{
   //DO Something else
}

```

It's that easy to use!




