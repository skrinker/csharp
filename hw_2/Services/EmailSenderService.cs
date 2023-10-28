using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Services
{
    class EmailSenderService : IEmailSenderService
    {
        Task IEmailSenderService.sendEmail(string email, string data)
        {
            throw new NotImplementedException();
        }
    }
}
