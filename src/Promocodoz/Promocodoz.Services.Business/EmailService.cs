using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Promocodoz.Services.Business
{
    public class EmailService : IIdentityMessageService
    {
        const string Name = "PromocodOz";
        const string UserName = "mail@promocodoz.com";
        const string Password = "Pr0m0C0d0$";
        const string Host = "smtp.masterhost.ru";
        const int Port = 25;

        public Task SendAsync(IdentityMessage message)
        {
            var client = new SmtpClient(Host, Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = true
            };

            var email = new MailMessage(UserName, message.Destination)
            {
                From = new MailAddress(UserName, Name),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            return client.SendMailAsync(email);
        }
    }
}
