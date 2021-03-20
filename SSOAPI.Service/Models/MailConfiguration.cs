using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SSOAPI.Infra.Mail.Models
{
    public class MailConfiguration
    {
        public MailConfiguration(IConfiguration configuration)
        {
            Domain = configuration["Mail:Domain"];
            Port = configuration["Mail:Port"];
            Username = configuration["Mail:Username"];
            Password = configuration["Mail:Password"];
            From = configuration["Mail:FromEmail"];
        }

        public string Port { get; set; }
        public string From { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
