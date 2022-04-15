using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ApiPracticaFinal.Models;
using ApiPracticaFinal.Repository.Usuarios;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using ApiPracticaFinal.Resultados;
using Microsoft.Extensions.Options;

namespace ApiPracticaFinal.Repository.SendGrid
{
    public class MailService : IMailService
    {
        //private readonly IConfiguration configuration;
        private readonly dd4snj9pkf64vpContext context;
        private readonly Settings _settings;

        public MailService(dd4snj9pkf64vpContext context, IOptions<Settings> optionSetting)
        {
            this.context = context;
            _settings = optionSetting.Value;
        }

        public async Task<ResultadosApi> ConfirmEmailAync(string userId, string token)
        {
            ResultadosApi resultados = new ResultadosApi();
            var id = Convert.ToInt32(userId);
            var user = await context.Usuarios.FindAsync(id);
            if(user == null)
            {
                throw new Exception("No encontrado");
            }
            else
            {
                var decodedToken = WebEncoders.Base64UrlDecode(token);
                string normaltoken = Encoding.UTF8.GetString(decodedToken);

                //string c = Convert.ToBase64String(decodedToken);

                //string b = normaltoken.Split(new char { ',' }, StringSplitOptions.None);

                var result = await ConfirmEmailAync(userId, normaltoken);
                if(result != null)
                {
                    resultados.Ok = true;
                    resultados.Respuesta = result;
                    resultados.InfoAdicional = "Email confirmado";
                    return resultados;
                }
                else
                {
                    resultados.Ok = false;
                    resultados.Error = "Email no confrimado";
                    return resultados;
                }
            }
        }

        public async Task<ResultadosApi> ConfirmEmailPassAync(string userId, string password)
        {
            ResultadosApi resultados = new ResultadosApi();
            var id = Convert.ToInt32(userId);
            var user = await context.Usuarios.FindAsync(id);
            if (user == null)
            {
                throw new Exception("No encontrado");
            }
            else
            {                
                if (password.Equals(user.Password))
                {
                    resultados.Ok = true;
                    user.Activo = true;
                    context.Usuarios.Update(user);
                    await context.SaveChangesAsync();
                    resultados.Respuesta = user;
                    resultados.InfoAdicional = "Email confirmado";
                    return resultados;
                    
                    //else
                    //{
                    //    resultados.Ok = false;
                    //    resultados.Error = "Email no confrimado";
                    //    return resultados;
                    //}
                }
                else
                {
                    resultados.Ok = false;
                    resultados.Error = "No se pudo registrar";
                    return resultados;
                }
            }
            
        }

        public async Task ExecuteMail(string email, string subject, string content)
        {
            //var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            //var apiKey = configuration["SendGridApiKey"];
            var apiKey = _settings.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("lautarodisbro@gmail.com", "Example User");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
