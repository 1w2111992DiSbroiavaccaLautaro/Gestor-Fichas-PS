using ApiPracticaFinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Personales
{
    public class PersonalRepository : IPersonalRepository
    {

        private readonly dd4snj9pkf64vpContext context;

        public PersonalRepository(dd4snj9pkf64vpContext context)
        {
            this.context = context;
        }
        public async Task<List<Personal>> GetPersonalAsync()
        {
            return await context.Personals.Where(x => x.Activo == true).OrderBy(x => x.Nombre).ToListAsync();
        }
    }
}
