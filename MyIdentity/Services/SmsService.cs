using System.Net;

namespace MyIdentity.Services;

    public class SmsService
    {
        public void Send(string PhoneNumber,string Code)
        {
            var client = new WebClient();                 //apikey= sms provider give you the value
            string url = $"http://panel.kavenegar.com/v1/apikey/verify/lookup.json?receptor={PhoneNumber}&token={Code}&template=VerifyAccount";  //template = you set it in the site of sms provider
            var content= client.DownloadString(url);

        }
    }

