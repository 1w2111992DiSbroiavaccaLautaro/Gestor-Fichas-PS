using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace ApiPracticaFinal.Repository.SendGrid
{
    public class MailService : IMailService
    {
        private readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string email, string subject, string htmlContent)
        {
            //string apiKey = configuration["SendGridApiKey"];
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("111992@tecnicatura.frc.utn.edu.ar", "Example User");
            //var to = new EmailAddress(email);
            //var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);

            var apiKey = configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("111992@tecnicatura.frc.utn.edu.ar", "Example User");
            var tema = subject;
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var contenidoHtml = htmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, tema, plainTextContent, contenidoHtml);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
