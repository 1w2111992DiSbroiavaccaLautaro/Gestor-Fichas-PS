using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.UsuariosDTO;
using ApiPracticaFinal.Repository.SendGrid;
using ApiPracticaFinal.Repository.Usuarios;
using ApiPracticaFinal.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEmailSender emailSender;
        public UsuariosController(IUsuarioRepository usuarioRepository, IMailService mailService, IEmailSender emailSender)
        {
            this.usuarioRepository = usuarioRepository;
            this.mailService = mailService;
            this.emailSender = emailSender;
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            return await usuarioRepository.GetUsuariosAsync();
        }

        [HttpGet("sendMail")]
        public async Task<IActionResult> SendEmail()
        {
            await emailSender.SendEmailAsync("lautaro42genial@gmail.com", "Mail enviado", "Hola lau");
            //await mailService.SendEmail("lautaro42genial@gmail.com", "Email enviado", "<h1>Este es el cuerpo del correo</h1>");
            return Ok();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<Usuario> Post(UsuarioSignUp usu)
        {
            return await usuarioRepository.Signup(usu);
        }

        [HttpPost("Login")]
        public ActionResult<ResultadosApi> Login(UsuarioLogin usu)
        {
            return usuarioRepository.Login(usu);
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
    }
}
