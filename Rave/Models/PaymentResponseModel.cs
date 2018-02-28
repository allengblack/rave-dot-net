using System;
using Newtonsoft.Json;

namespace Rave
{
    public class ResponseModel<TData>
    {
        public string status { get; set; }
        public string message { get; set; }
        public TData data { get; set; }
    }

    public class PaymentResponseModel {
        public long id { get; set; }
        public string txref { get; set; }
        public string txid { get; set; }
        public string flwref { get; set; }
        public string devicefingerprint { get; set; }
        public string cycle { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string chargedamount { get; set; }
        public string appfee { get; set; }
        public string merchantfee { get; set; }
        public string merchantbearsfee { get; set; }
        public string chargecode { get; set; }
        public string chargemessage { get; set; }
        public string authmodel { get; set; }
        public string ip { get; set; }
        public string narration { get; set; }
        public string status { get; set; }
        public string vbvmessage { get; set; }
        public string vbvcode { get; set; }
        public string acctmessage { get; set; }
        public string acctcode { get; set; }
        public string paymenttype { get; set; }
        public string paymentid { get; set; }
        public string pcard6 { get; set; }
        public string pcardl4 { get; set; }
        public string pcardexpmonth { get; set; }
        public string pcardexpyear { get; set; }
        public string pcardcountry { get; set; }
        public string pcardisnigerian { get; set; }
        public string pcardisblacklisted { get; set; }
        public string pcardname { get; set; }
        public string pcardtype { get; set; }
        public string pcardcreated { get; set; }
        public string paccountnumber { get; set; }
        public string paccountbank { get; set; }
        public string paccountbankname { get; set; }
        public string paccountfirstname { get; set; }
        public string paccountlastname { get; set; }
        public string paccountcreated { get; set; }
        public string chargetype { get; set; }
        public string fraudstatus { get; set; }
        public string created { get; set; }
        public string customerid { get; set; }
        public string custphone { get; set; }
        public string custnetworkprovider { get; set; }
        public string custname { get; set; }
        public string custemail { get; set; }
        public string custemailprovider { get; set; }
        public string custcreated { get; set; }
        public string accountid { get; set; }
        public string acctbusinessname { get; set; }
        public string acctcontactperson { get; set; }
        public string acctcountry { get; set; }
        public string acctbearsfeeattransactiontime { get; set; }
        public string acctmarkupfee { get; set; }
        public string acctparent { get; set; }
        public string acctparentbusinessname { get; set; }
        public string acctparentcontactperson { get; set; }
        public string acctparentcountry { get; set; }
        public string acctvpcmerchant { get; set; }
        public string acctalias { get; set; }
        public string orderref { get; set; }
        public string amountsettledforthistransaction { get; set; }
    }
}
