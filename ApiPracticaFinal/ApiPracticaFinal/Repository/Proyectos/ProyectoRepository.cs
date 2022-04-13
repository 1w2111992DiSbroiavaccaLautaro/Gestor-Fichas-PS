using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.Areas;
using ApiPracticaFinal.Models.DTO.PresupuestoDTOs;
using ApiPracticaFinal.Models.DTO.ProyectoDTOs;
using ApiPracticaFinal.Models.DTO.PublicacionDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Proyectos
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly dd4snj9pkf64vpContext context;
        public ProyectoRepository(dd4snj9pkf64vpContext context)
        {
            this.context = context;
        }
        public async Task<List<ProyectoDTO>> GetProyectos()
        {
            var proyectos = await context.Proyectos.Where(i => i.Activo.Equals(true)).OrderByDescending(x => x.Id).ToListAsync();
            var areasxproyectosBD = await context.Areasxproyectos.ToListAsync();
            var publixproyectoBD = await context.Publicacionesxproyectos.ToListAsync();
            var presupuestoxproyectoBD = await context.Presupuestos.ToListAsync();
            var areaBD = await context.Areas.ToListAsync();

            var listProyectoDto = new List<ProyectoDTO>();
            
            foreach (var i in proyectos)
            {
                //var areaxProyecto = await context.Areasxproyectos.Where(x => x.Idproyecto == i.Id).ToListAsync();
                var areaxProyecto = areasxproyectosBD.Where(x => x.Idproyecto == i.Id).ToList();
                var publixproyecto = publixproyectoBD.Where(x => x.IdProyecto == i.Id).ToList();
                var presupuestoxproyecto = presupuestoxproyectoBD.Where(x => x.Idproyecto == i.Id).ToList();

                var listaAreaDto = new List<AreaDTO>();
                var listaPublicacionesDto = new List<PublicacionDTO>();
                var listaPrespuestoDto = new List<PresupuestoDTO>();

                foreach (var j in areaxProyecto)
                {
                    //var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == j.Idarea);
                    var area = areaBD.FirstOrDefault(x => x.Id == j.Idarea);

                    if (area != null)
                    {
                        var areaDto = new AreaDTO
                        {
                            Id = area.Id,
                            Area1 = area.Area1
                        };
                        listaAreaDto.Add(areaDto);
                    }
                }
                
                foreach (var p in publixproyecto)
                {
                    var publicacion = publixproyectoBD.FirstOrDefault(x => x.IdPublicacion == p.IdPublicacion);
                    if (publicacion != null)
                    {
                        var publiDTO = new PublicacionDTO
                        {
                            IdPublicacion = publicacion.IdPublicacion,
                            IdProyecto = publicacion.IdProyecto,
                            Año = publicacion.Año,
                            Codigobcs = publicacion.Codigobcs,
                            Publicacion = publicacion.Publicacion
                        };
                        listaPublicacionesDto.Add(publiDTO);
                    }
                }

                foreach (var item in presupuestoxproyecto)
                {
                    var presupuesto = presupuestoxproyectoBD.FirstOrDefault(x => x.Idpresupuesto == item.Idpresupuesto);
                    if (presupuesto != null)
                    {
                        var presupestoDto = new PresupuestoDTO
                        {
                            Idpresupuesto = item.Idpresupuesto,
                            Idproyecto = item.Idproyecto,
                            Honorario = item.Honorario,
                            Equipamiento = item.Equipamiento,
                            Gastos = item.Gastos,
                            Viatico = item.Viatico
                        };
                        listaPrespuestoDto.Add(presupestoDto);
                    }
                }
                var proyectoDto = new ProyectoDTO
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    AnioFinalizacion = i.AnioFinalizacion,
                    AnioInicio = i.AnioInicio,
                    Departamento = i.Departamento,
                    ListaAreas = listaAreaDto,
                    FichaLista = i.FichaLista,
                    MesFinalizacion = i.MesFinalizacion,
                    MesInicio = i.MesInicio,
                    PaisRegion = i.PaisRegion,
                    MontoContrato = i.MontoContrato,
                    Moneda = i.Moneda,
                    Link = i.Link,
                    ListaPublicaciones = listaPublicacionesDto,
                    ListaPresupuestos = listaPrespuestoDto
                };

                listProyectoDto.Add(proyectoDto);
            }
            return listProyectoDto;
        }
    }
}
