using System;
using Rave.Events;
using Rave.Models;
using Rave.Models.Events;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Rave.Helpers;

namespace Rave
{
    public class RaveService: RaveServiceEventHandler
    {
        public int RequeryCount { get; set; }
        public string IntegrityHash { get;  set; }
        protected ConfigModel Config { get; set; }
        protected PaymentRequestModel Request { get; set; }
        protected dynamic TransactionData { get; set; }

        protected SHA256Managed hash;

        public RaveService(ConfigModel config, PaymentRequestModel request) {
            this.Config = config;
            this.Request = request;
            this.hash = new SHA256Managed();
        }

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
                var result = await client.Post<ResponseModel<PaymentResponseModel>>(this.Config.GetUrl("/flwv3-pug/getpaidx/api/xrequery"), ApiClient.GetJsonContent<object>(body));

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
            }
            return null;
        }
    }
}
