using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.Areas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Areas
{
    public class AreaRepository : IAreaRepository
    {
        private readonly dd4snj9pkf64vpContext context;

        public AreaRepository(dd4snj9pkf64vpContext context)
        {
            this.context = context;
        }
        public async Task<Area> Create(AreaInsertDTO area)
        {
            var a = new Area
            {
                Area1 = area.Area1,
                Activo = true
            };
            if (a != null)
            {
                context.Areas.Add(a);
                await context.SaveChangesAsync();
                return a;
            }
            else
            {
                throw new Exception("No se pudo insertar el area");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);
            if (area != null)
            {
                area.Activo = false;
                context.Areas.Update(area);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("No se encontro ningun area");
            }
        }

        public async Task<AreaDTO> GetAreaId(int id)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id && x.Activo == true);
            if (area != null)
            {
                var a = new AreaDTO
                {
                    Id = area.Id,
                    Area1 = area.Area1,
                    Activo = area.Activo
                };
                return a;
            }
            else
            {
                throw new Exception("No se encontro ningun area");
            }
        }

        public async Task<List<Area>> GetAreas()
        {
            return await context.Areas.Where(x => x.Activo == true).OrderBy(x => x.Area1).ToListAsync();
        }

        public async Task<Area> Update(AreaUpdateDTO area)
        {
            var a = await context.Areas.FirstOrDefaultAsync(x => x.Id == area.Id);
            if (a != null)
            {
                a.Area1 = area.Area1;
                context.Areas.Update(a);
                await context.SaveChangesAsync();
                return a;
            }
            else
            {
                throw new Exception("No se pudo modificar el area");
            }
        }
    }
}
