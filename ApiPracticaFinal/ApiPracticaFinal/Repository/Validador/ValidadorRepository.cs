using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.Validadores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Validador
{
    public class ValidadorRepository : IValidadorRepository
    {
        private readonly dd4snj9pkf64vpContext context;

        public ValidadorRepository(dd4snj9pkf64vpContext context)
        {
            this.context = context;
        }
        public async Task<Validadore> Create(ValidadorInsert validador)
        {
            var v = new Validadore
            {
                Nombre = validador.Nombre,
                Activo = true
            };
            if (v!= null)
            {
                await context.Validadores.AddAsync(v);
                await context.SaveChangesAsync();
                return v;
            }
            else
            {
                throw new Exception("No se pudo insertar");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var v = await context.Validadores.FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);
            if(v != null)
            {
                v.Activo = false;
                context.Validadores.Update(v);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("No se pudo eliminar");
            }
        }

        public async Task<List<Validadore>> GetValidadores()
        {
            return await context.Validadores.Where(x => x.Activo == true).OrderBy(x => x.Nombre).ToListAsync();
        }

        public async Task<Validadore> GetValidadorId(int id)
        {
            return await context.Validadores.FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);
        }

        public async Task<Validadore> Update(ValidadorUpdate validador)
        {
            var v = await context.Validadores.FirstOrDefaultAsync(x => x.Id == validador.Id && x.Activo == true);
            if (v != null)
            {
                v.Nombre = validador.Nombre;
                context.Validadores.Update(v);
                await context.SaveChangesAsync();
                return v;
            }
            else
            {
                throw new Exception("No se pudo actualizar");
            }
        }
    }
}
