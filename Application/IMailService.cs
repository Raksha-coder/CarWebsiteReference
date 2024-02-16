using Application.mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IMailService
    {
        string sendData(EmailData data);

        public void SendForgotPasswordLink(string email, string name, string id);
    }
}
