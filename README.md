# RAVE .NET SDK

Use this library to integrate your dotnet app to use the [Flutterwave Rave Payment Service](https://rave.flutterwave.com)

## Specs

This library targets the dotnet standard 2.0

## How to install

To add the package to your .NET standard or core app via nuget, use the following terminal command:

```bash
dotnet add package RaveDotNet
```

To install in a .NET framework application via the Package Management Console in Visual Studio:

```cmd
Install-Package RaveDotNet
```

## How to use

To initialize,

```cs
private ConfigModel config = new ConfigModel()
{
    Meta = new List<string>(),
    RedirectUrl = "https://your-app.com/rave", //callback to retrieve payment status
    Env = ConfigModel.STAGING, //or ConfigModel.LIVE (for production)
    PublicKey = "<YOUR-PUBLIC-KEY>",
    SecretKey = "<YOUR-SECRET-KEY>"
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
To specify a custom Transaction reference, while configuring your `PaymentRequestModel`, you can configure a transaction reference like so:

```cs
private PaymentRequestModel request = new PaymentRequestModel() {
// other configurations here...

    new PaymentRequestModel() {
        GetTransactionReference = () => "my-tx-ref-01"
    };
}
```

To render the payment page,

```cs
raveService.RenderHtml(); //will generate an html string to be loaded on the client browser
```

When the payment is done (success or failure), the user will be redirected to the Redirect URL you specified. You can retrieve the transaction reference from the URL as a query string.

To verify transaction,

```cs
var result = await raveService.RequeryTransaction("<THE-TRANSACTION-REFERENCE-YOU-RECEIVED>");

if (result.IsSuccessful()) {
    //do something
}
else if (result.IsFailed()) {
    //handle failure
}
else {
    //indecisive
}
```

You can also listen for events for tasks like logging transactions:

```cs
using Rave.Models.Events;
```

```cs
raveService.SuccessEvent += ((object sender, SuccessEventArgs e) => {
 //triggered on a successful payment
});

raveService.FailedEvent += ((object sender, CancelledEventArgs e) => {
 //triggered on a failed payment
});

raveService.InitEvent += ((object sender, InitEventArgs e) => {
 //triggered when you render html
});

raveService.RequeryEvent += ((object sender, RequeryEventArgs e) => {
 //triggered when a verification (requery) request is created
});

raveService.RequeryErrorEvent += ((object sender, RequeryErrorEventArgs e) => {
 //triggered when verification fails
});

raveService.TimeoutEvent += ((object sender, TimeoutEventArgs e) => {
 //triggered during verification request timeout
});
```

It's that easy to use!
