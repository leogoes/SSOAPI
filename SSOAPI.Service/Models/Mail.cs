using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SSOAPI.Infra.Mail.Models
{
    public class Mail : MailMessage
    {
        public Mail(string to, bool formatIsHtml, string subject, string emailTemplate)
        {
            To.Add(to);
            Subject = subject;
            IsBodyHtml = formatIsHtml;
            Body = emailTemplate;
        }

    }
}
