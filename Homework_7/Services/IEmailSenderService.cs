using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Services
{
    interface IEmailSenderService
    {
        public void sendEmailWithAttachment(string senderEmail, string receiverEmail, string subject, string body, string attachmentPath);
    }
}
