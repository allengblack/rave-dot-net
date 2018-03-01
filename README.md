<h1>RAVE .NET SDK</h1>

Use this library to integrate your .NET app to Rave.

Begin by looking at the `ConfigModel` class and configuring your Secret and Public keys (remember not to add those to version control!!!). 
The value of the `Env` property of the `ConfigModel` (`LIVE` or `STAGING`) determines the `BaseUrl` or API endpoints used for your app.

Then you should populate a `PaymentRequestModel`. Edit this to contain all your request data however it comes. 

Next up is to implement Rave’s integrity checksum flow into your app and here is where you get introduced to the `RaveService` class
which does all the heavy lifting for you. The `RaveService`'s `CreateChecksum()` method returns a lowercase hashed string
of all your `request` data (from your `PaymentRequestModel` and the keys you setup in your `ConfigModel`) and sorts it for you!
This is your `integrity_hash` and it is ready to be sent to your client page.

For making your calls, you can use either the `RenderHtml()` or `RequeryTransaction()` methods. 
`RenderHtml` redirects you to a modal where transactions can be made using your custom data from your request model, or in the case of a bad
integrity check, an error message pops up.

`RequeryTransaction` dumps the raw response from the developer for use as she sees fit.

It's that easy to use!




