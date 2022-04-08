using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.PersonalesDTO;
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

        public async Task<Personal> Create(PersonalInsert insert)
        {
            var personal = new Personal
            {
                Nombre = insert.Nombre,
                Activo = true
            };

            if (personal != null)
            {
                context.Personals.Add(personal);
                await context.SaveChangesAsync();
                return personal;
            }
            else
            {
                throw new Exception("No se pudo insertar el personal;");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var p = await context.Personals.FindAsync(id);
            if(p != null)
            {
                p.Activo = false;
                context.Personals.Update(p);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("No se pudo encontrar el personal");
            }
        }

        public async Task<List<Personal>> GetPersonalAsync()
        {
            return await context.Personals.Where(x => x.Activo == true).OrderBy(x => x.Nombre).ToListAsync();
        }

        public async Task<List<Personal>> GetPersonalId(int id)
        {
            return await context.Personals.Where(x => x.Id == id && x.Activo == true).ToListAsync();
        }

        public async Task<Personal> Update(PersonalUpdate update)
        {
            var p = context.Personals.FirstOrDefault(x => x.Id == update.Id);
            if (p != null)
            {
                p.Nombre = update.Nombre ?? p.Nombre;
                p.Activo = true;

                context.Personals.Update(p);
                await context.SaveChangesAsync();
                return p;
            }
            else
            {
                throw new Exception("No se pudo actualizar");
            }
        }
    }
}
