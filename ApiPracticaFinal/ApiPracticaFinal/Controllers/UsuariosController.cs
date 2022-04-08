using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.UsuariosDTO;
using ApiPracticaFinal.Repository.Usuarios;
using ApiPracticaFinal.Resultados;
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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly dd4snj9pkf64vpContext context;
        public UsuariosController(IUsuarioRepository usuarioRepository, dd4snj9pkf64vpContext context)
        {
            this.usuarioRepository = usuarioRepository;
            this.context = context;
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            return await usuarioRepository.GetUsuariosAsync();
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
