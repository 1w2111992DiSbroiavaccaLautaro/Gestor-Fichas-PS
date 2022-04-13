using ApiPracticaFinal.Models;
using ApiPracticaFinal.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.SendGrid
{
    public interface IMailService
    {
        //Task SendEmail(string email, string subject, string htmlContent);
        Task ExecuteMail(string email, string subject, string content);
        Task<ResultadosApi> ConfirmEmailAync(string userId, string token);
        Task<ResultadosApi> ConfirmEmailPassAync(string userId, string password);
    }
}
