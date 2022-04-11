using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.SendGrid
{
    public interface IMailService
    {
        //Task SendEmail(string email, string subject, string htmlContent);
        Task ExecuteMail();
    }
}
