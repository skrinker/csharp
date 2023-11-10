using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Services
{
    class EmailSenderService : IEmailSenderService
    {
        static void sendEmail(string email, string data)
        {
            throw new NotImplementedException();
        }

        public void sendEmailWithAttachment(string senderEmail, string receiverEmail, string subject, string body, string attachmentPath)
        {
            string rootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string rootDirectory = Directory.GetParent(rootFolderPath).Parent.Parent.Parent.FullName;
            Console.WriteLine(rootDirectory);
            MailMessage mail = new MailMessage(senderEmail, receiverEmail, subject, body);
            Attachment attachment = new Attachment(attachmentPath);
            mail.Attachments.Add(attachment);

            SmtpClient smtpClient = new SmtpClient("smtp.example.com", 587); // Укажите SMTP-сервер и порт
            smtpClient.EnableSsl = true; // Если требуется SSL
            smtpClient.Credentials = new System.Net.NetworkCredential("ваш_логин", "ваш_пароль"); // Укажите учетные данные для аутентификации

            smtpClient.Send(mail);

            mail.Dispose();
            attachment.Dispose();
            smtpClient.Dispose();
        }

    }
}
