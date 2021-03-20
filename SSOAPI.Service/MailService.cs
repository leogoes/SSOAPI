using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SSOAPI.Infra.Mail.Models;

namespace SSOAPI.Infra.Mail
{
    public class MailService
    {
        private readonly MailConfiguration _mailConfiguration;
        private readonly MailMessage _mail;

        public MailService(MailMessage mail, IConfiguration configuration)
        {
            _mailConfiguration = new MailConfiguration(configuration);
            _mail = mail;
        }

        public void Send()
        {
            var client = new SmtpClient(_mailConfiguration.Domain, Convert.ToInt32(_mailConfiguration.Port))
            {
                Credentials = new NetworkCredential(_mailConfiguration.Username, _mailConfiguration.Password),
                EnableSsl = true
            };

            client.Send(_mail);
        }
    }
}
