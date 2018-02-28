using System;
using Rave.Events;
using Rave.Models;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Rave
{
    public class RaveService: RaveServiceEventHandler
    {
        public int RequeryCount { get; set; }
        public string IntegrityHash { get;  set; }
        protected ConfigModel Config { get; set; }
        protected PaymentRequestModel Request { get; set; }

        protected SHA256Managed hash;

        public RaveService(ConfigModel config, PaymentRequestModel request) {
            this.Config = config;
            this.Request = request;
            this.hash = new SHA256Managed();
        }

        // "amount" => $this->amount, 
        //     "customer_email" => $this->customerEmail, 
        //     "customer_firstname" => $this->customerFirstname, 
        //     "txref" => $this->txref, 
        //     "payment_method" => $this->paymentMethod, 
        //     "customer_lastname" => $this->customerLastname, 
        //     "country" => $this->country, 
        //     "currency" => $this->currency, 
        //     "custom_description" => $this->customDescription, 
        //     "custom_logo" => $this->customLogo, 
        //     "custom_title" => $this->customTitle, 
        //     "customer_phone" => $this->customerPhone,
        //     "pay_button_text" => $this->payButtonText,
        //     "redirect_url" => $this->redirectUrl,
        //     "hosted_payment" => 1

        public string CreateCheckSum() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Config.PublicKey);
            sb.Append(Convert.ToString(this.Request.Amount));
            sb.Append(this.Request.CustomerEmail);
            sb.Append(this.Request.CustomerFirstname);
            sb.Append(this.Request.TransactionReference);
            sb.Append(this.Request.PaymentMethod);
            sb.Append(this.Request.CustomerLastname);
            sb.Append(this.Request.Country);
            sb.Append(this.Request.Currency);
            sb.Append(this.Request.CustomDescription);
            sb.Append(this.Request.CustomLogo);
            sb.Append(this.Request.CustomTitle);
            sb.Append(this.Request.CustomerPhone);
            sb.Append(this.Request.PayButtonText);
            sb.Append(this.Config.RedirectUrl);
            sb.Append(1); //HostedPayment
            sb.Append(this.Config.SecretKey);

            string payload = sb.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            
            byte[] transformedBytes = hash.ComputeHash(bytes);
            
            return this.IntegrityHash = Encoding.UTF8.GetString(transformedBytes);
        }
    }
}
