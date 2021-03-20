using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.Extensions.Configuration; //using Microsoft.AspNetCore.Hosting;

namespace SSOAPI.Infra.Mail
{
    public class MailBuilder
    {
        
        private readonly IConfiguration _configuration;
        //private readonly IWebHostEnvironment _appEnvironment;
        private readonly MailMessage _mailMessage;

        public MailBuilder(MailMessage mailMessage ,IConfiguration configuration)
        {
            _configuration = configuration;
            //_appEnvironment = env;
            _mailMessage = mailMessage;
        }
        public MailMessage PreparaTemplate(string templateAssunto, string templateFileName,
            Dictionary<string, string> templateVariables)
        {
            var modeloTemplate = GetTemplateContent(templateFileName);

            modeloTemplate = MontaTemplateHeader(modeloTemplate, templateAssunto);
            modeloTemplate = MontaTemplateBody(modeloTemplate, templateVariables);

            GetMailConfiguration(templateAssunto, modeloTemplate);

            return _mailMessage;
        }

        private void GetMailConfiguration(string templateAssunto, string modeloTemplate)
        {

            _mailMessage.Body = modeloTemplate;
            _mailMessage.Subject = templateAssunto;
            _mailMessage.From = new MailAddress(_configuration["Mail:FromEmail"]);
            _mailMessage.IsBodyHtml = true;
        }


        private string GetTemplateContent(string templateFileName)
        {
            //return System.IO.File.ReadAllText($"{_appEnvironment.WebRootPath}\\Templates\\{templateFileName}.html");
            return System.IO.File.ReadAllText($"C:\\Users\\leonardo.souza\\Documents\\Leonardo\\Projects\\Random\\SSOAPI\\SSOAPI\\SSOAPI.Service\\Templates\\{templateFileName}.html");
        }

        private string MontaTemplateHeader(string modeloTemplate, string templateAssunto)
        {
            modeloTemplate = modeloTemplate.Replace("#TITULO", templateAssunto);
            return modeloTemplate;
        }

        private string MontaTemplateBody(string modeloEmail, Dictionary<string, string> templateVariables)
        {
            foreach (var variable in templateVariables)
            {
                modeloEmail = modeloEmail.Replace(variable.Key, variable.Value);

            }
            return modeloEmail;
        }
    }
}
