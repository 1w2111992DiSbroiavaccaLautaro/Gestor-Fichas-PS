using ApiPracticaFinal.Models.DTO.ProyectoDTOs;
using ApiPracticaFinal.Repository.Proyectos;
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
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoRepository proyectoRepository;
        public ProyectoController(IProyectoRepository proyectoRepository)
        {
            this.proyectoRepository = proyectoRepository;
        }
        // GET: api/<ProyectoController>
        [HttpGet]
        public async Task<List<ProyectoDTO>> Get()
        {
            return await proyectoRepository.GetProyectos();
        }

        // GET api/<ProyectoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProyectoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProyectoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProyectoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
