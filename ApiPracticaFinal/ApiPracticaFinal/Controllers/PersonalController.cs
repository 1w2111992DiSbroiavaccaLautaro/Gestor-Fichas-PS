using ApiPracticaFinal.Models;
using ApiPracticaFinal.Repository.Personales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPracticaFinal.Controllers.Personales
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {

        private readonly IPersonalRepository personalRepository;
        public PersonalController(IPersonalRepository personalRepository)
        {
            this.personalRepository = personalRepository;
        }
        // GET: api/<PersonalController>
        [HttpGet]
        public async Task<List<Personal>> GetPersonal()
        {
            return await personalRepository.GetPersonalAsync();
        }

        // GET api/<PersonalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PersonalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
