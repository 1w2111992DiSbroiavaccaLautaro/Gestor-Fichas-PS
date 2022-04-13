using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.UsuariosDTO;
using ApiPracticaFinal.Repository.SendGrid;
using ApiPracticaFinal.Repository.Usuarios;
using ApiPracticaFinal.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPracticaFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMailService mailService;
        private readonly IConfiguration configuration;
        public UsuariosController(IUsuarioRepository usuarioRepository, IMailService mailService, IConfiguration configuration)
        {
            this.usuarioRepository = usuarioRepository;
            this.mailService = mailService;
            this.configuration = configuration;
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            return await usuarioRepository.GetUsuariosAsync();
        }

        //[HttpGet("sendMail")]
        //public async Task<IActionResult> SendEmail()
        //{
        //    //await emailSender.SendEmailAsync(email, tema, mensaje);
        //    await mailService.ExecuteMail("lautarodisbro@gmail.com", "Login");
        //    return Ok();
        //}

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        //[HttpPost]
        //public async Task<Usuario> Post(UsuarioSignUp usu)
        //{
        //    var usaurio = await usuarioRepository.Signup(usu);
        //    var confirmToken = usuarioRepository.Authenticate(usu.Email, usu.Password);
        //    var encodedToken = Encoding.UTF8.GetBytes(confirmToken);
        //    var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);
        //    string url = $"{configuration["AppUrl"]}/api/usuarios/confirmemail?userid={usaurio.Idusuario}&token={validEmailToken}";
        //    await mailService.ExecuteMail(usu.Email, "Confirmar el mail", "<h1>Bienvenido a la confirmacion de mail</h1>" +
        //        $"<p>Porfavor confirma su mail by <a href='{url}'>Click aca</a></p");
        //    return usaurio;
        //}

        [HttpPost]
        public async Task<Usuario> Post(UsuarioSignUp usu)
        {
            var usuario = await usuarioRepository.Signup(usu);
            //var confirmToken = usuarioRepository.Authenticate(usu.Email, usu.Password);
            //var encodedToken = Encoding.UTF8.GetBytes(confirmToken);
            //var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);
            string url = $"{configuration["AppUrl"]}/api/usuarios/confirmemail?userid={usuario.Idusuario}&pass={usu.Password}";
            await mailService.ExecuteMail(usu.Email, "Confirmar el mail", "<h1>Bienvenido a la confirmacion de mail</h1>" +
                $"<p>Porfavor confirma su mail by <a href='{url}'>Click aca</a></p");
            return usuario;
        }

        [HttpPost("Login")]
        public ActionResult<ResultadosApi> Login(UsuarioLogin usu)
        {
            var usuario = usuarioRepository.Login(usu);
            if (usuario.Ok)
            {
                mailService.ExecuteMail(usu.Email, "Nuevo login", "<h1>Nuevo login a las " + DateTime.Now + "</h1>");
            }
            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("UpdatePassword")]
        public async Task<bool> UpdatePassword(UsuarioUpdatePass usuario)
        {
            return await usuarioRepository.UpdatePassword(usuario);
        }

        [HttpPut("UpdateRol")]
        public async Task<Usuario> UpdateRol(UsuarioRolDTO usu)
        {
            return await usuarioRepository.UpdateRol(usu);
        }

        [HttpPut("UpdateCredenciales")]
        public async Task<Usuario> UpdateCredenciales(UsuarioCredencialDTO usu)
        {
            return await usuarioRepository.UpdateCredenciales(usu);
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await usuarioRepository.Delete(id);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string pass)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(pass))
            {
                return NotFound();
            }
            
            var result = await mailService.ConfirmEmailPassAync(userId, pass);

            if (result.Ok)
            {
                return Redirect($"{configuration["AppUrl"]}/confirmEmail.html");
            }
            return BadRequest(result);

        }
    }
}
