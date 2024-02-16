
using Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static System.Net.WebRequestMethods;

namespace Infrastructure
{
    public class TwilioSMS 
    {
        public string? To { get; set; }
        public string? Otp { get; set; }
        public TwilioSMS(string? to, string? otp) {
            To = to;
            Otp = otp;
        }

        public void SendSms()
        {
            twiliOtp tw = new twiliOtp();
            var accountSid = tw.AccountSID;
            var authToken = tw.AuthToken;
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber("+91" + To));
            messageOptions.From = new PhoneNumber(tw.From);
            messageOptions.Body = "Your One Time Login OTP Is : " + Otp;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }








    }
}
